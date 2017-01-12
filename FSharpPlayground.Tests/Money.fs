namespace FSharpPlayground.Tests

type Currency = GBP | EUR | CHF 

[<Measure>] type gbp
[<Measure>] type eur = 
                static member fromGbp (x:float<gbp>) = x * 1.25<eur/gbp>
[<Measure>] type chf = 
                static member fromGbp (x:float<gbp>) = x * 1.36<chf/gbp>

type Money = Gbp of float<gbp> | Eur of float<eur> | Chf of float<chf>
