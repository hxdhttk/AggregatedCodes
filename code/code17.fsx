open System.Collections.Generic
open System

type Instr =
    | MoveR
    | MoveL
    | Flip
    | JumpF of int
    | JumpB of int
    | Nop

type MemoryCell =
    | Zero
    | One

let rec interpret code memory =
    let codeArray = code |> solve
    let memoryArray = memory |> format
    let rec innerFn (xs: Instr array) (ys: MemoryCell array) pos curr =
        if pos >= xs.Length || curr < 0 || curr >= ys.Length then
            ()
        else
            match xs.[pos] with
            | MoveR -> innerFn xs ys (pos + 1) (curr + 1)
            | MoveL -> innerFn xs ys (pos + 1) (curr - 1)
            | Flip -> ys.[curr] <- flip ys.[curr]
                      innerFn xs ys (pos + 1) curr
            | JumpF n -> match ys.[curr] with
                         | Zero -> innerFn xs ys (n + 1) curr
                         | _ -> innerFn xs ys (pos + 1) curr
            | JumpB n -> match ys.[curr] with
                         | One -> innerFn xs ys n curr
                         | _ -> innerFn xs ys (pos + 1) curr
            | Nop -> innerFn xs ys (pos + 1) curr
    innerFn codeArray memoryArray 0 0
    memoryArray |> output

and flip cell =
    match cell with
    | Zero -> One
    | One -> Zero

and solve (code: string) =
    let charList = code.ToCharArray() |> List.ofArray
    let backwardPos = Stack<int>()
    let instrArray = Array.init (code.Length) (fun _ -> Nop)
    let rec innerFn xs pos =
        match xs with
        | [] -> ()
        | head :: tail -> match head with
                          | '>' -> instrArray.[pos] <- MoveR
                                   innerFn tail (pos + 1) 
                          | '<' -> instrArray.[pos] <- MoveL
                                   innerFn tail (pos + 1)
                          | '*' -> instrArray.[pos] <- Flip
                                   innerFn tail (pos + 1)
                          | '[' -> backwardPos.Push(pos)
                                   innerFn tail (pos + 1)
                          | ']' -> let jumpBackTo = backwardPos.Pop()
                                   instrArray.[pos] <- JumpB(jumpBackTo)
                                   instrArray.[jumpBackTo] <- JumpF(pos) 
                                   innerFn tail (pos + 1)
                          | _ -> instrArray.[pos] <- Nop
                                 innerFn tail (pos + 1)
    innerFn charList 0
    instrArray
    
and format (memory: string) =
    let charList = memory.ToCharArray()
    charList
    |> Array.map (function | '1' -> One
                           | '0' -> Zero
                           | _ -> failwith "Invalid memory state!")

and output memory =
    memory
    |> Array.map (function | Zero -> "1"
                           | One -> "0")
    |> String.concat ""

let orderWeight(s: string): string =
    let weight (str: string) = str.ToCharArray() |> Array.sumBy (string >> Int32.Parse)
    if s = "" then ""
    else
        s.Split(' ')
        |> Array.mapi (fun index str -> (index, str))
        |> Array.groupBy (snd >> weight)
        |> Array.sortBy fst
        |> Array.map (snd >> Array.sortByDescending fst)
        |> Array.collect id
        |> Array.map snd
        |> String.concat " "