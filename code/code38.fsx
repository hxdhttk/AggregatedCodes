let optimalDivision nums =
    match nums with
    | [| a |] -> sprintf "%d" a
    | [| a; b |] -> sprintf "%d/%d" a b
    | _ -> 
        nums
        |> Array.skip 2
        |> Array.fold (fun state x -> sprintf "%s/%d" state x) (sprintf "%d/(%d" nums.[0] nums.[1])
        |> sprintf "%s)"

let evaluateAndMakeTuple f =
    let memorisedValue = f()
    fun s -> (memorisedValue, s)

let partialAppliedFunc s = evaluateAndMakeTuple (fun () -> 2) s

let result1 = partialAppliedFunc "2"

let result2 = partialAppliedFunc 3

open System

let validChars = [| yield! [| '0' .. '9' |]; yield '?' |]

let (|ValidChar|_|) c =
    if validChars |> Array.contains c then Some c
    else None

let (|Add|Sub|Mul|NG|) c =
    match c with
    | '+' -> Add c
    | '-' -> Sub c
    | '*' -> Mul c
    | _ -> NG

let (|Op|_|) c =
    match c with
    | Add _ | Sub _ | Mul _ -> Some c
    | _ -> None

let rec solveExpression (expr: string) =
    let charList = expr |> List.ofSeq
    match parse charList "" [] with
    | [lhs; op; rhs; res] ->
        let exsitedDigits = 
            [| yield! lhs; yield! rhs; yield! res |]
            |> Set.ofArray
            |> Set.remove '?'
            |> Set.map (fun x -> int x - int '0')
        let validDigits = [| 0 .. 9 |] |> Array.filter (fun x -> Set.contains x exsitedDigits |> not)
        validDigits
        |> Array.tryFind (checkMissingDigit lhs op rhs res)
        |> Option.defaultValue -1
    | _ -> -1

and parse expr temp res =
    match expr with
    | [] -> res @ [temp]
    | (ValidChar c) :: (Op op) :: tail-> parse tail "" (res @ [sprintf "%s%c" temp c; string op])
    | (ValidChar c) :: '=' :: tail -> parse tail "" (res @ [sprintf "%s%c" temp c])
    | '=' :: tail -> parse tail "" (res @ [temp])
    | (Sub op) :: (ValidChar c) :: tail -> parse tail (sprintf "%s%c%c" temp op c) res
    | (ValidChar c) :: tail -> parse tail (sprintf "%s%c" temp c) res
    | _ -> failwith "Bad input format!"

and eval lhs op rhs =
    match op with
    | Add _ -> lhs + rhs
    | Sub _ -> lhs - rhs
    | Mul _ -> lhs * rhs
    | NG -> failwithf "Unkonwn op: %c!" op

and checkMissingDigit (lhs: string) (op: string) (rhs: string) (res: string) missingDigit =
    let newLhs = (lhs.Replace("?", string missingDigit))
    let newRhs = (rhs.Replace("?", string missingDigit))
    let newRes = (res.Replace("?", string missingDigit))
    if [| newLhs; newRhs; newRes |] |> Array.exists (fun x -> x.IndexOf('0') = 0) then
        false
    else
        let newLhsValue = Convert.ToInt32(newLhs)
        let newRhsValue = Convert.ToInt32(newRhs)
        let newResValue = Convert.ToInt32(newRes)
        eval newLhsValue (char op) newRhsValue = newResValue

printfn "%d" (solveExpression "1+1=?")
printfn "%d" (solveExpression "123*45?=5?088")
printfn "%d" (solveExpression "-5?*-1=5?")
printfn "%d" (solveExpression "19--45=5?")
printfn "%d" (solveExpression "??*??=302?")
printfn "%d" (solveExpression "?*11=??")
printfn "%d" (solveExpression "??*1=??")
