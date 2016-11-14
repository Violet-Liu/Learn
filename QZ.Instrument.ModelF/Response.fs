#light
module Response

type Trade(_code, _name) =
    let mutable trades : Trade list = List.Empty
    member x.Code with get() = _code
    member x.Name with get() = _name
    member x.Trades with get() = trades and set _list = trades <- _list