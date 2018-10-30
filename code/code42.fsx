open System

let inline reds seed =
    let rand = Random(int seed)
    let rec inner numberList =
        match numberList |> List.length with
        | 5 -> numberList
        | _ ->
            let newNumber = rand.Next(1, 36)
            if numberList |> List.contains newNumber then
                inner numberList
            else
                inner (newNumber :: numberList)
    inner []

let inline blues seed = 
    let rand = Random(int seed)
    let rec inner numberList =
        match numberList |> List.length with
        | 2 -> numberList
        | _ ->
            let newNumber = rand.Next(1, 13)
            if numberList |> List.contains newNumber then
                inner numberList
            else
                inner (newNumber :: numberList)
    inner []

let inline roll seed =
    reds seed, blues seed