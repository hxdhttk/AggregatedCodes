let addOneAndSort input =
    input
    |> Array.map (fun x -> x |> List.map string |> String.concat "" |> int |> (+) 1)
    |> Array.sort
    |> Array.map (fun x -> x |> string |> Seq.toList |> List.map (string >> int))

[| [1; 2; 3];
   [9; 9; 9; 9] |]
|> addOneAndSort

let addOne input =
    let revList = input |> List.rev
    let rec innerFn xs ys carry =
        match xs with
        | [] -> match carry with
                | true -> ys @ [1]
                | false -> ys
        | head :: tail -> match carry with
                          | true -> match head with
                                    | 9 -> innerFn tail (ys @ [0]) true
                                    | _ -> innerFn tail (ys @ [head + 1]) false
                          | false -> innerFn tail (ys @ [head]) false
    (innerFn revList [] true) |> List.rev

type NumSource = A | B

let minDiff a b =
    let a' = a |> List.map (fun x -> (x, A))
    let b' = b |> List.map (fun x -> (x, B))
    let merged = a' @ b' |> List.sortBy (fun (x, _) -> x)
    let rec innerFn (xs: (int * NumSource) list) (diffs: int list) =
        match xs with
        | [_] -> diffs |> List.min
        | f :: s :: tail -> let ft, st = snd f, snd s
                            if ft = st then
                                innerFn (s :: tail) diffs
                            else
                                let fv, sv = fst f, fst s
                                let diff = abs (fv - sv)
                                if diff = 0 then
                                    0
                                else
                                    innerFn (s :: tail) (diff :: diffs)
        | _ -> failwith "Invalid path!"
    innerFn merged []

let oddEvenSplit xs =
    (xs |> List.filter (fun x -> x % 2 = 1)) @ (xs |> List.filter (fun x -> x % 2 = 0))

let oddEvenSplitBySort xs =
    xs |> List.sortBy (fun x -> if x % 2 = 1 then -1 else 1)

let rec comb count xs =
    match count, xs with
    | 0, _ -> [[]]
    | _, [] -> []
    | k, (h :: t) -> List.map ((@) [h]) (comb (k - 1) t) @ comb k t
