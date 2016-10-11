module Templates

open System.IO
open System.Text.RegularExpressions

let createUsersTemplate =
    lazy (File.ReadAllText Config.CreateUsersTemplatePath)

let verifyConfigTemplate =
    lazy (File.ReadAllText Config.VerifyConfigTemplatePath)

let replace (pattern : string) (replacement : string) (input : string) =
    Regex.Replace (input, pattern, replacement)

let applyCreateUsersTemplate user =
    createUsersTemplate.Force()
    |> replace "{{ user }}" (fst user)
    |> replace "{{ keys }}" (snd user)

let createUsers input =
    input
    |> List.map applyCreateUsersTemplate
    |> String.concat "\n"

let verifyConfig input =
    match String.isEmpty input with
    | true  -> input
    | false -> [input; verifyConfigTemplate.Force()] |> String.concat "\n"