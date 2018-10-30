module DSL =
    type Computation<'T> = C of (unit -> 'T)

    [<System.Diagnostics.DebuggerHidden>]
    let apply (C f) = f ()

    let inline bind (C f) k = C (fun () -> let (C f2) = k (f ()) in f2 ())

    let inline ret x = C (fun () -> x)

    let inline add c1 c2 = bind c1 (fun v1 -> bind c2 (fun v2 -> ret (v1 + v2)))

    let inline stack msg c1 =
        bind (ret ()) (fun () ->
            System.Console.WriteLine("----{0}-----\n{1}----------", msg, System.Diagnostics.StackTrace(true).ToString())
            c1)

    let run c = apply c

open DSL

let f1() = stack "f1" (ret 6)
let f2() = stack "f2" (ret 6)
let f3() = stack "f3" (add (f1()) (f2()))

f3() |> run