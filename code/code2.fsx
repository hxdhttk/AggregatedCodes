open System

type GCCollClass() =
    static member private maxGarbage = 1000
    member  __.MakeSomeGarbage () =
        let vts = Array.init 1 (fun _ -> Version())
        for i in 0 .. GCCollClass.maxGarbage do
            vts.[0] <- Version()

let main () =

    let gccc = GCCollClass()
    printfn "The highest generation is %d" GC.MaxGeneration
    
    gccc.MakeSomeGarbage()
    printfn "Generation: %d" (GC.GetGeneration(gccc))
    printfn "Total Memory: %d" (GC.GetTotalMemory(false))

    GC.Collect(0)
    printfn "Generation: %d" (GC.GetGeneration(gccc))
    printfn "Total Memory: %d" (GC.GetTotalMemory(false))

    GC.Collect(2)
    printfn "Generation: %d" (GC.GetGeneration(gccc))
    printfn "Total Memory: %d" (GC.GetTotalMemory(false))

    Console.Read() |> ignore