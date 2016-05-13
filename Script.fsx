open System

(*
 * Vim support:
 * leader<i> / Alt-Enter - eval line/selection
 * leader<d> - go to declaration
 * leader<s> - go back
 * leader<t> - show type
*)

// let .. in - zamiast nowej linii
let x = let y = 5 in let z = 3 in y + z

// ">>" vs. "|>"
let sqrt n = n * n
[1; 2; 3] |> List.map sqrt |> List.rev
[1; 2; 3] |> (List.map sqrt >> List.rev)
let f = List.map sqrt >> List.rev
f [1; 2; 3]

// Suave Web Server: https://suave.io/
#r "packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Http
open Suave.Web

let html =
    [ yield "<html><body>hello word</body></html>" ]
    |> String.concat "\n"

startWebServer defaultConfig (Successful.OK html)

// mutually recursive functions
let rec even n = (n = 0u) || odd(n - 1u)
and odd n = (n <> 0u) && even(n - 1u)

// mutable records
type EventCounter = { mutable Total: int; Name: string }
let ec = { Total = 0; Name = "name" }
ec.Total <- ec.Total + 1

let mutable cell = 1
cell <- 2
