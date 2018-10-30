open System
open System.Linq
open System.Runtime.InteropServices
open System.Threading
open System.Diagnostics

let decay = Seq.initInfinite (fun x -> exp ((float x / -1000.) / 2. * Math.PI))

let rec qsort1 lst =
    match lst with
    | [] -> []
    | x :: xs -> let lessThanX = [ for y in xs do if y < x then yield y ]
                 let greaterThanOrEqualToX = [ for y in xs do if y >= x then yield y ]
                 qsort1 (lessThanX @ [x] @ greaterThanOrEqualToX)

let rec perms lst =
    match lst with
    | [] -> [ [] ]
    | x :: xs ->
        let rec between e lst' =
            match e, lst' with
            | e, [] -> [[e]]
            | e, y :: ys -> (e :: y :: ys) :: (List.map (fun s -> y :: s) (between e ys))
        List.collect (between x) (perms xs)

let employees = [ ("Simon", "MS", 80); 
                  ("Erik", "MS", 100); 
                  ("Phil", "Ed", 40); 
                  ("Gordon", "Ed", 45); 
                  ("Paul", "Yale", 60) ]

let output = query {
    for (name, dept, salary) in employees do
    groupBy dept into grouping
    sortBy (grouping.Sum(fun (s, _, _) -> s))
    select (grouping.Key, grouping.Sum(fun (s, _, _) -> s))
    take 5
}

let output' = 
    employees
    |> List.groupBy (fun (_, dept, _) -> dept)
    |> List.sortBy (fun (_, es) -> es |> List.sumBy (fun (_, _, salary) -> salary))
    |> List.map (fun (key, es) -> key, es |> List.sumBy (fun (_, _, salary) -> salary))
    |> List.truncate 5

let g f =
    match f with
    | _ when f.GetType().BaseType = typeof<FSharpFunc<float, float>> -> "This is a mapping from float to float."
    | _ when f.GetType().BaseType = typeof<FSharpFunc<int, int>> -> "This is a mapping from int to int."
    | _ -> sprintf "The type of \"f\" is %s" (f.GetType().Name)

let reverse lst = List.fold (fun xs x -> x :: xs) [] lst

let reverse' lst = List.foldBack (fun x k -> fun acc -> k (x :: acc)) lst id []

let foldl f v lst =
    let rec g lst =
        match lst with
        | [] -> id
        | x :: xs -> fun v -> g xs (f v x)
    g lst v

let acc = ref 0

let rec sumUp r lo hi =
    if lo <> hi then
        r := !r + lo
        sumUp r (lo + 1) hi

let rec sumDn r lo hi =
    if lo <> hi then
        r := !r + lo
        sumDn r lo (hi - 1)

[<DllImport("kernel32.dll")>]
extern unit Sleep(int dwMilliseconds)

let win32Sleep = Sleep
let stopwatch = Stopwatch()

let sleep1 = async { do! Async.Sleep(5000)
                     printfn "Async sleep!!! %dms" stopwatch.ElapsedMilliseconds }
let sleep2 = async { Thread.Sleep(5000)
                     printfn "Thread sleep!!! %dms" stopwatch.ElapsedMilliseconds }
let sleep3 = async { win32Sleep(5000)
                     printfn "Win32 sleep!!! %dms" stopwatch.ElapsedMilliseconds }

[sleep1; sleep2; sleep3]
|> Async.Parallel
|> fun x -> stopwatch.Restart()
            Async.RunSynchronously x