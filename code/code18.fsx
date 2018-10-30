open System
let (|Default|) defaultValue input =
    defaultArg input defaultValue

let showList (Default [] lst) =
    match lst with
    | [] -> printfn "The list is an empty list."
    | _ -> lst |> List.iter (printfn "%A ")

let randomSequence (Default 1 m) (Default 100 n) =
    seq {
        let rng = new Random()
        while true do
            yield rng.Next(m, n)
    }

let rec maxRot (n: int) =
  n 
  |> string
  |> Seq.map (string >> Int32.Parse)
  |> Array.ofSeq
  |> rotate
  |> List.map (Array.map string >> String.concat "" >> int)
  |> List.max

and rotate (xs: int array) =
    let ret = ResizeArray<int array> []
    let mutable ys = xs
    ys |> ret.Add
    for i in 0 .. ys.Length - 2 do
        if i = 0 then
            ys <- Array.concat [ ys.[1..]; ys.[..0] ]
            ys |> ret.Add
        else
            ys <- Array.concat [ ys.[..(i - 1)]; ys.[(i + 1)..]; [| ys.[i] |] ]
            ys |> ret.Add
    ret |> List.ofSeq
