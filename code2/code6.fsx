open System.Text.RegularExpressions
open System.Collections.Generic

let input = "SectionPlane.Add(FVector(0, 0, 0));
-	SectionPlane.Add(FVector(0, 0, 12));
-	SectionPlane.Add(FVector(1, 0, 12));
-	SectionPlane.Add(FVector(1, 0, 10));
-	SectionPlane.Add(FVector(2, 0, 10));
-	SectionPlane.Add(FVector(2, 0, 0));"

let regex = @"SectionPlane\.Add\(FVector\(([\d\-\.]+), 0, ([\d\-\.]+)\)\);" |> Regex

let matches = regex.Matches(input)

let strings = List<string>()

for mat in matches do
    let x = mat.Groups.[1]
    let z = mat.Groups.[2]
    strings.Add(sprintf "%s,%s" x.Value z.Value)

strings |> String.concat ";"