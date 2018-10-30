let input = 361527

let findSE x =
    let rec a n = match n with
                  | 0 -> 1
                  | _ -> (8 * n) + (a (n - 1))
    let rec find n m =
        match n with
        | n when (a n <= m) && (a (n + 1) > m) -> n
        | _ -> find (n + 1) m
    find 0 x