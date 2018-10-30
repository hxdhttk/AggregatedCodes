module A =
    let getAString() = "Www"

    let calculate a b = a + b

module B =
    let getAString() = "Hahaha"

    let calculate a b = a * b

module App =

    module R = A
    //module R = B

    let test a b =
        printfn "%s" (R.getAString())
        printfn "%d" (R.calculate a b)

open System
open System.Collections.Generic

let printTuples (tuples: IEnumerable<Tuple<int, string>>) =
    tuples
    |> Seq.iter (fun (i, s) -> printfn "%d %s" i s)

let tuples = seq { yield Tuple.Create(10, "ten")}

printTuples tuples

[<AllowNullLiteral>]
type ListNode(_x) = 
    member val next: ListNode = null with get, set
    member val x: int = _x

let addTwoNumbers (l1: ListNode) (l2: ListNode) =
    let rec inner (x1: ListNode) (x2: ListNode) (carry: bool) (res: ListNode) (ret: ListNode)=
        match x1, x2, carry with
        | _, _, true when isNull x1 && isNull x2 ->
            let newNode = ListNode(1)
            if isNull res then
                newNode
            else
                res.next <- newNode
                ret
        | _, _, false when isNull x1 && isNull x2 ->
            ret
        | v1, v2, true -> 
            let newX = v1.x + v2.x + 1
            if newX > 9 then
                let newNode = ListNode(newX - 10)
                if isNull res then
                    inner v1.next v2.next true newNode newNode
                else
                    res.next <- newNode
                    inner v1.next v2.next true res.next ret
            else
                let newNode = ListNode(newX)
                if isNull res then
                    inner v1.next v2.next false newNode newNode
                else
                    res.next <- newNode
                    inner v1.next v2.next false res.next ret
        | v1, v2, false -> 
            let newX = v1.x + v2.x
            if newX > 9 then
                let newNode = ListNode(newX - 10)
                if isNull res then
                    inner v1.next v2.next true newNode newNode
                else
                    res.next <- newNode
                    inner v1.next v2.next true res.next ret
            else
                let newNode = ListNode(newX)
                if isNull res then
                    inner v1.next v2.next false newNode newNode
                else
                    res.next <- newNode
                    inner v1.next v2.next false res.next ret
    let (res: ListNode), (ret: ListNode) = null, null
    inner l1 l2 false res ret

let l1 = ListNode(9)
l1.next <- ListNode(9)
l1.next.next <- ListNode(9)

let l2 = ListNode(9)
l2.next <- ListNode(0)
l2.next.next <- ListNode(9)

let res = addTwoNumbers l1 l2
printfn "%d -> %d -> %d -> %d" (res.x) (res.next.x) (res.next.next.x) (res.next.next.next.x)