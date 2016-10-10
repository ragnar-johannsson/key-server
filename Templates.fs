module Templates

open System.IO
open System.Text.RegularExpressions

let template =
    lazy (File.ReadAllText Config.TemplatePath)

let replace (pattern : string) (replacement : string) (input : string) =
    Regex.Replace (input, pattern, replacement)

let applyTemplate user =
    template.Force()
    |> replace "{{ user }}" (fst user)
    |> replace "{{ keys }}" (snd user)

let createUsers input =
    input
    |> List.map applyTemplate
    |> String.concat "\n"
