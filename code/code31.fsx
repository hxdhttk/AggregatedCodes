let answerSet = set ['A'; 'B'; 'C'; 'D']

let f a (b, c, d) =
    (b = c) && (c = d) && (a <> b)

let inline h a b =
    let diff = abs ((int a) - (int b))
    diff <> 1

let g t a rest =
    (h t a) && (rest |> Array.forall (h t >> not))

let answers = query {
    for a1 in answerSet do
    for a2 in answerSet do
    for a3 in answerSet do
    for a4 in answerSet do
    for a5 in answerSet do
    for a6 in answerSet do
    for a7 in answerSet do
    for a8 in answerSet do
    for a9 in answerSet do
    for a10 in answerSet do
    let predicates = [| a1; a2; a3; a4; a5; a6; a7; a8; a9; a10 |]
    let max, min = predicates |> Array.groupBy id |> Array.map (snd >> Array.length) |> (fun x -> Array.max x, Array.min x)
    let minAns = predicates |> Array.groupBy id |> Array.map (fun (key, values) -> key, values.Length) |> Array.find (snd >> ((=) min)) |> fst
    let diff = max - min
    where (
        (a2 = 'A' && a5 = 'C' || a2 = 'B' && a5 = 'D' || a2 = 'C' && a5 = 'A' || a2 = 'D' && a5 = 'B') &&
        (a3 = 'A' && f a3 (a6, a2, a4) || a3 = 'B' && f a6 (a3, a2, a4) || a3 = 'C' && f a2 (a6, a3, a4) || a3 = 'D' && f a4 (a6, a2, a3)) &&
        (a4 = 'A' && a1 = a5 || a4 = 'B' && a2 = a7 || a4 = 'C' && a1 = a9 || a4 = 'D' && a6 = a10) &&
        (a5 = 'A' && a8 = 'A' || a5 = 'B' && a4 = 'B' || a5 = 'C' && a9 = 'C' || a5 = 'D' && a7 = 'D') &&
        (a6 = 'A' && a8 = a2 && a8 = a4 || a6 = 'B' && a8 = a1 && a8 = a6 || a6 = 'C' && a8 = a3 && a8 = a10 || a6 = 'D' && a8 = a5 && a8 = a9) &&
        (a7 = 'A' && minAns = 'C' || a7 = 'B' && minAns = 'B' || a7 = 'C' && minAns = 'A' || a7 = 'D' && minAns = 'D') &&
        (a8 = 'A' && g a1 a7 [| a5; a2; a10 |]  || a8 = 'B' && g a1 a5 [| a7; a2; a10 |] || a8 = 'C' && g a1 a2 [| a5; a7; a10 |] || a8 = 'D' && g a1 a10 [| a5; a2; a7 |]) &&
        (a9 = 'A' && ((a1 = a6) <> (a6 = a5)) || a9 = 'B' && ((a1 = a6) <> (a10 = a5)) || a9 = 'C' && ((a1 = a6) <> (a2 = a5)) || a9 = 'D' && ((a1 = a6) <> (a9 = a5))) &&
        (a10 = 'A' && diff = 3 || a10 = 'B' && diff = 2 || a10 = 'C' && diff = 4 || a10 = 'D' && diff = 1)
    )
    select (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10)
}

// seq [('B', 'C', 'A', 'C', 'A', 'C', 'D', 'A', 'B', 'A')]
printfn "%A" answers