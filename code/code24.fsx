#nowarn "25"

open System
open FSharp.Quotations

let inline (/!) a b =
    match b <> LanguagePrimitives.GenericZero with
    | true -> Ok (a / b)
    | false -> Error "Devided by zero!"

let err = 15M /! 0M
let res1 = 15 /! 3
let res2 = 15M /! 3M
let res3 = 15 /! 5

let inline giveMeFive res = res |> Result.bind (int >> (function | 5 -> Ok 5 | _ -> Error "It is not five!"))

giveMeFive res1
giveMeFive res2
giveMeFive res3
giveMeFive err

type T1 = { f1: string; f2: int; f3: DateTime }
type T2 = { f2: int; f3: DateTime; f4: float }

type TCommon = { f2: int; f3: DateTime }

let inline toTCommon n = {
    TCommon.f2 = (^N: (member f2: int) n)
    f3 = (^N: (member f3: DateTime) n)
}

let primes =
    let rec filterPrime nums =
        let p = nums |> Seq.head
        let xs = nums |> Seq.tail
        seq {
            yield p
            yield! (filterPrime (seq { for x in xs do if x % p <> 0 then yield x }))
        }
    filterPrime (Seq.initInfinite ((+) 2))

let primes' = primes |> Seq.cache

let listOfPrimes n =
    let rec filterPrime lst =
        match lst with
        | [] -> []
        | p :: xs -> p :: (filterPrime [ for x in xs do if x % p <> 0 then yield x ])
    filterPrime (List.init n ((+) 2))

primes |> Seq.truncate 500

let sqr e = <@ let v = %e in v * v @>
let mul x e = <@ %x * %e @>

let rec genpow y x =
    if y = 0 then <@ 1 @>
    elif y % 2 = 0 then sqr (genpow (y / 2) x)
    else mul x (genpow (y - 1) x)

let x = Var("x", typeof<int>)

let specialpow y =
    Expr.Lambda(x, genpow y (Expr.Cast<int>(Expr.Var(x))))

let chunk numbers size = Array.chunkBySize size numbers

let concat first second = Array.append first second

let countOccurrences numbers value = numbers |> ((Array.filter ((=) value)) >> Array.length)

let rec deepFlatten (input: obj array) =
    [| for element in input do
           let t = element.GetType()
           if t.IsArray then
               yield! deepFlatten (element :?> obj array)
           else
               yield element |]

let difference first second = Array.except second first

let differenceWith first second comparator =
    first
    |> Array.filter (fun a -> second |> Array.exists (fun b -> comparator a b) |> not)

let distinctValuesOfArray elements = elements |> Array.distinct

let dropElements elements condition = elements |> Array.skipWhile condition

let dropRight elements n =
    match n < 0 with
    | true -> None
    | false -> let arrayLength = Array.length elements
               match n < arrayLength with
               | true -> Some elements.[.. (arrayLength - n - 1)]
               | false -> Some [||]

let everyNth elements nth =
    [| for index, element in elements |> Array.indexed do
           if index % nth = nth - 1 then
               yield element |]

let indexOf elements el = elements |> Array.tryFindIndex ((=) el)
                                   |> function | Some x -> x | None -> -1

let lastIndexOf elements el = elements |> Array.tryFindIndexBack ((=) el)
                                       |> function | Some x -> x | None -> -1

let filterNonUnique elements = elements |> Array.groupBy id
                                        |> Array.filter (snd >> Array.length >> (=) 1)
                                        |> Array.map fst

let flatten (elements: obj array) =
    [| for element in elements do
           let t = element.GetType()
           if t.IsArray then 
               yield! (element :?> obj array)
           else
               yield element |]

let rec flattenDepth (elements: obj array) depth =
        match depth with
        | 0 -> elements
        | _ -> [| for element in elements do
                      let t = element.GetType()
                      if t.IsArray then
                          yield! flattenDepth (element :?> obj array) (depth - 1)
                      else
                          yield element |]

let groupBy elements func = elements |> Array.groupBy func

let initial (elements: 'a array) = elements.[..(elements.Length - 2)]

let initializeArrayWithRange ``end`` start = [| start .. ``end`` |]

let initializeArrayWithValues n value = [| for _ in 1 .. n -> value |]

let intersection first second = first |> Array.filter (fun x -> set second |> Set.contains x)

let isSorted arr =
    match arr = (arr |> Array.sort), arr = (arr |> Array.sortDescending) with
    | true, _ -> 1
    | _, true -> -1
    | _, _ -> 0

let join arr separator ``end`` =
    arr |> Array.map string
        |> String.concat separator
        |> (fun x -> x + ``end``)

let nthElement (arr: 'a array) n =
    match n > 0 with
    | true -> arr.[n - 1]
    | false -> arr.[arr.Length + n]

let pick obj arr =
    arr |> Array.filter (fun x -> obj |> Map.containsKey x)
        |> Array.map (fun x -> x, obj.[x])
        |> Map

let reducedFilter data keys fn =
    data
    |> Array.filter fn
    |> Array.map (fun el -> keys |> Array.filter (fun x -> el |> Map.containsKey x)
                                 |> Array.map (fun x -> x, el.[x])
                                 |> Map)

let sample (arr: 'a array) = let rnd = Random()
                             arr.[(rnd.NextDouble() * (float arr.Length)) |> (floor >> int)]

let sampleSize input n =
    let rnd = Random()
    let rec inner (arr: 'a array) m =
        match m with
        | 0 -> arr
        | _ -> let i = (rnd.NextDouble() * (float m)) |> (floor >> int)
               let tmp = arr.[i]
               arr.[i] <- arr.[m]
               arr.[m] <- tmp
               inner arr (m - 1)
    let copy = input |> Array.copy
    let length = input.Length
    if n > length then (inner copy (length - 1)) else (inner copy n) |> Array.take n

let shuffle input =
    let rnd = Random()
    let rec inner (arr: 'a array) m =
        match m with
        | 0 -> arr
        | _ -> let i = (rnd.NextDouble() * (float m)) |> (floor >> int)
               let tmp = arr.[i]
               arr.[i] <- arr.[m]
               arr.[m] <- tmp
               inner arr (m - 1)
    let copy = input |> Array.copy
    let length = input.Length
    inner copy (length - 1)

let similarity first second =
    first |> Array.filter (fun x -> set second |> Set.contains x)

let sortedIndex (arr: 'a array) el =
    let isDescending = arr.[0] > arr.[1]
    [| 0 .. arr.Length - 1 |]
    |> Array.filter (fun index -> if isDescending then el >= arr.[index] else el <= arr.[index])
    |> Array.tryHead
    |> function | Some x -> x | None -> arr.Length

let symmericDifference first second =
    [| first |> Array.filter (fun x -> set second |> Set.contains x);
       second |> Array.filter (fun x -> set first |> Set.contains x) |]
    |> Array.concat

let tail arr = arr |> Array.tail

let take arr n = arr |> Array.skip n

let takeRight (arr: 'a array) n = arr.[(arr.Length - n)..]

let union first second = set first |> Set.union (set second) |> Array.ofSeq

let without arr elements = arr |> Array.filter (fun x -> set elements |> Set.contains x |> not)

let zip (arrays: 'a array array) =
    let maxIndex = arrays |> Array.map (fun x -> x.Length)
                     |> Array.max
    [| for index in [0 .. maxIndex - 1] do
           yield arrays |> Array.map (fun x -> if index < x.Length then Some x.[index] else None) |]

let zipObject props values =
    props
    |> Array.mapi (fun index prop -> prop, if index < (values |> Array.length) then Some values.[index] else None)
    |> Map

let inline average (arr: ^a array when ^a : (static member (+) : ^a * ^a -> ^a)) = arr |> Array.average

let gcd numbers =
    let rec inner a b =
        match b = 0 with
        | true -> a
        | false -> inner b (a % b)
    numbers |> Array.reduce inner

let lcm numbers =
    let rec gcd' a b =
        match b = 0 with
        | true -> a
        | false -> gcd' b (a % b)
    let lcm' a b = (a * b) / (gcd' a b)
    numbers |> Array.reduce lcm'

let findNextPositivePowerOfTwo value = 1 <<< Convert.ToString(value - 1, 2).Length

let isEven value = (value &&& 1) = 0

let isPowerOfTwo value = (value > 0) && ((value &&& (~~~value + 1)) = value)

let generateRandomInt () =
    let rnd = Random()
    rnd.Next(Int32.MinValue, Int32.MaxValue)
