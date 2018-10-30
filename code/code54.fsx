let keys m = m |> Map.toList |> List.map (fun (k, v) -> k)

let merge f m1 m2 =
    let k1 = Set.ofList (keys m1)
    let k2 = Set.ofList (keys m2)
    let ks = Set.union k1 k2
    ks
    |> Seq.choose (fun k ->
        let o1 = Map.tryFind k m1
        let o2 = Map.tryFind k m2
        f k o1 o2
        |> Option.map (fun v -> k, v))
    |> Map.ofSeq

let sum _ m1Value m2Value =
    match m1Value, m2Value with
    | Some m1, Some m2 -> Some (m1 + m2)
    | Some m1, None -> Some m1
    | None, Some m2 -> Some m2
    | None, None -> None

let m1 = 
    seq {
        for alphbet in ['A' .. 'Z' ] do
            yield alphbet, int alphbet
    } |> Map.ofSeq

let m2 = 
    seq {
        for alphabet in ['A' .. 'Z'] do
            yield alphabet, 1
    } |> Map.ofSeq

printfn "%A" m1
printfn "%A" m2
printfn "%A" (merge sum m1 m2)
