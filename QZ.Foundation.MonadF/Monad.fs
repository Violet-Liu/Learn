#light
module Monad

//Collapse current – CTRL+M, CTRL+S
//Collapse All – CTRL+M, CTRL+A
//Expand current – CTRL+M, CTRL+E
//Expand All – CTRL+M, CTRL+X

type Option<'T> with
    member x.Do(a:'T -> Unit) =
        match x with
        | Some v -> a v
                    x
        | None -> x

    member x.Select<'U>(f:'T -> 'U option) = x |> Option.bind f

    member x.Select<'U, 'V when 'V : null>(f: 'T -> 'U option, g: 'T -> 'U -> 'V) =
        x |> Option.bind (fun t -> f t |> Option.bind (fun u -> g t u |> Option.ofObj))

    member x.Filter(f: 'T -> bool) = x |> Option.filter f

    member x.DoWhen(f: 'T -> bool, a: 'T -> Unit) =
        if(x.IsSome && (f x.Value)) 
            then a x.Value
                 x
        else x

    member x.DoWhenOrElse(f: 'T -> bool, w: 'T -> Unit, e: 'T -> Unit) =
        match x with
        | Some v -> match f v with
                    | true  -> w v
                               x
                    | false -> e v
                               x
        | None -> x


    member x.DoWhenButNone(f: 'T -> bool, a: 'T -> Unit) =
        if(x.IsSome && f x.Value)
            then a x.Value 
                 None
        else 
            x

    member x.ShiftWhenOrElse<'U>(f: 'T -> bool, w: 'T -> 'U, e: 'T -> 'U) =
        match x with
        | Some v -> match f v with
                    | true  -> w v |> Some
                    | false -> e v |> Some
        | None -> None


type Either<'L, 'R>() =

    static member FromLeft (l: 'L) = 
        let either = new Either<'L, 'R>()
        either.Left <- Some l
        either

    static member FromRight (r: 'R) =
        let either = new Either<'L, 'R>()
        either.Right <- Some r
        either

    member val Left: Option<'L> = None with get, set

    member val Right: Option<'R> = None with get, set

    member x.Bind<'NR>(f: 'R -> Either<'L, 'NR>) =
        match x.Left with
        | Some l -> Either.FromLeft l
        | None   -> f x.Right.Value

    

