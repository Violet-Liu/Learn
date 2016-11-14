// Module ESUtil providers some functions that helps ES to analyze user's input, 
// Create by Sha Jianjian, 2016-09-23
#light
module ESUtil

open System
open QZ.Instrument.ModelF

/// Naively analyze user's input. This function is implemented using imperative paradigm
let ImQueryNaiveAnalyze input = 
    let mutable alphanumber = true
    let mutable ascii = true
    let mutable ``at`` = false
    let mutable dot = false
    
    for c in input do
        ``at`` <- if(not ``at`` && c = '@') then true else false
        dot <- if(not dot && c = '.') then true else false

        if(alphanumber && not (Char.IsLetterOrDigit(c))) 
        then 
            alphanumber <- false 
        else if(c > '\u007F')
        then
            ascii <- false
        else
            ()
    
    let tail = new FieldQuery(MatchType.MatchPhrase, "oc_brands", 3.0) :: []

    let r = match ascii with
            | true ->
                match alphanumber, ``at`` with
                | true, _ ->
                    let tail1 = new FieldQuery(MatchType.MatchPhrase, "oc_sites") :: tail
                    match String.length input with
                    | x when x > 2 ->
                        let tail2 = new FieldQuery(MatchType.Prefix, "oc_name.py_oc_name") :: tail1
                        if(x > 7)
                        then
                            new FieldQuery("oc_code") :: new FieldQuery("oc_number") :: new FieldQuery("oc_creditcode") :: tail2
                        else
                            tail2
                    | _ -> tail1
                | _, true -> new FieldQuery(MatchType.MatchPhrase, "oc_mails") :: tail
                | _ -> tail
            | _ ->
                new FieldQuery("oc_name.oc_name_raw") 
                :: new FieldQuery("od_faren") 
                :: new FieldQuery(MatchType.MatchPhrase, "oc_members") 
                :: new FieldQuery(MatchType.MatchPhrase, "od_gds")
                :: new FieldQuery(MatchType.MatchPhrase, "oc_name", true)
                :: new FieldQuery(MatchType.MatchPhrase, "oc_areaname")
                :: new FieldQuery(MatchType.Match, "oc_business")
                :: tail
    Array.ofList r

/// All ES fields that may be used to match use's input
/// Note that hex integer value means the states of alpha, dot, at, utf-8 respectively, from left to right
let AllFieldQuery =
    new FieldQueryV("oc_name.oc_name_raw", 0x0001) 
    :: new FieldQueryV("od_faren", 0x0001) 
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_members", 0x0001) 
    :: new FieldQueryV(MatchType.MatchPhrase, "od_gds", 0x0001)
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_name", true, 0x0001)
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_areaname", 0x0001)
    :: new FieldQueryV(MatchType.Match, "oc_business", 0x0001)
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_mails", 0x0010)
    :: new FieldQueryV("oc_code", 0x1000) :: new FieldQueryV("oc_number", 0x1000) :: new FieldQueryV("oc_creditcode", 0x1000)
    :: new FieldQueryV(MatchType.Prefix, "oc_name.py_oc_name", 0x1000)
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_sites", 0x1100)
    :: new FieldQueryV(MatchType.MatchPhrase, "oc_brands", 3.0, 0x1111) :: []

/// Naively analyze use's input. This method is implemented using functional paradigm
let QueryNaiveAnalyze input =
    // alpha: only contains alphanumber
    // dot: contains alphanumber and other ascii expect '@'
    // at: contains all ascii
    // utf-8: all utf8 chars
    // only one vary is true at the same time(eg. state true is exclusively)
    let rec Keypoint(alpha, dot, at, utf8, str: char list) =
        match str with
        | c :: tail ->
            let alpha1 = alpha && Char.IsLetterOrDigit c
            let utf81 = utf8 || c > '\u007F'
            let at1 = not utf81 && (at || c = '@')
            let dot1 = not at1 && (dot || not (Char.IsLetterOrDigit c))
            Keypoint(alpha1, dot1, at1, utf81, tail)
        | [] -> alpha :: dot :: at :: utf8 :: []

    let bools = Keypoint(true, false, false, false, input |> List.ofArray)

    // Transform the state to a hex integer value, and each bit represents if the related state is lighted
    let rec State2Val(bools: bool list, i) =
        match bools with
        | b :: tail -> 
            let i1 = if(b) then (i <<< 1) ||| 0x0001 else i <<< 1
            State2Val(tail, i1)
        | [] -> i

    let v = State2Val(bools, 0x0000)

    AllFieldQuery |> List.filter (fun x -> (x.V &&& v) > 0)

    
          