// module Shape =
//     type Triangle = private { a: float; b: float; c: float }
//     let createTriangle a b c : Triangle option =
//         let isValid a b c = a + b > c && b + c > a && c + a > b
//         if isValid a b c then Some { Triangle.a = a; b = b; c = c } else None
//     let (|Triangle|) { Triangle.a = a; b = b; c = c; } = (a, b, c)

module Shape =
    [<StructuralEquality>]
    [<StructuralComparison>]
    type Triangle =
        struct
            val a: float
            val b: float
            val c: float
            private new(a, b, c) = { a = a; b = b; c = c }
            static member Create a b c : Triangle option =
                let isValid a b c = a + b > c && b + c > a && c + a > b 
                if isValid a b c then Some(Triangle(a, b, c)) else None
        end

open Shape

// createTriangle 1. 2. -1.
// let tri = createTriangle 2. 3. 4. |> Option.get
// let (Triangle(a, b, c)) = tri

Triangle.Create 1. 2. -1.
let tri = Triangle.Create 3. 4. 5. |> Option.get
let { Triangle.a = a; Triangle.b = b; Triangle.c = c } = tri

printfn "a = %f; b = %f; c = %f" a b c
