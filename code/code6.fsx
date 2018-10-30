type Color = R | B
type Tree<'a, 'b when 'a: comparison> =
    | E
    | T of Color *
           Tree<'a, 'b> *
           key: 'a *
           elem: 'b *
           Tree<'a, 'b>

let rec contains x = function
    | E -> false
    | (T (_, left, key, _, right)) ->
        if x < key then contains x left
        else if x > key then contains x right
        else true

let balance color left key elem right =
    match color, left, key, elem, right with
    | (B, T(R, T(R, a, x, xe, b), y, ye, c), z, ze, d) -> T(R, T(B, a, x, xe, b), y, ye, T(B, c, z, ze, d))
    | (B, T(R, a, x, xe, T(R, b, y, ye, c)), z, ze, d) -> T(R, T(B, a, x, xe, b), y, ye, T(B, c, z, ze, d))
    | (B, a, x, xe, T(R, T(R, b, y, ye, c), z, ze, d)) -> T(R, T(B, a, x, xe, b), y, ye, T(B, c, z, ze, d))
    | (B, a, x, xe, T(R, b, y, ye, T(R, c, z, ze, d))) -> T(R, T(B, a, x, xe, b), y, ye, T(B, c, z, ze, d))
    | _ -> T(color, left, key, elem, right)

let insert x e s =
    let rec ins = function
        | E -> T(R, E, x, e, E)
        | T(color, a, y, ye, b) ->
            if x < y then balance color (ins a) y ye b
            else if x > y then balance color a y ye (ins b)
            else T(color, a, x, e, b)
    let (T(_, a, y, ye, b)) = ins s
    T(B, a, y, ye, b)

let empty = E