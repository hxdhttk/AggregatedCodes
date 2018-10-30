type Device() = class end

type Phone(model: string) =
    inherit Device()

    member __.ScreenOff() = "Turning the screen off..."

type Computer(model: string) =
    inherit Device()

    member __.ScreenSaverOn() = "Turning screen saver on..."

type DeviceDU =
    | Phone of Phone
    | Computer of Computer

let mkDU (device: Device) =
    match device with
    | :? Phone as p -> Phone p
    | :? Computer as c -> Computer c
    | _ -> failwithf "No more implementations for type %s" (typeof<Device>.FullName)

let goIdle (device: Device) =
    match mkDU device with
    | Phone p -> p.ScreenOff()
    | Computer c -> c.ScreenSaverOn()

let rec validate (str: string) =
    let nums: int array = parse str
    let isEven = nums.Length % 2 = 0
    nums
    |> (fun x -> if isEven then 
                    x |> Array.mapi (fun index num -> if index % 2 = 0 then num * 2 else num)
                 else 
                    x |> Array.mapi (fun index num -> if index % 2 = 0 then num else num * 2))
    |> Array.sumBy (fun num -> if num > 9 then num - 9 else num)
    |> (fun x -> x % 10 = 0)

and private parse (str: string) =
    str.Replace(" ", "").ToCharArray()
    |> Array.map (string >> int)
