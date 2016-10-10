module Config

open System

let defaultValue d p =
    match String.IsNullOrEmpty p with
    | true  -> d
    | false -> p

let Iface =
    Environment.GetEnvironmentVariable("IFACE")
    |> defaultValue "0.0.0.0"

let Port =
    Environment.GetEnvironmentVariable("PORT")
    |> defaultValue "3000"
    |> System.Int32.Parse

let KeysDirectory =
    Environment.GetEnvironmentVariable("KEYS_DIRECTORY")
    |> defaultValue "/opt/keys"

let KeysFilename =
    Environment.GetEnvironmentVariable("KEYS_FILENAME")
    |> defaultValue "authorized_keys"

let TemplatePath =
    Environment.GetEnvironmentVariable("TEMPLATE_PATH")
    |> defaultValue "/app/create_user.sh"
