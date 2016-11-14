module Extension
open System
open System.Text.RegularExpressions
open Chiron
open Chiron.Mapping

/// Extent String type
type String with
    /// Convert html to plain text. "\r" -> <![CDATA[<br>]]>
    member x.Html2PlainText() =
        match x with
        | s when not (String.IsNullOrWhiteSpace x)
            -> s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>")
        | _ -> x

    member x.ToSafe() = 
        if(String.IsNullOrWhiteSpace x) then x
        else
            Regex.Replace(Regex.Replace(x, "[';\"\\r\\n]+", ""), "CHAR<\\d+>", "", RegexOptions.IgnoreCase)

    

(* json serialization *)
