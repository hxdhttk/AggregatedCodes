let rec hanoi n a b c =
    match n with
    | 1 -> printfn "%c -> %c" a c
    | _ -> hanoi (n - 1) a c b
           hanoi 1 a b c
           hanoi (n - 1) b a c

type Node<'a> = { Left: Node<'a> option; Value: 'a; Right: Node<'a> option }

let rec getNodes node =
    match node with
    | None -> 0
    | Some n -> (getNodes (n.Left)) + (getNodes (n.Right)) + 1

let asciiOF (str: string) =
    str
    |> Seq.map int64
    |> Seq.reduce (Checked.(*))

let containAllRots (strng: string) (a: string list) =
    match strng with
    | "" -> true
    | _ -> strng :: [ for i in 1 .. (strng.Length - 1) -> strng.[i..] + strng.[..(i - 1)] ]
           |> List.forall (fun x -> a |> List.contains x)