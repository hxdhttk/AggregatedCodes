open System.IO

let input = File.ReadAllText(@"ac11.txt")

let instructions = input.Split([| ',' |])

let finalPos = instructions
               |> Seq.fold (fun state instruction ->
                                let (x, y) = state 
                                match instruction with
                                | "nw" -> (x - 1, y + 1)
                                | "n" -> (x, y + 1)
                                | "ne" -> (x + 1, y + 1)
                                | "sw" -> (x - 1, y - 1)
                                | "s" -> (x, y - 1)
                                | "se" -> (x + 1, y - 1))
                           (0, 0)
