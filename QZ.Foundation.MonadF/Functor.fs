namespace QZ.Foundation.MonadF

type Functor<'F> =
    abstract member Map: ('A -> 'B) * _1<'F, 'A> -> _1<'F, 'B>

