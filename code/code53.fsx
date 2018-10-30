type ``[,,]``<'a> with
    member this.GetSlice(x: int, startY: int option, endY: int option, startZ: int option, endZ: int option) =
        let startY, endY = 
            Option.defaultValue (this.GetLowerBound 1) startY,
            Option.defaultValue (this.GetUpperBound 1) endY
        let startZ, endZ = 
            Option.defaultValue (this.GetLowerBound 2) startZ,
            Option.defaultValue (this.GetUpperBound 2) endZ
        let slice = Array2D.zeroCreate<'a> (this.GetLength 1) (this.GetLength 2)
        for y in startY..endY do
            for z in startZ..endZ do
               slice.[y-startY, z-startZ] <- this.[x,y,z]
        slice
    
    member this.GetSlice(startX: int option, endX: int option, y: int, startZ: int option, endZ: int option) =
        let startX, endX = 
            Option.defaultValue (this.GetLowerBound 1) startX,
            Option.defaultValue (this.GetUpperBound 1) endX
        let startZ, endZ = 
            Option.defaultValue (this.GetLowerBound 2) startZ,
            Option.defaultValue (this.GetUpperBound 2) endZ
        let slice = Array2D.zeroCreate<'a> (this.GetLength 1) (this.GetLength 2)
        for x in startX..endX do
            for z in startZ..endZ do
               slice.[x-startX, z-startZ] <- this.[x,y,z]
        slice
    
    member this.GetSlice(startX: int option, endX: int option, startY: int option, endY: int option, z: int) =
        let startX, endX = 
            Option.defaultValue (this.GetLowerBound 1) startX,
            Option.defaultValue (this.GetUpperBound 1) endX
        let startY, endY = 
            Option.defaultValue (this.GetLowerBound 2) startY,
            Option.defaultValue (this.GetUpperBound 2) endY
        let slice = Array2D.zeroCreate<'a> (this.GetLength 1) (this.GetLength 2)
        for x in startX..endX do
            for y in startY..endY do
               slice.[x-startX, y-startY] <- this.[x,y,z]
        slice