namespace QZ.Instrument.Experiments

open MongoDB.Bson
open MongoDB.Driver

/// ES observation type
type Observation() =
    member val Id : ObjectId = ObjectId.GenerateNewId() with get, set
    member val ObId : string  = "" with get, set        // unique mark id, => Name + Query + Script
    member val Name : string = "" with get, set   // to be observed object's name. Format is [es_index].[es_type]
    member val Query : string = "" with get, set // query string
    member val Script : string = "" with get, set // script content
    member val Weight: double array = [||] with get, set // doc weight field value
    member val WeightExt: double array = [||] with get, set // weight ext
    member val Score : double array = [||] with get, set   // data list with item type of double

