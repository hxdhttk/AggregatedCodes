type Memoize<'a, 'b when 'a : comparison>(f: ('a -> 'b)) =
    let mutable cache = Map.empty<'a, 'b>

    member __.Call(x) =
        let result = cache |> Map.tryFind x
        match result with
        | None -> let y = f x
                  cache <- Map.add x y cache
                  y
        | Some y -> y

let append (a:string) (b: string) =
    a + b

let appendTuple (a, b) = append a b

let appendMemoize = Memoize(appendTuple)

[ for i in 'A' .. 'Z' do
    for j in 'A' .. 'Z' ->
        (string i, string j) ]
|> List.map appendMemoize.Call
|> List.iter (printf "%A ")