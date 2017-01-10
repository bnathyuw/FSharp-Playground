namespace FSharpPlayground.Tests

module CurrencyConverter = 
    let private withCommission = (+) 200.0<gbp>
    
    let convertTo currency priceInGbp = 
        match currency with
            | EUR -> priceInGbp |> withCommission |> eur.fromGbp |> eur.asMoney
            | CHF -> priceInGbp |> withCommission |> chf.fromGbp |> chf.asMoney
            | GBP -> priceInGbp |> gbp.asMoney