#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

type Config = JsonProvider<"../../configs/botconfig.json">

let config = Config.GetSample()

Http.Request(
    "https://api.wit.ai/message?q=Hey%20Ho", 
    headers = [ ("Authorization", "Bearer " + config.ApiServerKey) ])
