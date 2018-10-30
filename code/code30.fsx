type T = T with
    static member ($) (T, _: int) = (+)
    static member ($) (T, _: decimal) = (+)

let inline sum (i: 'a) (x: 'a) : 'r = (T $ Unchecked.defaultof<'r>) i x

type T with
    static member inline ($) (T, _: 't -> 'rest) = fun (a: 't) -> (+) a >> sum

let x: int = sum 2 3
let y: int = sum 2 3 4
let z: int = sum 2 3 4 5
let d: decimal = sum 2M 3M 4M

let inline op_BarQmark f y = f y
let test =
    let add x = x + 1
    add |? 12

let inline op_Dollar f y = f y
let test' =
    let add x = seq { yield x + 1}
    add $ 12