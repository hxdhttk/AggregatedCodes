#nowarn "9"

open FSharp.NativeInterop

let encrypt (s: string) =
    let chars = s.ToCharArray()
    match chars.Length % 2 = 1, chars.Length / 2 with
    | true, x -> let oddChars = NativePtr.stackalloc<char> (x + 1)
                 let evenChars = NativePtr.stackalloc<char> x
                 chars
                 |> Seq.chunkBySize 2
                 |> Seq.iteri (fun i a -> match a with
                                          | [| f; s |] -> NativePtr.set oddChars i f
                                                          NativePtr.set evenChars i s
                                          | [| last |] -> NativePtr.set oddChars i last
                                          | _ -> failwith "Will not happen!")
                 [ for i in 0 .. x -> NativePtr.get oddChars i ] @ [ for i in 0 .. x - 1 -> NativePtr.get evenChars i ]
    | false, x -> let oddChars = NativePtr.stackalloc<char> x
                  let evenChars = NativePtr.stackalloc<char> x
                  chars
                  |> Seq.chunkBySize 2
                  |> Seq.iteri (fun i a -> let f, s = a.[0], a.[1]
                                           NativePtr.set oddChars i f
                                           NativePtr.set evenChars i s)
                  [ for i in 0 .. x - 1 -> NativePtr.get oddChars i ] @ [ for i in 0 .. x - 1 -> NativePtr.get evenChars i ]
