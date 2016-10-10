module Keys

open System.IO

let getKeyFiles dir =
    Directory.GetFiles (dir, Config.KeysFilename, SearchOption.AllDirectories) |> Array.toList

let userAndKey path =
    Path.GetDirectoryName path |> Path.GetFileName, File.ReadAllText path

let getKeysforUserOrGroup name =
    let dir = [Config.KeysDirectory; name] |> List.toArray |> Path.Combine

    match Directory.Exists dir && not (String.isEmpty name) with
    | true ->
        dir
        |> getKeyFiles
        |> List.map userAndKey
        |> List.distinct
    | false -> []
