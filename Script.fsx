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

// F# 4
let mutable cell = 1
cell <- 2

// F# 3
let cell2 = ref 1
cell2 := 2
cell2.Value
!cell2

// lazy
let sixty = lazy (30 + 30)
sixty.Force()

// safe IDisposable
open System.IO
let linesOfFile =
    seq { use reader = new StreamReader(File.OpenRead("Script.fsx"))
          while not reader.EndOfStream do
              yield reader.ReadLine() }

linesOfFile |> Seq.take 1 |> Seq.head

// records & type inference
type Dot = { X : int; Y : int }
type Point = { X : float; Y : float }
let dist p = p.X * p.X + p.Y * p.Y   // Point! because it's after Dot
let dist2 (p: Dot) = sqrt (p.X * p.X + p.Y * p.Y)

// good practice: using discriminated unions as records
type Point3D = Vector3D of float * float * float
let origin = Vector3D(0., 0., 0.)

// comparing
("abc", "def") < ("abc", "xyz")
compare [10; 30] [10; 20]

// hash
hash 100
hash "abc"
hash (100, "abc")

// F# 4: disable generic hashing & comparison
//open NonStructuralComparison
//compare (1,3) (5,4) // fails!
