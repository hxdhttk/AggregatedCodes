let getMiddle (str : string) =
    let middle =
        let l = str.Length
        match l % 2 = 0 with
        | true -> [| str.[l / 2 - 1]; str.[l / 2] |] 
        | false -> [| str.[l / 2] |]
    middle |> Array.map string |> String.concat ""

let mxdiflg(a1: string[]) (a2: string[]): int Option =
    match a1, a2 with
    | [||], _ | _, [||] -> None
    | a1, a2 -> let (xMax, xMin) = a1 |> Array.map (fun s -> s.Length) |> fun a -> (Array.max a, Array.min a)
                let (yMax, yMin) = a2 |> Array.map (fun s -> s.Length) |> fun a -> (Array.max a, Array.min a)
                [| xMax - yMin; yMax - xMin |]
                |> Array.map abs
                |> Array.max
                |> Some

open System
open System.Linq

module Char =
    let replicate count c =
        seq {
            for _ in 1 .. count do
                yield c 
        }

let range = function
            | a, b when a < b -> [ a .. b - 1]
            | _, _ -> []

let accum (s: string): string =
    s.ToCharArray()
    |> Array.mapi (fun index c -> String.replicate (index + 1) (Char.ToLower(c) |> string))
    |> Array.map (fun str -> (Char.ToUpper(str.[0]) |> string) + str.[1 .. (str.Length - 1)])
    |> String.concat "-"