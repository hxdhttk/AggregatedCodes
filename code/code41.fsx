type Adder() =
    member __.Zero() = 0
    member __.Yield(_) = 0
    [<CustomOperation("add")>]
    member __.Add(a, [<System.ParamArray>] args: int []) =
        printf "%A with %A" a args
        let result = args |> Array.fold (+) a
        printfn " = %A" result
        result

let adder = Adder()

adder {
    add [| 1; 2 |]
    add [| 3; 4; 5; 6 |]
    add 7
    add [||]
    add [| 8; 9 |]
    add [| 10; 11; 12; 13; 14 |]
}