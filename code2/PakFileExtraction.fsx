open System.IO
open System.Diagnostics

let src = @"*"

let dest = @"*"

let pakFiles = seq {
    yield! Directory.EnumerateFiles(src, "*.pak")
    yield! seq {
        for dir in Directory.EnumerateDirectories(src) do
            yield! Directory.EnumerateFiles(dir)
    }
}

let extract fileName = 
    use proc = new Process();
    let startInfo = ProcessStartInfo();
    startInfo.FileName <- "UnrealPak.exe"
    startInfo.UseShellExecute <- false
    startInfo.RedirectStandardOutput <- true
    startInfo.CreateNoWindow <- true

    let fileInfo = fileName |> FileInfo
    let fileBaseName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length)
    let destDir = sprintf "%s\\%s" dest fileBaseName
    let arguments = sprintf "%s -Extract %s" fileName destDir
    printfn "%s" arguments
    startInfo.Arguments <- arguments
    
    proc.StartInfo <- startInfo

    proc.Start() |> ignore;
    while not proc.StandardOutput.EndOfStream do
        printfn "%s" (proc.StandardOutput.ReadLine())

    proc.WaitForExit()    

pakFiles
|> Seq.distinct
|> Seq.iter extract