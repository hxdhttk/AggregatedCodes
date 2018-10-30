let fizzBuzz n =
    [|
        for i in 1 .. n do
            if i % 15 = 0 then yield "fizz buzz"
            elif i % 3 = 0 then yield "fizz"
            elif i % 5 = 0 then yield "buzz"
            else yield string i
    |]

let isArthmeticSlice slice =
    let rec inner slice diff =
        match slice with
        | [f; s] ->
            s - f = diff
        | f :: s :: tail ->
            if s - f = diff then
                inner (s :: tail) diff
            else
                false
        | _ -> false
    match slice with
    | f :: s :: tail -> 
        inner (s :: tail) (s - f)
    | _ -> false

let listSlider lst p q =
    let n = lst |> List.length
    if (p >= 0) && (q < n) && (p < q) &&
       (p + 1 < q) then
        lst
        |> List.skip p
        |> List.take (q - p + 1)
    else
        []

let numberOfArithmeticSlices lst =
    let n = lst |> List.length
    [
        for sliceLength in 3 .. n do
            for startIdx in 0 .. (n - sliceLength) do
                let slice = listSlider lst startIdx (startIdx + sliceLength - 1)
                if isArthmeticSlice slice then
                    yield slice
    ]
    |> List.length

type Tree<'a> =
    | Empty
    | Node of value: 'a * left: Tree<'a> * right: Tree<'a>

let traverseDepth tree depth =
    let rec inner tree depthIdx res =
        match tree with
        | Empty -> res
        | Node(value, left, right) ->
            if depthIdx = 0 then
                value :: res
            else
                (inner left (depthIdx - 1) res) @ (inner right (depthIdx - 1) res)
    inner tree depth []

let traverseAllDepths tree maxDepth =
    let rec inner tree depth res =
        match tree with
        | Empty -> res
        | Node(value, left, right) ->
            if depth = maxDepth then
                (depth, value) :: res
            else
                (depth, value) :: ((inner left (depth + 1) res) @ (inner right (depth + 1) res))
    inner tree 0 []
    |> List.groupBy fst
    |> List.map (fun (key, values) ->
        let lst = [ for _, value in values -> value ]
        key, lst)

let largestValues root =
    let rec inner root depthIdx res =
        match traverseDepth root depthIdx with
        | [] -> res
        | values -> inner root (depthIdx + 1) (res @ [ values |> List.max ])
    inner root 0 []

let treeA =
    Node(1, Node(3, Node(5, Empty, Empty), Node(3, Empty, Empty)), Node(2, Empty, Node(9, Empty, Empty)))

largestValues treeA