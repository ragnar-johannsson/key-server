module Handlers

open Suave
open System.Text.RegularExpressions

let sanitize input =
    let pattern = @"[^a-zA-Z0-9-]+"
    Regex.Replace (input, pattern, "")

let response output =
    match String.isEmpty output with
    | true  -> RequestErrors.NOT_FOUND ""
    | false -> output |> Successful.OK

let getKeys name =
    name
    |> sanitize
    |> Keys.getKeysforUserOrGroup
    |> Templates.createUsers
    |> response

