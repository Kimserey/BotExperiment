#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

type Config = JsonProvider<"../../configs/botconfig.json">

let config = Config.GetSample()
let witRoot = "https://api.wit.ai/"

// Test message
Http.Request(
    witRoot + "message",
    query = [ ("q", "Hey ho") ],
    headers = [ ("Authorization", "Bearer " + config.ApiServerKey) ]).Body

// Get all entities
Http.Request(
    witRoot + "entities",
    headers = [ ("Authorization", "Bearer " + config.ApiServerKey) ]).Body