#r "bin/Debug/net8.0/CSLib.dll"
#load "Stuff.fs"

open CSLib

let logger = Logger()

logger.Warn "Hello"


type OptionBuilder() =
    member _.Return(x) = Some x
    member _.Bind(x, f) = Option.bind f x
    member _.MergeSources(x, y) =
        match (x, y) with
        | Some x, Some y -> Some (x, y)
        | _, _ -> None

let option = OptionBuilder()

let (>>=) = Option.bind
let (<!>) = Option.map
let (<*>) func value =
    match (func, value) with
    | Some f, Some x -> Some <| f x
    | _ -> None


let nonneg n =
    if n < 0
    then None
    else Some n

let add x y = x + y

let calc x y =
    option {
        let! x' = nonneg x
        let! y' = nonneg y
        return (add x' y')
    }


let validatePrice (p: decimal)  = 
        if p <= 0M || p > 100000M 
        then None 
        else Some p

let validateModel (s: string)  = 
        if s.Length = 0 
        then None 
        else Some s

type Guitar = Guitar of model:string * price:decimal

let makeGuitar m p = Guitar(m, p)