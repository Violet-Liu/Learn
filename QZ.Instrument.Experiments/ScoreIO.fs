#light
namespace QZ.Instrument.Experiments

open MongoDB.Driver
open MongoDB.Driver.Builders
open MongoDB.Bson

module ScoreIO =


    let client = new MongoClient("mongodb://192.168.0.153")


    let database = client.GetDatabase("test")

    let collection = database.GetCollection<Observation>("ESScore")

    let Exist obId = collection.Find(fun x -> x.ObId = obId).Any()

    let InsertAsync (ob:Observation) = collection.InsertOneAsync(ob)

    let Query prefix = collection.Find(fun x -> x.ObId.StartsWith(prefix))

    let Index_Create() = 
        collection.Indexes.CreateOne(new BsonDocumentIndexKeysDefinition<Observation>(new BsonDocument("ObId", BsonInt32(1))))
//        match Query.EQ("Id", BsonString(ob.Id)) with
//        | query when query. -> Builders<BsonDocument>
//        | _    -> collection.InsertOne ob
        






