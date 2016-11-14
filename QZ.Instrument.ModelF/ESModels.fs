#light

namespace QZ.Instrument.ModelF

type MatchType = 
    MatchPhrase
    |   Match
    |   Prefix
    |   Query
    |   Term

[<AbstractClass>]
type BaseQuery(alpha, ascii, at, dot) =
    member val Alpha : bool = alpha with get, set
    member val Ascii : bool = ascii with get, set
    member val At : bool = at with get, set
    member val Dot : bool = dot with get, set


type FieldQuery(_type, field, boost, nonstrict) =
    member val Type : MatchType = _type with get, set
    member val Field : string = field with get, set
    member val Boost : double = boost with get, set
    member val NonStrict : bool = nonstrict with get, set

    new(field) = new FieldQuery(MatchType.Term, field, 0.0, false)
    new(_type, field) = new FieldQuery(_type, field, 0.0, false)
    new(_type, field, nonstrict) = new FieldQuery(_type, field, 0.0, nonstrict)
    new(_type, field, boost) = new FieldQuery(_type, field, boost, false)

type FieldQueryV(_type, field, boost, nonstrict, v) =
    member val V : int = v with get, set
    member val Type : MatchType = _type with get, set
    member val Field : string = field with get, set
    member val Boost : double = boost with get, set
    member val NonStrict : bool = nonstrict with get, set

    new(field, v) = new FieldQueryV(MatchType.Term, field, 0.0, false, v)
    new(_type, field, v) = new FieldQueryV(_type, field, 0.0, false, v)
    new(_type, field, nonstrict, v) = new FieldQueryV(_type, field, 0.0, nonstrict, v)
    new(_type, field, boost, v) = new FieldQueryV(_type, field, boost, false, v)


   