type INum = abstract member Num: int
let inline makeNum f = { new INum with member __.Num = f() }

type TestClass(n) = 
    let addOne x = x + 1
    let inner = makeNum (fun () -> addOne n)
    member __.GetNum() = inner.Num

let t = TestClass(42)
t.GetNum() |> printfn "%d"