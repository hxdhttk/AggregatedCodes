open System.Diagnostics

let shell (cmd: string) =
    let exeName = cmd.Split(' ').[0]
    let arguments = String.concat " " (cmd.Split(' ').[1..])

    use proc = new Process()
    let startInfo = ProcessStartInfo()
    startInfo.FileName <- exeName
    startInfo.UseShellExecute <- false
    startInfo.RedirectStandardOutput <- true
    startInfo.CreateNoWindow <- true
    startInfo.Arguments <- arguments

    proc.StartInfo <- startInfo

    proc.Start() |> ignore
    while not proc.StandardOutput.EndOfStream do
        printfn "%s" (proc.StandardOutput.ReadLine())

    proc.WaitForExit()
    proc.ExitCode

