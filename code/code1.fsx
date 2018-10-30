open System
open System.Threading
open System.Runtime.InteropServices

module Win32Methods =

    type CtrlTypes =
        | CTRL_C_EVENT = 0
        | CTRL_BREAK_EVENT = 1
        | CTRL_CLOSE_EVENT = 2  
        | CTRL_LOGOFF_EVENT = 5
        | CTRL_SHUTDOWN_EVENT = 6

    type HandlerRoutine = delegate of CtrlTypes -> bool

    [<DllImport("Kernel32")>]
    extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add)

let handler (ctrlType: Win32Methods.CtrlTypes) =
    let message = match ctrlType with
                  | Win32Methods.CtrlTypes.CTRL_C_EVENT ->
                    "A CTRL_C_EVENT was raised by the user."
                  | Win32Methods.CtrlTypes.CTRL_BREAK_EVENT ->
                    "A CTRL_BREAK_EVENT was raised by the user."
                  | Win32Methods.CtrlTypes.CTRL_CLOSE_EVENT ->
                    "A CTRL_CLOSE_EVENT was raised by the user."
                  | Win32Methods.CtrlTypes.CTRL_LOGOFF_EVENT ->
                    "A CTRL_LOGOFF_EVENT was raised by the user."
                  | Win32Methods.CtrlTypes.CTRL_SHUTDOWN_EVENT ->
                    "A CTRL_SHUTDOWN_EVENT was raised by the user."
                  | _ -> "This message should never be seen!"
    printfn "%s" message
    true

let main () =
    let hr = new Win32Methods.HandlerRoutine(handler)
    Win32Methods.SetConsoleCtrlHandler(hr, true) |> ignore

    printfn "%s" "Waiting 30 seconds for console ctrl events..."

    GC.Collect()
    GC.WaitForPendingFinalizers()
    GC.Collect()

    Thread.Sleep(30000)

    printfn "%s" "Finished!"

    GC.KeepAlive(hr)
    Console.Read() |> ignore