open System
open System.Security.Cryptography

// Preparation for the secret rite!
let roll rand count =
    let rec innerRoll rand count lst =
        match lst |> List.length with
        | len when len = count -> lst
        | _ -> 
            let newNumber = rand()
            match List.contains newNumber lst with
            | true ->
                innerRoll rand count lst
            | false ->
                innerRoll rand count (newNumber :: lst)
    innerRoll rand count []

let rec random bound =
    let rng = RandomNumberGenerator.Create();
    let bytes = Array.create 4 0uy
    rng.GetBytes(bytes)
    let number = BitConverter.ToInt32(bytes, 0)
    (abs (number % bound)) + 1

let reds () =
    let rand = fun () -> random 35
    roll rand 5
    |> List.sort

let blues () =
    let rand = fun () -> random 12
    roll rand 2
    |> List.sort

let rec filter filters lst =
    match filters with
    | [] -> lst
    | head :: tail ->
        match head lst with
        | true -> filter tail lst
        | false -> []

let oddEven value lst =
    let oddCount = lst |> List.filter (fun x -> x % 2 = 1) |> List.length
    let evenCount = lst |> List.filter (fun x -> x % 2 = 0) |> List.length
    (float oddCount / float evenCount) - value <= Double.Epsilon

let bigSmall pivot value lst =
    let bigCount = lst |> List.filter (fun x -> x >= pivot) |> List.length
    let smallCount = lst |> List.filter (fun x -> x < pivot) |> List.length
    (float bigCount / float smallCount) - value <= Double.Epsilon

let repeat threshold prev lst =
    let repeatedCount = lst |> List.filter (fun x -> List.contains x prev) |> List.length
    let prevLength = prev |> List.length
    float repeatedCount / float prevLength >= threshold

let continuous threshold lst =
    let rec checkContinuous lst res =
        match lst with
        | [_; _]  | [_] | [] -> res
        | h1 :: h2 :: h3 :: tail ->
            if (h2 - h1 = 1) && (h3 - h2 = 1) then
                checkContinuous (h2 :: h3 :: tail) (res + 1)
            else
                checkContinuous (h2 :: h3 :: tail) res
    let continuousCount = checkContinuous lst 0
    let totalCount = (lst |> List.length) - 2
    float continuousCount / float totalCount >= threshold

let recommend pool threshold lst =
    let totalCount = lst |> List.length
    let recommendedCount = lst |> List.filter (fun x -> List.contains x pool) |> List.length
    (float recommendedCount / float totalCount) >= threshold

// Start selling! My soul is on sale!
let redOddEven = 2.0 / 3.0
let redPivot = 18
let redPrev = [7; 16; 24; 26; 31]
let redBigSmall = 3.0 / 2.0
let redRepeatThreshold = 0.2
let redContinuousThreshold = 0.0
let redPool = [3; 7; 8; 10; 14; 15; 17; 20; 22; 25; 27; 32]
let redThreshold = 0.5

let bluePool = [2; 5; 7; 10]
let blueThreshold = 0.5

let redFilters =
    [
        oddEven redOddEven
        bigSmall redPivot redBigSmall
        repeat redRepeatThreshold redPrev
        continuous redContinuousThreshold
        recommend redPool redThreshold
    ]

let blueFilters =
    [
        recommend bluePool blueThreshold
    ]

// The rite is just beginning!
let sellMySoulToTheDevil () =
    let rec onSale soul filters lst =
        match lst with
        | [] ->
            let newLst = soul()
            onSale soul filters (filter filters newLst)
        | _ -> lst
    [(onSale reds redFilters []); (onSale blues blueFilters [])]

// Sell! Sell! Sell! All I need is money, but not my soul!
printfn "%A" (sellMySoulToTheDevil())