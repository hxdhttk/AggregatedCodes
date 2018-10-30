open System
open System.Collections.Generic

type TrieNode =
    | Node of IDictionary<char, TrieNode> * bool
    | Eow

let rec generateTrieNode words =
    let hasEowNode = Seq.exists (List.isEmpty) words
    if Seq.isEmpty words then
        Eow
    else
        let nextWordFragments =
            words
            |> Seq.filter (fun word -> word <> [])
            |> Seq.groupBy (Seq.head)
            |> Seq.map ((fun (firstLetter, words) -> (firstLetter, words |> Seq.map List.tail)) >>
                        (fun (firstLetter, rest) -> (firstLetter, generateTrieNode rest)))
            |> Seq.fold (fun acc (letter, trieNode) ->
                (Char.ToUpper(letter), trieNode) :: (Char.ToLower(letter), trieNode) :: acc) []
        Node (dict nextWordFragments, hasEowNode)

let isWord word tree =
    let letters = word |> Seq.toList
    let rec isWord currentNode letters =
        match letters, currentNode with
        | [], Eow | [], Node(_, true) -> true
        | nextLetter :: rest, Node (childNodes, _) ->
            let hasNode, childNode = childNodes.TryGetValue(nextLetter)
            if hasNode then
                isWord childNode rest
            else
                false
        | _ -> false
    isWord tree letters