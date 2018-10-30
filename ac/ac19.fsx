open System
open System.IO

let input = File.ReadAllText(@"ac19.txt").Split([| "\r\n" |], options=StringSplitOptions.RemoveEmptyEntries)
            |> Array.map (fun str -> str.ToCharArray())

type Dir = | L | U | R | D

let rec walk curX curY (dir: Dir) (charMatrix: char[][]) =
    let letterList = ResizeArray<char> []
    let rec innerFn innerCurX innerCurY innerDir =
        match charMatrix.[innerCurY].[innerCurX] with
        | ' ' -> letterList |> Seq.map string |> String.concat ""
        | '|' | '-' -> match innerDir with
                       | L -> innerFn (innerCurX - 1) innerCurY L
                       | U -> innerFn innerCurX (innerCurY - 1) U
                       | R -> innerFn (innerCurX + 1) innerCurY R
                       | D -> innerFn innerCurX (innerCurY + 1) D
        | '+' -> match innerDir with
                 | L | R -> let up = charMatrix.[innerCurY - 1].[innerCurX]
                            let down = charMatrix.[innerCurY + 1].[innerCurX]
                            match up, down with
                            | '|', _ -> innerFn innerCurX (innerCurY - 1) U
                            | _, '|' -> innerFn innerCurX (innerCurY + 1) D
                            | _, _ -> failwith "Invalid path!"
                 | U | D -> let left = charMatrix.[innerCurY].[innerCurX - 1]
                            let right = charMatrix.[innerCurY].[innerCurX + 1]
                            match left, right with
                            | '-', _ -> innerFn (innerCurX - 1) innerCurY L
                            | _, '-' -> innerFn (innerCurX + 1) innerCurY R
                            | _, _ -> failwith "Invalid path!"
        | ch when Char.IsLetter(ch) -> letterList.Add(ch)
                                       match innerDir with
                                       | L -> innerFn (innerCurX - 1) innerCurY L
                                       | U -> innerFn innerCurX (innerCurY - 1) U
                                       | R -> innerFn (innerCurX + 1) innerCurY R
                                       | D -> innerFn innerCurX (innerCurY + 1) D
        | _ -> failwith "Invalid path!"
    
    innerFn curX curY dir
    
printf "%s" (walk 1 0 D input)