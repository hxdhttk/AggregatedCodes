open System
open FSharp.Quotations.Patterns
open FSharp.Quotations

let replaceTypeOnCall (t: Type) = function
    | Call(o, mi, args) ->
        let newMi = mi.GetGenericMethodDefinition().MakeGenericMethod([| t |])
        match o with
        | Some o -> Expr.Call(o, newMi, args)
        | None -> Expr.Call(newMi, args)
    | _ -> failwith "Called on a non-call Expr"

let makeArrayExpr (length: int) (t: Type) =
    <@@ Array.zeroCreate<obj> length @@>
    |> replaceTypeOnCall t