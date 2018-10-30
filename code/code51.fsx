type Incrementor(delta) =
    member this.Increment(i: byref<int>) = i <- i + delta

let i = 1
let incrementor = Incrementor(5)
incrementor.Increment(ref i)

printfn "%d" i