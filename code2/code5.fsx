type IdParameter = 
    | IntParam of int
    | LongParam of int64
    | LongArrParam of int64[]

let convertIdParam (param: obj) =
    match param with
    | :? int -> IntParam (param :?> int)
    | :? int64 -> LongParam (param :?> int64)
    | :? (int64[]) -> LongArrParam (param :?> int64[])
    | _ -> failwithf "Unknown id type: %s" (param.GetType().Name)

let parseId id =
    match convertIdParam id with
    | IntParam value -> printfn "Get an id with int type: %d" value
    | LongParam value -> printfn "Get an id with int64 type: %d" value
    | LongArrParam value -> printfn "Get an id with int64[] type: %A" value
