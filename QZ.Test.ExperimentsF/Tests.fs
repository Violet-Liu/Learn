module Tests

open Xunit
open MongoDB.Bson
open MongoDB.Driver
open QZ.Instrument.Experiments.ScoreIO

type ``ES weight-score relationship write into MongoDB``() =
    
    [<Fact>]
    member this.``It can find object from MongoDB``() =
        let result = Exist "test"
        Assert.Equal(true, result)

    [<Fact>]
    member this.``Some docs``() =
        let docs = Query "company_nextgen1"
        Assert.Equal("_score * doc['oc_weight'].value", docs.First().Script)
