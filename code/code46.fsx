open System.Linq

module TrivialStream =
    type Receiver<'T> = 'T -> unit
    type Stream<'T> = Receiver<'T> -> unit

    module Details =
        module Loop =
            let rec range s e r i = if i <= e then r i; range s e r (i + s)

    open Details

    let inline range b s e : Stream<int> =
        fun r -> Loop.range s e r b

    let inline filter (f: 'T -> bool) (s: Stream<'T>) : Stream<'T> =
        fun r -> s (fun v -> if f v then (r v))

    let inline map (m: 'T -> 'U) (s: Stream<'T>) : Stream<'U> =
        fun r -> s (fun v -> (r (m v)))

    let inline sum (s: Stream<'T>) : 'T =
        let mutable ss = LanguagePrimitives.GenericZero
        s (fun v -> ss <- ss + v)
        ss

let trivialTest n =
    TrivialStream.range 0 1 n
    |> TrivialStream.map (int64)
    |> TrivialStream.filter (fun v -> v &&& 1L = 0L)
    |> TrivialStream.map (fun v -> v + 1L)
    |> TrivialStream.sum

let linqTest n =
    Enumerable
        .Range(0, n + 1)
        .Select(int64)
        .Where(fun v -> v &&& 1L = 0L)
        .Select(fun v -> v + 1L)
        .Sum()

#time "on"

trivialTest 100000000

linqTest 100000000
