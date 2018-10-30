open System
open System.Threading.Tasks

type Dag = private {
    InputValues: obj array
    FunctionInputs: (Set<int> * Set<int>) array
    FunctionFunctions: (Dag -> obj) array
    FunctionValues: Lazy<obj> array
}

module Dag =
    let private append a v =
        let mutable a = a
        Array.Resize(&a, Array.length a + 1)
        a.[Array.length a - 1] <- v
        a

    type Input = private | CellInput
    type Function = private | CellFunction

    type Cell<'a, 'b> = private | Cell of int

    let empty = {
        InputValues = [||]
        FunctionInputs = [||]
        FunctionFunctions = [||]
        FunctionValues = [||]
    }

    let addInput (v: 'a) (d: Dag) : Dag * Cell<'a, Input> =
        {
            d with
                InputValues = box v |> append d.InputValues
        }, Cell d.InputValues.Length

    let getValue (Cell i: Cell<'a, Input>) (d: Dag) : 'a =
        downcast d.InputValues.[i]

    let setInput (Cell i: Cell<'a, Input>) (a: 'a) (d: Dag) : Dag =
        if downcast d.InputValues.[i] = a then d
        else
            let dirtyCalcs =
                Seq.fold (fun (j, s) (inputs, calcInputs) ->
                    if Set.contains i inputs ||
                       Set.intersect s calcInputs |> Set.isEmpty |> not then
                        j + 1, Set.add j s
                    else
                        j + 1, s
                ) (0, Set.empty) d.FunctionInputs
                |> snd

            let inputValues = Array.copy d.InputValues
            inputValues.[i] <- box a

            if Set.isEmpty dirtyCalcs then { d with InputValues = inputValues }
            else
                let functionValues = Array.copy d.FunctionValues
                let dag = {
                    d with
                        InputValues = inputValues
                        FunctionValues = functionValues
                }
                Set.iter (fun i ->
                    functionValues.[i] <- lazy d.FunctionFunctions.[i] dag
                ) dirtyCalcs
                dag

    let getValueTask (Cell i: Cell<'a, Function>) (d: Dag) : Task<'a> =
        downcast d.FunctionValues.[i].Value

    let changed (Cell i: Cell<'a, 't>) (before: Dag) (after: Dag) : bool =
        if typeof<'t> = typeof<Function> then
            before.FunctionValues.[i] <> after.FunctionValues.[i]
        else
            downcast before.InputValues.[i] <> downcast after.FunctionValues.[i]

    type 'a Builder = private {
        Dag: Dag
        Inputs: Set<int> * Set<int>
        Function: Dag -> Task<'a>
    }

    let buildFunction (d: Dag) f = {
        Dag = d
        Inputs = Set.empty, Set.empty
        Function = fun _ -> Task.FromResult f
    }

    let applyCell (Cell i: Cell<'a, 't>) { Dag = dag; Inputs = inI, inC; Function = bFn } =

        let inline taskMap f (t: Task<_>) =
            t.ContinueWith(fun (r: Task<_>) -> f r.Result)

        let isFunctionCall = typeof<'t> = typeof<Function>
        {
            Dag = dag
            Inputs =
                if isFunctionCall then inI, Set.add i inC
                                  else Set.add i inI, inC
            Function =
                if isFunctionCall then
                    fun d ->
                        let fTask = bFn d
                        ( downcast d.FunctionValues.[i].Value
                          |> taskMap (fun a -> taskMap (fun f -> f a) fTask)
                        ).Unwrap()
                else
                    fun d -> 
                        bFn d |> taskMap (fun f ->
                            downcast d.InputValues.[i] |> f)
        }

    let addFunction ({ Dag = dag; Inputs = ips; Function = fn }: 'a Builder) =
        let calc = fn >> box
        let d = {
            dag with
                FunctionInputs = append dag.FunctionInputs ips
                FunctionFunctions = append dag.FunctionFunctions calc
                FunctionValues = append dag.FunctionValues null
        }
        d.FunctionValues.[d.FunctionValues.Length - 1] <- lazy calc d
        let cell: Cell<'a, Function> = Cell dag.FunctionValues.Length
        d, cell

let printfnObject : obj -> unit = printfn "%A"

let test1() =
    let dag, cell1 = Dag.addInput 7 Dag.empty
    printfnObject dag
    printfnObject (Dag.getValue cell1 dag)

let test2() =
    let dag, cell1 = Dag.addInput 8 Dag.empty
    let dag, cell2 = Dag.addInput 9 dag
    printfnObject dag
    printfnObject (Dag.getValue cell1 dag)
    printfnObject (Dag.getValue cell2 dag)

let test3() =
    let dag, cell1 = Dag.addInput 42 Dag.empty
    let dag, cell2 =
        Dag.buildFunction dag (fun x -> x * 10)
        |> Dag.applyCell cell1
        |> Dag.addFunction
    let result = (Dag.getValueTask cell2 dag).Result
    printfnObject dag
    printfnObject result