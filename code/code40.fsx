open System

let curry f arg1 arg2 =
    let args = (arg1, arg2)
    f args

let curry3 f arg1 arg2 arg3 =
    let args = arg1, arg2, arg3
    f args

let a1 = "a"
let a2 = "b"
curry String.Compare a1 a2

let uncurry f args =
    let arg1, arg2 = args
    f arg1 arg2

let uncurry3 f args =
    let arg1, arg2, arg3 = args
    f arg1 arg2 arg3

let a = (1, 2)
uncurry (+) a

let inline implicitConvert< ^a, ^b when ^b : (static member op_Implicit: ^a -> ^b) > (value: ^a) =
    (^b : (static member op_Implicit: ^a -> ^b) (value))

let inline (~%%)< ^a, ^b when ^b : (static member op_Implicit: ^a -> ^b) > (value: ^a) = implicitConvert< ^a, ^b > value

let generateSkipGrams windowSize (data: string array) =
    assert (windowSize % 2 <> 0)
    let middleIdx = windowSize / 2
    data
    |> Array.windowed windowSize
    |> Array.map (fun arr -> ([| yield! arr.[0 .. middleIdx - 1]; yield! arr.[middleIdx + 1 .. arr.Length - 1] |], arr.[middleIdx]))

let mergeSkipGramsToPairs (skipGrams: (string array * string) array) =
    [|
        for skipGram in skipGrams do
            let context, dataset = skipGram
            yield! [|
                for word in context do
                yield (dataset, word) 
            |] 
    |]