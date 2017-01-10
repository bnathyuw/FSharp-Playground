namespace FSharpPlayground.Tests

type Currency = GBP | EUR | CHF

type Money = { Amount:float; Currency:Currency } with
    override this.ToString() = sprintf "%A" this
