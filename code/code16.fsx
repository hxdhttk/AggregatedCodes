open System
open System.Collections.Generic

let memoize f = 
    let cache = Dictionary<_, _>()
    fun x ->
        if cache.ContainsKey(x) then
            cache.[x]
        else
            let res = f x
            cache.[x] <- res
            res

let memoizeAppend =
    memoize (fun input -> 
        printfn "Working out the value for '%A'" input
        String.concat ", " [ for i in 0 .. 9 -> sprintf "%d%s" i input ])

Console.WriteLine(memoizeAppend("this"))
Console.WriteLine(memoizeAppend("this"))

let rec fact n =
    match n with
    | 0 | 1 -> 1I
    | _ -> (bigint n) * (fact (n - 1))

let memoizeFact = memoize fact

[for i in 20 .. 30 -> memoizeFact i] |> printfn "%A"
[for i in 20 .. 30 -> memoizeFact i] |> printfn "%A"