module Requests
open Chiron

type Request =
    { h: string
      b: string }
    static member ToJson(x: Request) = json {
        do! Json.write "h" x.h
        do! Json.write "b" x.b
    }

    static member FromJson(_: Request) = json{
        let! hr = Json.read "h"
        let! br = Json.read "b"
        return {h = hr; b = br}
    }

let FromJson(input: string) : Request = input |> Json.parse |> Json.deserialize