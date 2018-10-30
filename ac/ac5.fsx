open System.IO

let input = File.ReadAllText(@"ac5.txt")

let stringToInt = string >> int

let instructions = input.Split([| ' ' |]) |> Array.map stringToInt

let mutable pos = 0
let mutable stepCount = 0

let answer =
    let length = instructions.Length
    let rec jump () =
        match instructions.[pos] with
        | jmp when (pos + jmp) >= length || (pos + jmp) < 0 ->
            instructions.[pos] <- instructions.[pos] + 1
            stepCount <- stepCount + 1
        | jmp -> let nextPos = pos + jmp
                 instructions.[pos] <- instructions.[pos] + 1
                 stepCount <- stepCount + 1
                 pos <- nextPos
                 jump()
    jump()
    stepCount