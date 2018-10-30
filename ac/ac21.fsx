open System
open System.IO

type Pattern = int[][]

type EnhancementRule2 = {
    Patterns: Pattern []
    Result: Pattern
}

type EnhancementRule3 = {
    Patterns: Pattern []
    Result: Pattern
}

let strToPattern (str: string) =  str.Split([| '/' |])
                                  |> Array.map (fun str -> str.ToCharArray() |> Array.map (function | '#' -> 1 | '.' -> 0 | _ -> failwith "Invalid path!"))

let initialPattern = ".#./..#/###" |> strToPattern

let init length =
    Array.init length (fun _ -> Array.init length (fun _ -> 0))

let flip2 (pattern: Pattern) =
    let flipped = init 2
    flipped.[0].[0] <- pattern.[0].[1]
    flipped.[0].[1] <- pattern.[0].[0]
    flipped.[1].[0] <- pattern.[1].[1]
    flipped.[1].[1] <- pattern.[1].[0]
    flipped

let rotate2 (pattern: Pattern) =
    let innerRotate (p: Pattern) =
        let r = init 2
        r.[0].[0] <- p.[0].[1]
        r.[0].[1] <- p.[1].[1]
        r.[1].[0] <- p.[0].[0]
        r.[1].[1] <- p.[1].[0]
        r
    let r1 = pattern |> innerRotate
    let r2 = r1 |> innerRotate
    let r3 = r2 |> innerRotate
    [r1; r2; r3]

let flip3 (pattern: Pattern) =
    let flipped = init 3
    flipped.[0].[0] <- pattern.[0].[2]
    flipped.[0].[1] <- pattern.[0].[1]
    flipped.[0].[2] <- pattern.[0].[0]
    flipped.[1].[0] <- pattern.[1].[2]
    flipped.[1].[1] <- pattern.[1].[1]
    flipped.[1].[2] <- pattern.[1].[0]
    flipped.[2].[0] <- pattern.[2].[2]
    flipped.[2].[1] <- pattern.[2].[1]
    flipped.[2].[2] <- pattern.[2].[0]
    flipped

let rotate3 (pattern: Pattern) =
    let innerRotate (p: Pattern) =
        let r = init 3
        r.[0].[0] <- p.[0].[2]
        r.[0].[1] <- p.[1].[2]
        r.[0].[2] <- p.[2].[2]
        r.[1].[0] <- p.[0].[1]
        r.[1].[1] <- p.[1].[1]
        r.[1].[2] <- p.[2].[1]
        r.[2].[0] <- p.[0].[0]
        r.[2].[1] <- p.[1].[0]
        r.[2].[2] <- p.[2].[0]
        r
    let r1 = pattern |> innerRotate
    let r2 = r1 |> innerRotate
    let r3 = r2 |> innerRotate
    [r1; r2; r3]

let rules2 = File.ReadLines(@"ac21_1.txt")
             |> Seq.map (fun str -> let result = str.Split([| " => " |], options=StringSplitOptions.RemoveEmptyEntries)
                                    let pattern = result.[0] |> strToPattern
                                    let output = result.[1] |> strToPattern
                                    { EnhancementRule2.Patterns = pattern :: (flip2 pattern) :: (rotate2 pattern) |> Array.ofList |> Array.distinct
                                      Result = output }) 

let rules3 = File.ReadLines(@"ac21_2.txt")
             |> Seq.map (fun str -> let result = str.Split([| " => " |], options=StringSplitOptions.RemoveEmptyEntries)
                                    let pattern = result.[0] |> strToPattern
                                    let output = result.[1] |> strToPattern
                                    { EnhancementRule3.Patterns = pattern :: (flip3 pattern) :: (rotate3 pattern) |> Array.ofList |> Array.distinct
                                      Result = output })

let mergeH (m1: Pattern) (m2: Pattern) : Pattern =
    Array.zip m1 m2
    |> Array.map (fun (l, r) -> Array.append l r)

let mergeV (m1: Pattern) (m2: Pattern) : Pattern =
    seq {
        for rows1 in m1 do
            yield rows1
        for rows2 in m2 do
            yield rows2
    } |> Array.ofSeq

let rec iter (pattern: Pattern) iterCount (rulesFor2: seq<EnhancementRule2>) (rulesFor3: seq<EnhancementRule3>) =
    if iterCount > 0 then
        if pattern.Length % 2 = 0 then
            let out = transform2 pattern pattern.Length rulesFor2
            iter out (iterCount - 1) rulesFor2 rulesFor3
        elif pattern.Length % 3 = 0 then
            let out = transform3 pattern pattern.Length rulesFor3
            iter out (iterCount - 1) rulesFor2 rulesFor3
        else
            failwith "Invalid length!"
    else
        pattern

and transform2 (input: Pattern) length (rulesFor2: seq<EnhancementRule2>) =
    let results = ResizeArray<Pattern> []
    let sliceCount = length / 2
    for i in 0 .. sliceCount - 1 do
        for j in 0 .. sliceCount - 1 do
            let slicedArray =
                seq {
                    yield [| input.[2 * j].[2 * i]; input.[2 * j].[2 * i + 1] |]
                    yield [| input.[2 * j + 1].[2 * i]; input.[2 * j + 1].[2 * i + 1] |]
                } |> Array.ofSeq
            let matchedRule = rulesFor2 |> Seq.find (fun r -> r.Patterns |> Array.contains slicedArray)
            results.Add(matchedRule.Result)
    results |> Seq.chunkBySize sliceCount
            |> Seq.map (fun patterns -> Array.reduce (mergeV) patterns)
            |> Seq.reduce (mergeH)

and transform3 (input: Pattern) length (rulesFor3: seq<EnhancementRule3>) =
    let results = ResizeArray<Pattern> []
    let sliceCount = length / 3
    for i in 0 .. sliceCount - 1 do
        for j in 0 .. sliceCount - 1 do
            let slicedArray =
                seq {
                    yield [| input.[3 * j].[3 * i]; input.[3 * j].[3 * i + 1]; input.[3 * j].[3 * i + 2] |]
                    yield [| input.[3 * j + 1].[3 * i]; input.[3 * j + 1].[3 * i + 1]; input.[3 * j + 1].[3 * i + 2] |]
                    yield [| input.[3 * j + 2].[3 * i]; input.[3 * j + 2].[3 * i + 2]; input.[3 * j + 2].[3 * i + 2] |]
                } |> Array.ofSeq
            let matchedRule = rulesFor3 |> Seq.find (fun r -> r.Patterns |> Array.contains slicedArray)
            results.Add(matchedRule.Result)
    results |> Seq.chunkBySize sliceCount
            |> Seq.map (fun patterns -> Array.reduce (mergeV) patterns)
            |> Seq.reduce (mergeH)

let answer = iter initialPattern 5 rules2 rules3

let count = Array.sumBy (fun row -> row |> Array.filter (fun e -> e = 1) |> Array.length) answer
printfn "%A" count