module Templates

open System.IO
open System.Text.RegularExpressions

let createUsersTemplate =
    lazy (File.ReadAllText Config.CreateUsersTemplatePath)

let verifyConfigTemplate =
    lazy (File.ReadAllText Config.VerifyConfigTemplatePath)

let replace (pattern : string) (replacement : string) (input : string) =
    Regex.Replace (input, pattern, replacement)

let applyCreateUsersTemplate ttl user =
    createUsersTemplate.Force()
    |> replace "{{ user }}" (fst user)
    |> replace "{{ keys }}" (snd user)
    |> replace "{{ ttl }}" (ttl.ToString())

let createUsers ttl users =
    users
    |> List.map (applyCreateUsersTemplate ttl)
    |> String.concat "\n"

let verifyConfig input =
    match String.isEmpty input with
    | true  -> input
    | false -> [input; verifyConfigTemplate.Force()] |> String.concat "\n"