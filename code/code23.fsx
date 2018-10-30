open FSharp.Quotations

let lambda (f: Expr<'T> -> Expr<'R>) : Expr<'T -> 'R> =
    let var = Var("__temp__", typeof<'T>)
    Expr.Cast<_>(Expr.Lambda(var, f (Expr.Cast<_>(Expr.Var var))))

let fix: (Expr<'T -> 'R> -> Expr<'T -> 'R>) -> Expr<'T -> 'R> = fun f ->
    <@ fun x -> let rec loop x = (% lambda (f)) loop x in loop x @>

let rec ack (m: int) : Expr<int -> int> =
    fix (fun f -> lambda (fun (n: Expr<int>) ->
        if m = 0 then <@ %n + 1 @>
        else <@ if %n = 0 then (% ack (m - 1)) 1
                else (% ack (m - 1)) ((%f) (%n - 1)) @>))

let clone (e : 'a) =
    let bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
    use m = new System.IO.MemoryStream()
    bf.Serialize(m, e)
    m.Position <- 0L
    bf.Deserialize m :?> 'a

type Fmap () =
    static member (?) (_: Fmap, m: _ option) = fun f -> Option.map f m
    static member (?) (_: Fmap, m: _ list) = fun f -> List.map f m
let inline (<%>) f x = Fmap() ? (x) f

type Ap() = 
  static member (?) (_:Ap , mf:_ option) = fun (m:_ option) -> 
    match mf , m with Some f , Some x -> Some (f x) | _ -> None
  static member (?) (_:Ap , mf:_ list)   = fun (m:_ list) -> 
    [ for f in mf do for x in m -> f x]
let inline (<*>) mf m = Ap() ? (mf) m

(+) <%> ["a"; "b"] <*> ["x"; "y"; "z"] |> printfn "%A"

