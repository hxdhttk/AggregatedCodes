type ImAnInterface = interface end

type T1 = A | B of int interface ImAnInterface

type T2 = B of int | C | D of int interface ImAnInterface

let f (t: ImAnInterface) =
    match t with
    | :? T1 as t -> 
        match t with
        | T1.B _ | _ -> ()
    | :? T2 as t ->
        match t with
        | T2.B _ | _ -> ()
    | _  -> ()

let (|T1'|_|) (t: ImAnInterface) =
    match t with
    | :? T1 -> Some (t :?> T1) 
    | _ -> None

let (|T2'|_|) (t: ImAnInterface) =
    match t with
    | :? T2 -> Some (t :?> T2) 
    | _ -> None

let g (t: ImAnInterface) =
    match t with
    | T1' (T1.B _)
    | T2' (T2.B _) -> ()
    | _ -> ()
