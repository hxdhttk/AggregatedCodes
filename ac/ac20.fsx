open System
open System.IO
open System.Text.RegularExpressions

type Particle = {
    pos: int * int * int
    v: int * int * int
    a: int * int * int
}

let regex = @"p=<(-?\d+),(-?\d+),(-?\d+)>, v=<(-?\d+),(-?\d+),(-?\d+)>, a=<(-?\d+),(-?\d+),(-?\d+)>" |> Regex

let input = File.ReadAllText(@"ac20.txt").Split([| "\r\n" |], options=StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun str -> let groups = regex.Match(str).Groups
                                     {
                                         Particle.pos = (int groups.[1].Value, int groups.[2].Value, int groups.[3].Value)
                                         v = (int groups.[4].Value, int groups.[5].Value, int groups.[6].Value)
                                         a = (int groups.[7].Value, int groups.[8].Value, int groups.[9].Value)
                                     })

let update particle steps =
    let (pX, pY, pZ) = particle.pos
    let (vX, vY, vZ) = particle.v
    let (aX, aY, aZ) = particle.a
    let displacement x0 v0 a t = x0 + (v0 * t) + (a * t * t / 2)
    (displacement pX vX aX steps,
     displacement pY vY aY steps,
     displacement pZ vZ aZ steps)

let mdToOriginalPos (pos: int * int * int) =
    let x, y, z = pos
    (abs x) + (abs y) + (abs z)

let indices = ResizeArray<int> []
for i in 0 .. 1000 do
    input
    |> Array.map (fun p -> update p i |> mdToOriginalPos)
    |> (fun x -> Array.findIndex (fun d -> d = Array.min x) x)
    |> (fun x -> indices.Add(x))

indices
|> Seq.groupBy id
|> Seq.sortByDescending (fun (_, y) -> Seq.length y)
|> Seq.map (fun (x, y) -> (x, Seq.length y))