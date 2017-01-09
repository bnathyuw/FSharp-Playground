namespace FSharpPlayground.Tests

[<Measure>] type gbp =
                static member asMoney amount = { Amount = amount / 1.0<gbp>; Currency = GBP }
[<Measure>] type eur = 
                static member fromGbp x = x * 1.25<eur/gbp>
                static member asMoney amount = { Amount = amount / 1.0<eur>; Currency = EUR }
[<Measure>] type chf = 
                static member fromGbp x = x * 1.36<chf/gbp>
                static member asMoney amount = { Amount = amount / 1.0<chf>; Currency = CHF }
[<Measure>] type cm
[<Measure>] type l = cm ^ 3
[<Measure>] type g