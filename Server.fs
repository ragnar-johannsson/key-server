module Server

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Logging

let logger = Targets.create Verbose

let router =
    choose [
        GET >=> pathScan "/%s" Handlers.getKeys
        RequestErrors.BAD_REQUEST ""
    ]
    >=> Writers.setMimeType "text/plain"
    >=> log logger logFormat

let config = {
    defaultConfig with bindings = [ HttpBinding.createSimple HTTP Config.Iface Config.Port ]}

[<EntryPoint>]
let main argv =
    startWebServer config router
    0