open System.IO

let dir = @"*"

let houseDirs = Directory.EnumerateDirectories(dir)

let dest = @"*"

for houseDir in houseDirs do
    let houseDirInfo = DirectoryInfo(houseDir)
    let jpgFileOpt = houseDirInfo.GetFiles("*.jpg") |> Array.tryHead
    match jpgFileOpt with
    | Some jpgFile ->
        let lastIndexOfSlash = houseDir.LastIndexOf("\\");
        let jpgFilename = houseDir.Substring(lastIndexOfSlash)
        let path = sprintf "%s\\%s.jpg" dest jpgFilename
        printfn "%s" path
        jpgFile.CopyTo(path) |> ignore
    | None -> ()
