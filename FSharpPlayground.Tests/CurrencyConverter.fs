namespace FSharpPlayground.Tests

module CurrencyConverter = 
    let private withCommission = (+) 200.0<gbp>
    
    let convertTo = function
        | EUR -> withCommission >> eur.fromGbp >> eur.asMoney
        | CHF -> withCommission >> chf.fromGbp >> chf.asMoney
        | GBP -> gbp.asMoney