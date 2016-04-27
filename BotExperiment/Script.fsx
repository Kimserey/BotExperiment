#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data
open FSharp.Data.HttpRequestHeaders

type Config = JsonProvider<"../../configs/botconfig.json">

let config = Config.GetSample()
let witRoot = "https://api.wit.ai/"

let headers =
    [ ("Authorization", "Bearer " + config.ApiServerKey) ]

[<AutoOpen>]
module Wit =
    let message str     = ("q", str)
    let sessionId id    = ("session_id", id)
    let quantity (n: int)      = ("n", string n)

// Test message
Http.Request(
    witRoot + "message",
    query = [ ("q", "Hey ho") ],
    headers = headers).Body

// Get all entities
Http.Request(
    witRoot + "entities",
    headers = headers).Body

(** 
    Post request - converse
**)
let sid = "123ab"

Http.Request(
    witRoot + "converse",
    query = [ message "Show me the list of items"; sessionId sid ],
    headers = headers @ [ ContentType HttpContentTypes.Json; Accept HttpContentTypes.Json ],
    httpMethod = "POST")

Http.Request(
    witRoot + "converse",
    query = [ sessionId sid ],
    headers = headers @ [ ContentType HttpContentTypes.Json ],
    httpMethod = "POST")

Http.Request(
    witRoot + "converse",
    query = [ sessionId sid ],
    headers = headers @ [ ContentType HttpContentTypes.Json ],
    body = TextRequest """ { "items": "hello" } """)

(**
    Get outcomes based on text - message
**)

Http.Request(
    witRoot + "message",
    query = [ message "Add something"; quantity 3 ],
    headers = headers)