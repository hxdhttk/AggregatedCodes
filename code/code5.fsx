[<AutoOpen>]
module TimerExtensions =
    type System.Timers.Timer with
        static member StartWithDisposable interval handler =
            let timer = new System.Timers.Timer(interval)
            do timer.Elapsed.Add handler
            timer.Start()

            { new System.IDisposable with
                member disp.Dispose() =
                    do timer.Stop()
                    do printfn "Timer stopped"}

let testTimerWithDisposable =
    let handler = (fun _ -> printfn "elapsed")
    use timer = System.Timers.Timer.StartWithDisposable 100.0 handler
    System.Threading.Thread.Sleep 500