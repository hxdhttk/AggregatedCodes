open FSharp.Collections

type Distribution<'a when 'a: comparison> =
    abstract Sample: 'a
    abstract Support: Set<'a>
    abstract Expectation: ('a -> float) -> float

let always x = 
    {
        new Distribution<'a> with
            member d.Sample = x
            member d.Support = Set.singleton x
            member d.Expectation(H) = H(x)
    }

let rnd = System.Random()

let coinFlip (p: float) (d1: Distribution<'a>) (d2: Distribution<'a>) =
    if p < 0.0 || p > 1.0 then failwith "invalid probability in coinFlip"
    {
        new Distribution<'a> with
            member d.Sample =
                if rnd.NextDouble() < p then d1.Sample else d2.Sample
            member d.Support = d1.Support + d2.Support
            member d.Expectation(H) =
                p * d1.Expectation(H) + (1.0 - p) * d2.Expectation(H)
    }

let bind (dist: Distribution<'a>) (k: 'a -> Distribution<'b>) =
    {
        new Distribution<'b> with
            member d.Sample = (k(dist.Sample)).Sample
            member d.Support = Set.unionMany (dist.Support |> Set.map (fun d -> (k d).Support))
            member d.Expectation(H) = dist.Expectation(fun x -> (k x).Expectation(H))
    }

type DistributionBuilder() =
    member x.Delay f = bind (always ()) f
    member x.Let(v, f) = bind (always v) f
    member x.Bind(d, f) = bind d f
    member x.Return v = always v
    member x.ReturnFrom vs = vs

let dist = DistributionBuilder()

let weightedCases (inp: ('a * float) list) =
    let rec coinFlips w l =
        match l with
        | [] -> failwith "no coniFlips"
        | [(d, _)] -> always d
        | (d, p) :: rest -> coinFlip (p / (1.0 - w)) (always d) (coinFlips (w + p) rest)
    coinFlips 0.0 inp

let countedCases inp =
    let total = List.sumBy (fun (_, v) -> v) inp
    weightedCases (inp |> List.map (fun (x, v) -> (x, (float v / float total))))

type Outcome = Even | Odd | Zero
let roulette = countedCases [Even, 18; Odd, 18; Zero, 1]

roulette.Sample

roulette.Expectation (function Even -> 10.0 | Odd -> 0.0 | Zero -> 0.0)

type Light =
    | Red
    | Green
    | Yellow

let trafficLightD = weightedCases [ Red, 0.50; Yellow, 0.10; Green, 0.40 ]

type Action = Stop | Drive

let cautiousDriver light =
    dist {
        match light with
        | Red -> return Stop
        | Yellow -> return! weightedCases [ Stop, 0.9; Drive, 0.1 ]
        | Green -> return Drive
    }

let aggressiveDriver light =
    dist {
        match light with
        | Red -> return! weightedCases [ Stop, 0.9; Drive, 0.1 ]
        | Yellow -> return! weightedCases [ Stop, 0.1; Drive, 0.9 ]
        | Green -> return Drive
    }

let otherLight light =
    match light with
    | Red -> Green
    | Yellow -> Red
    | Green -> Red

type CrashResult = Crash | NoCrash

let crash (driverOneD, driverTwoD, lightD) =
    dist {
        let! light = lightD
        let! driverOne = driverOneD light
        let! driverTwo = driverTwoD (otherLight light)
        match driverOne, driverTwo with
        | Drive, Drive -> return! weightedCases [ Crash, 0.9; NoCrash, 0.1 ]
        | _ -> return NoCrash
    }

let model = crash(cautiousDriver, aggressiveDriver, trafficLightD)
let model2 = crash(aggressiveDriver, aggressiveDriver, trafficLightD)

let psychoDriver light =
    dist {
        match light with
        | Red -> return! weightedCases [Stop, 0.0; Drive, 1.0]
        | Yellow -> return! weightedCases [Stop, 0.0; Drive, 1.0]
        | Green -> return! weightedCases [Stop, 0.0; Drive, 1.0]
    }

let model3 = crash(cautiousDriver, psychoDriver, trafficLightD)
let model4 = crash(aggressiveDriver, psychoDriver, trafficLightD)
let model5 = crash(psychoDriver, psychoDriver, trafficLightD)

printfn "Model test: %A" model.Sample
printfn "Model expectation: %A" (model.Expectation (function Crash -> 1.0 | NoCrash -> 0.0))

printfn "Model2 test: %A" model2.Sample
printfn "Model2 expectation: %A" (model2.Expectation (function Crash -> 1.0 | NoCrash -> 0.0))

printfn "Model3 test: %A" model3.Sample
printfn "Model3 expectation: %A" (model3.Expectation (function Crash -> 1.0 | NoCrash -> 0.0))

printfn "Model4 test: %A" model4.Sample
printfn "Model4 expectation: %A" (model4.Expectation (function Crash -> 1.0 | NoCrash -> 0.0))

printfn "Model5 test: %A" model5.Sample
printfn "Model5 expectation: %A" (model5.Expectation (function Crash -> 1.0 | NoCrash -> 0.0))