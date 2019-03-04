open System

let center = 14., -12.

let radius = 6.

let getCord center radius theta =
    let centX, centY = center
    let x, y = (radius * cos(theta)), (radius * sin(theta))
    (x + centX), (y + centY)

Array.init (100) (fun x -> (Math.PI / 2.) + (float x) * ((Math.PI / 2.) / 100.))
|> Array.map (getCord center radius)
|> Array.map (fun (x, y) -> sprintf "SectionPlane.Add(FVector(%f, 0, %f));\n" x y)
|> Array.iter (printf "%s") 

open System.IO

File.ReadAllLines("*")
|> Array.filter (fun line -> line.Contains("Debugger"))
|> fun x -> File.WriteAllLines("*", x)

open System
open System.IO

let path = @"*"

let dirs = Directory.EnumerateDirectories(path)

let rand = Random();

dirs 
|> Seq.map DirectoryInfo
|> Seq.iter (fun dir -> dir.MoveTo((sprintf "*%d" (rand.Next()))))