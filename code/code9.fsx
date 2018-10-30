let (|EndIes|EndEs|EndS|PRaw|) (str: string) = 
    if str.EndsWith("ies") then EndIes(str)
    elif str.EndsWith("es") then EndEs(str)
    elif str.EndsWith("s") then EndS(str)
    else PRaw(str)

let (|EndVY|EndCY|EndO|SRaw|) (str: string) =
    if str.EndsWith("y") then
        let vowels = "aeiou"
        let charAtSecondLast = str.[str.Length - 2] |> string
        if vowels.Contains charAtSecondLast then
            EndVY(str)
        else
            EndCY(str)
    elif str.EndsWith("o") then EndO(str)
    else SRaw(str)

let getSingleOrPlural input =
    let length = String.length input
    let lowerCase = input.ToLower()
    match lowerCase.EndsWith("s") with
    | true -> match lowerCase with
              | EndIes str -> str.Substring(0, length - 3) + "y"
              | EndEs str -> str.Substring(0, length - 2)
              | EndS str -> str.Substring(0, length - 1)
              | PRaw str -> str
    | false -> match lowerCase with
               | EndVY str -> str + "s"
               | EndCY str -> str.Substring(0, length - 1) + "ies"
               | EndO str -> str + "es"
               | SRaw str -> str + "s"