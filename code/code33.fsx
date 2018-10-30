module List =
    let countOccurrences value lst =
        let rec inner lst occurrences =
            match lst with
            | [] -> occurrences
            | head :: tail -> if head = value then
                                inner tail (occurrences + 1)
                              else
                                inner tail occurrences
        inner lst 0

module Array =
    let countOccurrences value (arr: 'a array) =
        let mutable acc = 0
        for i in 0 .. (arr.Length - 1) do
            if arr.[i] = value then
                acc <- acc + 1
        acc

module Seq =
    let countOccurrences value (iter: 'a seq) =
        let mutable acc = 0
        let enumerator = iter.GetEnumerator()
        while enumerator.MoveNext() do
            if enumerator.Current = value then
                acc <- acc + 1
        acc

