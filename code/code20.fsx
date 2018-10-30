let rec gps (s: int) (l: list<float>) =
    if l |> List.length <= 1 then 0
    else
        l
        |> Seq.pairwise
        |> Seq.map (getV (float s))
        |> Seq.max
        |> floor
        |> int

and getV (s: float) (ds: float * float) : float =
    let d1, d2 = ds
    (3600.0 * (d2 - d1)) / s

open System

let race v1 v2 g =
    match v1 >= v2 with
    | true -> None
    | false -> let time = (decimal g) / (decimal (v2 - v1))
               let totalSeconds = time * 3600.0m |> float
               let timeSpan = TimeSpan.FromSeconds totalSeconds
               Some [ 24 * timeSpan.Days + timeSpan.Hours; timeSpan.Minutes; timeSpan.Seconds ]

let rec chooseBestSum(t: int) (k: int) (ls: int list) =
    if k > (ls |> List.length) then -1
    else
       nChooseK ls k
       |> List.map List.sum
       |> List.filter (fun x -> x <= t)
       |> function | [] -> -1
                   | x -> List.max x

and nChooseK n k = let rec choose lo  = function
                                        | 0 -> [ [ ] ]
                                        | i -> [ for j = lo to (List.length n) - 1 do for ks in choose (j + 1) (i - 1) do yield n.[j] :: ks ]
                                        in choose 0 k