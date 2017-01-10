namespace FSharpPlayground.Tests

type Currency = GBP | EUR | CHF

type Money = { Amount:float; Currency:Currency } with
    override this.ToString() = sprintf "%A" this

[<Measure>] type gbp =
                static member asMoney amount = { Amount = amount / 1.0<gbp>; Currency = GBP }
[<Measure>] type eur = 
                static member fromGbp (x:float<gbp>) = x * 1.25<eur/gbp>
                static member asMoney amount = { Amount = amount / 1.0<eur>; Currency = EUR }
[<Measure>] type chf = 
                static member fromGbp (x:float<gbp>) = x * 1.36<chf/gbp>
                static member asMoney amount = { Amount = amount / 1.0<chf>; Currency = CHF }