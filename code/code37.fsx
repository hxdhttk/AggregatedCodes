let rec removeFirst (list: List<'t>) (item: 't) : List<'t> =
    match list with
    | [] -> []
    | head :: tail when head = item -> tail
    | head :: tail -> head :: (removeFirst tail item)

and mapcons (a: 't) (ps: List<List<'t>>) (qs: List<List<'t>>) : List<List<'t>> =
    match ps with
    | [] -> qs
    | head :: tail -> (a :: head) :: mapcons a tail qs

and mapperm (x: List<'t>) (y: List<'t>) : List<List<'t>> =
    match y with
    | [] -> []
    | head :: tail ->
        let permuteNext = permute (removeFirst x head)
        let mappermNext = mapperm x tail
        mapcons head permuteNext mappermNext

and permute (x: List<'t>) : List<List<'t>> =
    match x with
    | [] -> [[]]
    | _ -> mapperm x x