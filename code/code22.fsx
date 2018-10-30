#light "off"
#nowarn "62";;

#r "System.ValueTuple.dll";;

type fstsnd = class
    static member Fst (struct (x, _)) = x
    static member Fst ((x, _)) = x
    static member Snd (struct (_, y)) = y
    static member Snd ((_, y)) = y

    static member inline _invoke_fst (x: ^a) =
        let inline call (arg, _: ^b) =
            ((^a or ^b) : (static member Fst : _ -> _) arg) in
        call (x, Unchecked.defaultof<fstsnd>)

    static member inline _invoke_snd (x: ^a) =
        let inline call (arg, _: ^b) =
            ((^a or ^b) : (static member Snd : _ -> _) arg) in
        call (x, Unchecked.defaultof<fstsnd>)
end

let inline xfst x = fstsnd._invoke_fst x
let inline xsnd x = fstsnd._invoke_snd x

let x = xfst (struct (1, 2))
let y = xfst (3, 4)
let xx = xsnd (struct (5, 6))
let yy = xsnd (7, 8)

do printfn "%d %d %d %d" x y xx yy