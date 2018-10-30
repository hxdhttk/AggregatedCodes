open System
open System.IO
open System.Collections.Generic

type Instruction =
    | Set of string * string
    | Add of string * string
    | Mul of string * string
    | Mod of string * string
    | Snd of string
    | Rcv of string
    | Jgz of string * string

let input = File.ReadAllText(@"ac18.txt").Split([| "\r\n" |], options=StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun str -> let row = str.Split([| ' ' |])
                                     match row.[0] with
                                     | "snd" -> Snd(row.[1])
                                     | "rcv" -> Rcv(row.[1])
                                     | "set" -> Set(row.[1], row.[2])
                                     | "add" -> Add(row.[1], row.[2])
                                     | "mul" -> Mul(row.[1], row.[2])
                                     | "mod" -> Mod(row.[1], row.[2])
                                     | "jgz" -> Jgz(row.[1], row.[2])
                                     | _ -> failwith "Invalid path!")

let rec startCompute pos lastSnd (registers: Dictionary<string, int64>) (instructions: Instruction []) =
    match instructions.[pos] with
    | Set(reg, v) -> let (isNumber, number) = Int32.TryParse(v)
                     let hasValue = registers.ContainsKey(reg)
                     if hasValue then
                        if isNumber then registers.[reg] <- (int64 number)
                        else registers.[reg] <- registers.[v]
                     else
                        if isNumber then registers.Add(reg, (int64 number))
                        else registers.Add(reg, registers.[v])
                     startCompute (pos + 1) lastSnd registers instructions
    | Add(reg, v) -> startCompute (pos + 1) lastSnd (exec reg v registers (+)) instructions
    | Mul(reg, v) -> startCompute (pos + 1) lastSnd (exec reg v registers (*)) instructions
    | Mod(reg, v) -> startCompute (pos + 1) lastSnd (exec reg v registers (%)) instructions
    | Snd(reg) -> startCompute (pos + 1) (registers.[reg]) registers instructions
    | Rcv(reg) -> if registers.[reg] <> 0L then
                    lastSnd
                  else
                    startCompute (pos + 1) lastSnd registers instructions
    | Jgz(reg, v) -> if registers.[reg] > 0L then
                        let (isNumber, number) = Int32.TryParse(v)
                        if isNumber then
                            startCompute (pos + int number) lastSnd registers instructions
                        else 
                            startCompute (pos + int registers.[v]) lastSnd registers instructions
                     else
                        startCompute (pos + 1) lastSnd registers instructions

and exec reg v (registers: Dictionary<string, int64>) func =
    let (isNumber, number) = Int32.TryParse(v)
    let hasValue = registers.ContainsKey(reg)
    if hasValue then
        if isNumber then registers.[reg] <- (func registers.[reg] (int64 number))
        else registers.[reg] <- (func registers.[reg] registers.[v])
    else
        if isNumber then registers.Add(reg, func 0L (int64 number))
        else registers.Add(reg, func 0L registers.[v])
    registers

printfn "%A" (startCompute 0 0L (Dictionary()) input)