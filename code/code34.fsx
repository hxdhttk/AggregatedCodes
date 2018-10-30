type AsyncResultBuilder() =
    member __.Return (value: 'a) : Async<Result<'a, 'b>> = async { return Ok value }

    member __.Bind
        (asyncResult: Async<Result<'a, 'c>>,
         binder: 'a -> Async<Result<'b, 'c>>)
        : Async<Result<'b, 'c>> =
        async {
            let! result = asyncResult
            let bound =
                match result with
                | Ok x -> binder x
                | Error x -> async { return Error x }
            return! bound
        }

[<AutoOpen>]
module Extenstions =
    type AsyncResultBuilder with
        member this.Bind
            (asnc: Async<'a>,
             binder: 'a -> Async<Result<'b, 'c>>)
            : Async<Result<'b, 'c>> =
            let asyncResult = async {
                let! x = asnc
                return Ok x
            }
            this.Bind(asyncResult, binder)

let asyncResult = AsyncResultBuilder()

let f () : Async<Result<int, string>> =
    asyncResult {
        let! (str: string) = asyncResult { return "" }
        return str.Length
    }