namespace Tommy


///Factory for simple dependency inversion in main application. 
module Factory =
    open CSLib
    let GetLogger (): ILogger  = 
        new Logger()
   



///Modules for practicing functional-style domain modeling and data validation.

module Order =
    type Order =
        | Digital of downloads:int * price:decimal
        | Normal of quantity:double * price:decimal
        | ValidatedOrder of Order


module Validator =
    type private Validator() =
        member this.Return(x) = Ok(x)
        member this.Bind(x, f) =
            match x with
            | Ok(v) -> f(v)
            | Error e -> Error e
        member this.MergeSources(r1, r2) =
            match (r1, r2) with
            | Ok x, Ok y -> Ok(x, y)
            | Ok _, Error e -> Error e
            | Error e, Ok _ -> Error e
            | Error e1, Error e2 -> Error <| e1@e2
            
    open Order
    let private validateQuantity (q: double) = 
        if q <= 0.0 || q > 100.0
        then Error ["Invalid quantity"] 
        else Ok q
    let private validatePrice (p: decimal)  = 
        if p <= 0M || p > 100000M 
        then Error ["Invalid price"] 
        else Ok p
    let private validator = Validator()
    let validate (order: Order) =
        match order with
        | ValidatedOrder o -> Ok <| ValidatedOrder o
        | Normal (quantity, price) -> 
            validator {
                let! q = validateQuantity quantity
                and! p = validatePrice price
                return ValidatedOrder <| Normal(q, p)
            }
        | Digital (downloads, price) ->
            validator {
                let! p = validatePrice price
                return ValidatedOrder <| Digital(downloads, p)
            }

module Stuff =
    type Money = private Money of double
    let createMoney amount =
        if amount < 0.0
        then None
        else Some <| Money amount

    let (|Money|)(Money amount) = amount
        



