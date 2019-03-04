[<Struct>]
type Vector = {X: float; Y: float; Z: float}
    with
        static member ZeroVector = {X = 0.; Y=0.; Z = 0.}

type Dependency = {
    Id: string
    Type: string
}

type Root = {
    Id: string
    Name: string
    DependsOn: Dependency option
    Method: string
}

type Zoom = {
    X: bool
    Y: bool
    Z: bool
}

type Rotation = {
    Roll: float
    Yaw: float
    Pitch: float
}

type Paddings = {
    Front: float
    Back: float
    Left: float
    Right: float
    Top: float
    Bottom: float
}

type Anchor = { Target: string; Offset: float }

type Anchors = {
    Front: Anchor option
    Back: Anchor option
    Left: Anchor option
    Right: Anchor option
    Top: Anchor option
    Bottom: Anchor option
}

type RelativePos = {
    Id: string
    Anchors: Anchors
}

type Product = {
    Id: string
    Name: string
    Path: string
    Zoom: Zoom
    Paddings: Paddings
    RelativeTo: RelativePos
}

let transformer = id

let dummyVector = {Vector.X = 0.; Y = 0.; Z = 0.}

let ``+X``, ``-X``, ``+Y``, ``-Y``, ``+Z``, ``-Z`` = dummyVector, dummyVector, dummyVector, dummyVector, dummyVector, dummyVector

let dummyProduct = {
    Id = "Dummy"
    Name = "Dummy"
    Path = "Dummy"
    Zoom = { Zoom.X = false; Y = false; Z= false}
    Paddings = {Paddings.Front = 0.; Back = 0.; Left = 0.; Right = 0.; Top = 0.; Bottom = 0.}
    RelativeTo = {
        Id = "Dummy"
        Anchors = {
            Front = Some { Target = "Dummy"; Offset = 0.}
            Back = Some { Target = "Dummy"; Offset = 0.}
            Left = Some { Target = "Dummy"; Offset = 0.}
            Right = Some { Target = "Dummy"; Offset = 0.}
            Top = Some { Target = "Dummy"; Offset = 0.}
            Bottom = Some { Target = "Dummy"; Offset = 0.}
        }
    }
}

let front anchors pos =
    match anchors.Front with
    | Some anchor -> 
        match anchor.Target with
        | "Front" -> transformer pos
        | "Back" -> transformer pos
        | _ -> pos
    | _ -> pos

let back anchors pos =
    match anchors.Back with
    | Some anchor ->
        match anchor.Target with
        | "Back" -> transformer pos
        | "Front" -> transformer pos
        | _ -> pos
    | _ -> pos