open System

let inline f s x = match x with true, value -> Ok value | _ -> Error (sprintf "Not a valid %s" s)

type TryParse = TryParse with
    static member ($)(_: decimal, TryParse) = Decimal.TryParse >> f "decimal" : _ -> Result<decimal, string>
    static member ($)(_: float, TryParse) = Double.TryParse >> f "float" : _ -> Result<float, string>
    static member ($)(_: int, TryParse) = Int32.TryParse >> f "int" : _ -> Result<int, string>

let inline tryParse (x: string) : Result<'t, string> = (Unchecked.defaultof<'t> $ TryParse) x

let (|Between|_|) a b x =
    let m = min a b
    let n = max a b
    if x > m && x < n then
        Some Between
    else
        None

type T = T with
    static member inline ($) (T, _: string) : _ -> _ -> ^b = fun (path: string) (fn: string -> ^b) -> fn ""
    static member inline ($) (T, _: int) : _ -> _ -> ^b = fun (path: string) (fn: int -> ^b) -> fn 0
    static member inline ($) (T, _: float) : _ -> _ -> ^b = fun (path: string) (fn: float -> ^b) -> fn 0.
    static member inline ($) (T, _: bool) : _ -> _ -> ^b = fun (path: string) (fn: bool -> ^b) -> fn true

let inline parser (fmt: PrintfFormat< ^a -> ^b, _, _, ^b >) (fn: ^a -> ^b) (v: string) = (T $ Unchecked.defaultof< ^a >) v fn

let inline patternTest (fmt: PrintfFormat< ^a -> Action< ^T >, _, _, Action< ^T > >) (fn: ^a -> Action< ^T >) v : Action< ^T > = parser fmt fn v

let parseFn1 = parser "adfadf%i" (fun v -> printfn "%i" v; Unchecked.defaultof<int>)
let parseFn2 = parser "adf%s245" (fun v -> printfn "%s" v; Unchecked.defaultof<string>)