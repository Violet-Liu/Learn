module Processor
open QZ.Instrument.Model

let Head_Handle (request: Request, key: string) =
    Util.Context_Set()
