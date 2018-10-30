type FooType =
    static member foo(x: int) = printfn "This is %d" x
    static member foo(x: string) = printfn "The string is %s" x

type FooBar = { o: obj } with
    static member bar (x: FooBar) =
        match x.o with
        | :? string as x -> FooType.foo x
        | :? int as x -> FooType.foo x
        | _ -> failwith "We should never, ever get here!"

type System.String with static member DoseFoo() = true
type System.Int32 with static member DoseFoo() = true

let inline barfoo (x: 't when 'a: (static member DoesFoo: unit -> bool)) =
    { o = x } |> FooBar.bar

barfoo 6
barfoo "elephants"
barfoo 6.0