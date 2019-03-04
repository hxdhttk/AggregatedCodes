open System.IO
open System.Diagnostics

type FileInfo with
    member this.BaseName =
        this.Name.Substring(0, this.Name.Length - this.Extension.Length)

let dest = @"*"

let destTemplate = @"*"

let src = @"*"

let functionDirs =
    seq {
        yield destTemplate
        yield! Directory.EnumerateDirectories(destTemplate)
    }

for functionDir in functionDirs do
    let dirInfo = functionDir |> DirectoryInfo

    let dirName = dirInfo.Name;
    let productsDest = 
        if dirName = "Default" then dest
        else sprintf "%s\\%s" dest dirName
    let pakFiles = Directory.EnumerateFiles(functionDir)
    for pakFile in pakFiles do
        let pakFileInfo = pakFile |> FileInfo
        let pakFileBaseName = pakFileInfo.BaseName
        let packagingDir = sprintf "\"%s\\%s\\*\"" src pakFileBaseName
        let destFileName = sprintf "\"%s\\%s.pak\"" productsDest pakFileBaseName

        use proc = new Process();
        let startInfo = ProcessStartInfo();
        startInfo.FileName <- "UnrealPak.exe"
        startInfo.UseShellExecute <- false
        startInfo.RedirectStandardOutput <- true
        startInfo.CreateNoWindow <- true

        let arguments = sprintf "%s %s" destFileName packagingDir
        printfn "%s" arguments
        startInfo.Arguments <- arguments
    
        proc.StartInfo <- startInfo

        proc.Start() |> ignore;
        while not proc.StandardOutput.EndOfStream do
            printfn "%s" (proc.StandardOutput.ReadLine())

        proc.WaitForExit()       