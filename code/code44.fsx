let calD toCal d =
    let rec inner toCal d count =
        match toCal with
        | 0 -> count
        | _ -> 
            let digit = toCal % 10
            if digit = d then
                inner (toCal / 10) d (count + 1)
            else
                inner (toCal / 10) d (count)
    inner toCal d 0

let nbDig n d =
    [
        for i in [0 .. n] do
            let toCal = pown i 2
            yield calD toCal d
    ]
    |> List.sum