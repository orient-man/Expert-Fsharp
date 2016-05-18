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

// boxing
let x' = 5
let objx = box x'
let y' : int = unbox objx
let y'' = objx :?> int
let z' : string = unbox objx // InvalidCastException
let z'' = objx :?> string   // InvalidCastException

// making things generic
// defaults to int!
let rec intHcf a b = if a = 0 then b elif a < b then intHcf a (b - a) else intHcf (a - b) b

let hcfGeneric1 (zero, sub, lessThan) =
    let rec hcf a b =
        if a = zero then b
        elif lessThan a b then hcf a (sub b a)
        else hcf (sub a b) b
    hcf

// Generic Algorithms through Function Parameters
type INumeric<'T> =
    abstract Zero : 'T
    abstract Subtract : 'T * 'T -> 'T
    abstract LessThan : 'T * 'T -> bool

let hcfGeneric2 (ops : INumeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan(a, b) then hcf a (ops.Subtract(b, a))
        else hcf (ops.Subtract(a, b)) b
    hcf

let intOps =
    { new INumeric<int> with
        member ops.Zero = 0
        member ops.Subtract(x, y) = x - y
        member ops.LessThan(x, y) = x < y }

let intHcf2 = hcfGeneric2 intOps

// final: inlining + function parameters
let inline hcf a b =
    let op = { new INumeric<'T> with
        member ops.Zero = LanguagePrimitives.GenericZero<'T>
        member ops.Subtract(x, y) = x - y
        member ops.LessThan(x, y) = x < y }
    hcfGeneric2 op a b

// page: 134/599
