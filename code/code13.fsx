let zeros num =
    let rec innerFn x times count =
        match x / (pown 5 times) with
        | 0 -> count
        | y -> innerFn x (times + 1) (count + y)
    innerFn num 1 0

let fact n =
     let rec innerFn x acc =
         match x with
         | 0 -> 1I
         | 1 -> acc
         | _ -> innerFn (x - 1) (acc * (bigint x))
     innerFn n 1I

type BoundType = S | E

let rec insertInterval intervals newInterval =
    let mark = function | [s; e] -> [(s, S); (e, E)] | _ -> failwith "Will not happen!"
    let markedIntervals = intervals |> List.map mark |> List.collect id
    let markedNewInterval = newInterval |> mark
    let sortedIntervals = (markedNewInterval @ markedIntervals) 
                          |> List.sortWith (fun (v1, t1) (v2, t2) -> if v1 < v2 then
                                                                        -1
                                                                     elif v1 = v2 then
                                                                        match t1, t2 with
                                                                        | S, E -> -1
                                                                        | _ -> 1
                                                                     else
                                                                        1)
    construct sortedIntervals None 0 []

and construct xs temp stackHeight ys =
        match xs with
        | [] -> ys
        | head :: tail -> match temp with
                          | None -> match head with
                                    | v, S -> construct tail (Some v) (stackHeight + 1) ys
                                    | _ -> failwith "Invalid!"
                          | Some s -> match head with
                                      | v, E -> match stackHeight with
                                                | 1 -> construct tail None 0 (ys @ [[s; v]])
                                                | _ -> construct tail temp (stackHeight - 1) ys
                                      | _, S -> construct tail temp (stackHeight + 1) ys

let alone xs =
    xs |> List.reduce (^^^)

type Case =
    | Letter of char
    | Question
    | Any

let rec validate (str: string) (exprStr: string) =
    let charList = str.ToCharArray() |> List.ofArray
    let expr = solveExpr exprStr
    let rec innerFn (chars: char list) (cases: Case list) =
        match chars, cases with
        | [], [] | [], [Any] -> true
        | cHead :: cTail, eHead :: eTail -> match eHead with
                                            | Question -> innerFn cTail eTail
                                            | Any -> innerFn cTail cases
                                            | Letter l -> if cHead = l then 
                                                            innerFn cTail eTail
                                                          else
                                                            false
        | _, _ -> false
    innerFn charList expr

and solveExpr (exprStr: string) =
    exprStr.ToCharArray()
    |> Array.map (fun x -> match x with | '?' -> Question | '*' -> Any | _ -> Letter x)
    |> List.ofArray
