module Util
open System.ServiceModel.Web

let Context_Set() =
    let woc = WebOperationContext.Current
    woc.OutgoingResponse.ContentType = "application/json; charset=utf-8" |> ignore
    woc

