open System.Collections.Generic

let input = "4 10 4 1 8 4 9 14 5 1 14 15 0 15 3 5"

let stringToInt = string >> int

let memory = input.Split([| ' ' |]) |> Array.map stringToInt

let getNext (state: int array) =
    let length = state.Length
    let temp = Array.copy state
    let nextState =
        let max = Array.max temp
        let posOfMax = Array.findIndex (fun blocks -> blocks = max) temp
        temp.[posOfMax] <- 0
        let mutable blocksToDis = max
        let mutable indexToDis = posOfMax + 1
        while blocksToDis <> 0 do
            if indexToDis >= length then
                indexToDis <- 0
            else
                blocksToDis <- blocksToDis - 1
                temp.[indexToDis] <- temp.[indexToDis] + 1
                indexToDis <- indexToDis + 1
        temp
    nextState

let getCycleCount (state: int array) =
    let states = new List<int array>()
    let mutable cycleCount = 0
    let mutable s = state
    while not (states |> Seq.contains s) do
        states.Add(s)
        s <- getNext s
        cycleCount <- cycleCount + 1
    cycleCount

let answer = getCycleCount memory