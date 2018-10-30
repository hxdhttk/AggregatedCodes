open System
open System.IO

let fibs n = Array.unfold (fun (x, y, z) ->
    if z > 0 then Some (x, (y, x + y, z - 1)) else None) <| (1, 1, n)

let exn = InvalidOperationException()

let isEven i = if i &&& 1L = 0L then raise exn else ()

let isEven' i = i &&& 1L = 0L

let test() =
    let mutable s = 0L
    for i in 1L .. 100000000L do
        try isEven i
        with | _ -> s <- s + 1L
    s

let test'() =
    let mutable s = 0L
    for i in 1L .. 100000000L do
        if isEven' i then s <- s + 1L else ()
    s

let a = 
    // 1. Generating sequence
    [|0L..10000000L|]

let result = 
    a
    // 2. Mapping the sequence into another (Defered execution)
    |> Array.map ((*) 2L)
    // 3. Filtering the sequence (Defered execution)
    |> Array.filter (fun n->n%3L=0L)
    // 4. Reducing the sequence
    |> Array.fold (+) 0L

// As a result
printfn "%A" result

let countVowels str =
    str
    |> String.filter (string >> "AaEeIiOoUu".Contains)
    |> String.length

let undefined<'T> : 'T = failwith "Not implemented yes"

let stub1 (x: int) : float = undefined
let stub2 (x: 'T) : 'T = undefined

type Run<'a, 'b> = 'a -> ('a -> 'b) -> ('b -> unit) -> ('b -> unit) -> unit

let run: Run<_, _> = 
    fun arg1 -> fun pre -> fun proc -> fun after ->
        let arg2 = pre arg1
        do proc arg2
           after arg2

type ConsoleLog<'a> = 'a -> (TextWriter -> 'a -> unit) list -> unit

let consoleLog: ConsoleLog<'a> =
    fun arg -> fun loggers ->
        loggers
        |> List.iter (fun logger -> logger stdout arg)