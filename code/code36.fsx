let twoSum target (nums: int array) =
    [|
        for i in 0 .. nums.Length - 1 do
        for j in 0 .. nums.Length - 1 do
        if nums.[i] + nums.[j] = target then
            if i <> j then
                if i < j then
                    yield i
                    yield j
    |]

let findMedianSortedArrays (nums1: int array) (nums2: int array) =
    let totalCount = nums1.Length + nums2.Length
    let middleIndex = totalCount / 2
    let medianIndex = 
        if (nums1.Length + nums2.Length) % 2 = 0 then
            [ middleIndex - 1; middleIndex ]
        else
            [ middleIndex ]
    let rec inner idx1 idx2 midx res =
        match midx with
        | [] -> res |> List.average
        | head :: tail ->
            match idx1, idx2 with
            | Some x1, Some x2 ->
                let a, b = nums1.[x1], nums2.[x2]
                let newIdx1 = if x1 + 1 = nums1.Length then None else Some (x1 + 1)
                let newIdx2 = if x2 + 1 = nums2.Length then None else Some (x2 + 1)
                if a < b then
                    if x1 + x2 = head then
                        inner newIdx1 idx2 tail ((float a) :: res)
                    else
                        inner newIdx1 idx2 midx res
                else
                    if x1 + x2 = head then
                        inner idx1 newIdx2 tail ((float b) :: res)
                    else
                        inner idx1 newIdx2 midx res
            | None, Some x2 ->
                let b = nums2.[x2]
                let newIdx2 = if x2 + 1 = nums2.Length then None else Some (x2 + 1)
                let bottom = nums1.Length
                if bottom + x2 = head then
                    inner None newIdx2 tail ((float b) :: res)
                else
                    inner None newIdx2 midx res
            | Some x1, None ->
                let a = nums1.[x1]
                let newIdx1 = if x1 + 1 = nums1.Length then None else Some (x1 + 1)
                let bottom = nums2.Length
                if bottom + x1 = head then
                    inner newIdx1 None tail ((float a) :: res)
                else
                    inner newIdx1 None midx res
            | _ -> failwith "Cannot happen!"
    inner (Some 0) (Some 0) medianIndex []

open System.Text

let convert (text: string) rows =
    let indexedText = text.ToCharArray() |> Array.indexed
    let rec mapRowNumberToChar (arr: (int * char) array) up (res: (int * char) array) =
        match arr with
        | [| |] -> res
        | _ ->
            let idx, c = Array.head arr
            let rem = idx % (rows - 1)
            if rem <> 0 then
                if up then
                    mapRowNumberToChar (Array.tail arr) up (Array.append res [| (rem, c) |])
                else
                    mapRowNumberToChar (Array.tail arr) up (Array.append res [| (rows - rem - 1, c) |])
            else
                if up then
                    mapRowNumberToChar (Array.tail arr) (not up) (Array.append res [| (rows - 1, c) |])
                else
                    mapRowNumberToChar (Array.tail arr) (not up) (Array.append res [| (rem, c) |])
    let sb = StringBuilder()
    mapRowNumberToChar indexedText false [||]
    |> Array.groupBy fst
    |> Array.iter (fun (_, chars) -> chars |> Array.iter (snd >> sb.Append >> ignore))
    sb.ToString()

let reverse x =
    let s, v = sign x, abs x
    let rec inner value res =
        match value with
        | 0 -> res
        | _ -> let rem = value % 10
               let newValue = value / 10
               inner newValue (res * 10 + rem)
    s * (inner v 0)

[<AllowNullLiteral>]
type ListNode(_x) = 
    member val next: ListNode = null with get, set
    member val x: int = _x

// Merge k sorted Lists: Brute force
let mergeKLists (lists: ListNode array) =
    let head = ListNode(0)
    let curr = ref head
    let rec walk (l: ListNode) (res: ListNode array) =
        match isNull l.next with
        | true -> res
        | false -> walk (l.next) (Array.append res [| l |])
    let allNodes = lists |> Array.map (fun x -> walk x [||]) |> Array.collect id
    let sorted = allNodes |> Array.sortBy (fun x -> x.x)
    sorted
    |> Array.iter (fun x -> (!curr).next <- ListNode(x.x)
                            curr := (!curr).next)
    head.next

open System

let rotateString (a: string) (b: string) =
    let rec inner (rotated: string) count =
        match count with
        | 0 -> false
        | _ -> 
            let newRotatedString = [| yield! (rotated |> Seq.tail); yield rotated |> Seq.head |] |> String
            if newRotatedString = b then
                true
            else
                inner newRotatedString (count - 1)
    inner a (a.Length)

type System.String with
    member this.Contains(value: char) =
        let rec inner str res =
            match str with
            | "" -> res
            | _ -> 
                let head, tail = Seq.head str, Seq.tail str |> String.Concat 
                if head = value then
                    true
                else
                    inner tail res
        inner this false

let numJewelsInStones (j: string) (s: string) =
    s |> Seq.sumBy (fun x -> if j |> Seq.contains x then 1 else 0)

open System.Text.RegularExpressions

let (|C1|C2|C3|C4|C5|C6|NG|) (s: string) =
    if Regex.IsMatch(s, @"^\d+$") then C1
    elif Regex.IsMatch(s, @"^\d+\.$") then C2
    elif Regex.IsMatch(s, @"^\d+\.\d+$") then C3
    elif Regex.IsMatch(s, @"^\d+[e|E]\d+$") then C4
    elif Regex.IsMatch(s, @"^\d+\.[e|E]\d+$") then C5
    elif Regex.IsMatch(s, @"^\d+\.|d+[e|E]\d+$") then C6
    else NG

let isNumber (s: string) =
    let trimmed = s.Trim()
    match trimmed with
    | C1 | C2 | C3 | C4 | C5 | C6 -> true
    | NG -> false

let reverseWords (s: string) =
    s.Split(' ')
    |> Array.map (Seq.rev >> String.Concat)
    |> String.concat " "

let arrayPairSum nums =
    nums
    |> Array.sort
    |> Array.mapi (fun index num -> if index % 2 = 0 then num else 0)
    |> Array.sum

let hammingDistance x y =
    let res: int = (x ^^^ y)
    Convert.ToString(res, 2)
    |> Seq.sumBy (function | '1' -> 1 | _ -> 0)

type Board = int list

let listToOption lst =
    match lst with
    | [] -> None
    | a :: _ -> Some a

let ``null`` = function | [] -> true | _ :: _ -> false

let placeable n xs =
    [
        for i, x in List.indexed xs do
        if n = x || abs (n - x) = (i + 1) then
            yield (i + 1, x)
    ] |> ``null``

let concatMap f lists =
    List.collect f lists

let nQueens' n =
    let rec go k bs =
        match k with
            | 0 -> [bs]
            | _ -> 
                let f = go (k - 1)
                concatMap f [ for x in 1 .. n do if placeable x bs then yield (x :: bs) ]
    go n []

let nQueensFirst = nQueens' >> listToOption

let nQueensCount = nQueens' >> List.length

let printBoard board =
    let queensCount = board |> List.length
    let head = List.fold (fun state x -> state + (sprintf "%d " x)) "\\ " [1 .. queensCount]
    let lines = 
        board 
        |> List.indexed
        |> List.map (fun (index, x) -> 
            List.fold (fun state y -> if y = x then state + "Q " else state + "X ") (sprintf "%d " (index + 1)) [1 .. queensCount])
    printfn "%s" head
    lines |> List.iter (printfn "%s")