#light
module Util
open System
open System.Text

let red_pretag = "<font color=\"red\""
let red_posttag = "</font>"

let Code2Display (oc_code: string) = 
    match oc_code with
    | x when x.EndsWith("T") -> x.TrimEnd('T') + "(台湾统一编码)"
    | x when x.EndsWith("K") -> x.TrimEnd('K').TrimStart('0')+"(香港公司编号)"
    | _ -> oc_code

let Number2Display (oc_number: string) =
    match oc_number with
    | x when x.EndsWith "T" || x.EndsWith "K" -> ""
    | _ -> oc_number

/// Get operation status of a company according to it's oc_ext value
let OpStatus_Get (oc_ext: string) =
    match oc_ext with
    | x when x.Contains "吊销" -> "吊销"
    | x when x.Contains "注销" -> "注销"
    | x when x.Contains "停业" -> "停业"
    | x when x.Contains "清算" -> "清算"
    | x when x.Contains "解散" -> "解散"
    | x when x.Contains "存续" -> "存续"
    | x when x.Contains "核准成立" || x.Contains "核准设立" -> "核准设立"
    | _ -> "正常"

/// To judge if operation status of a company is normal according it's oc_ext value
/// true -> normal, false -> abnormal
let OpStatus_Filter(oc_ext: string) =
    match oc_ext with
    | x when x.Contains "吊销" || x.Contains "注销" || x.Contains "停业" || x.Contains "清算" || x.Contains "解散"
        -> false
    | _ -> true

/// Highlight src string with a keyword string
let Highlight (src: string) (keyword: string) =
    if  String.IsNullOrWhiteSpace src || String.IsNullOrWhiteSpace keyword then src
    else
        let sb = new StringBuilder()
        for w in src do
            match w with
            | w when keyword.IndexOf w >= 0 -> sb.Append(red_pretag).Append(w).Append(red_posttag)
            | _ -> sb.Append w
            |> ignore

        sb.ToString()

/// Make sure page index or size is located in [1, 50]
/// default value 'def' must be located in [1, 50]
let PageIndexCheck value def=
    match value with
    | x when x < 1 || x > 50 -> def
    | _ -> value


/// Handle phone number of a company by given the district code
/// Will keep a phone number virgin if it contains district code but not "-"
let Tel_Handle (tel:string) (district:string) = 
    let index = tel.IndexOf('-')
    match index with
    | i when i > -1 -> if i < 5 then tel else district + "-" + tel
    | _ -> match tel.Length with
            | j when j > 10 -> tel      // think it is a cell phone number
            | _ -> district + "-" + tel // think it is a telephone number


/// Handle phone number of a company by given the district code
/// This function is recommended
let Smart_Tel_Handle(tel: string) (district: string) =
    let prefix = district + "-"
    match tel.StartsWith prefix with
    | true -> tel
    | false -> match tel.StartsWith district with
                | true -> prefix + tel.Substring district.Length
                | false -> match tel.Length > 9 with
                            | true -> tel
                            | _ -> district + "-" + tel