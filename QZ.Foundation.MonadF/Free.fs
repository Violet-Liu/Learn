namespace QZ.Foundation.MonadF

type Free<'F, 'A> =
    abstract member Bind: ('A -> Free<'F, 'B>) -> Free<'F, 'B>

[<Sealed>]
type private Gosub<'F, 'A, 'B>(a: Free<'F, 'A>, f: 'A -> Free<'F, 'B>) =
    member this.Value = a


