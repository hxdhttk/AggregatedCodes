open System.Text.RegularExpressions

[<AutoOpen>]
module LanguageDefs =
    let [<Literal>] ``en-us`` = "en-us"
    let [<Literal>] ``zh-cn`` = "zh-cn"

let (|Language|_|) identifier url =
    let baseRegexString: Printf.StringFormat<string -> string> = @"%s"
    let regex = sprintf baseRegexString identifier |> Regex
    if regex.IsMatch(url) then Some url else None

let recognizeLanguage url =
    match url with
    | Language ``en-us`` x -> sprintf "%s is a page in US English." x
    | Language ``zh-cn`` x -> sprintf "%s is a page in Chinese." x
    | _ -> sprintf "%s is a page in unknown language." url

[ "https://docs.microsoft.com/zh-cn/"
  "https://docs.microsoft.com/en-us/"
  "https://docs.microsoft.com/xx-xx/" ]
|> List.map recognizeLanguage
|> List.iter (printfn "%s")
