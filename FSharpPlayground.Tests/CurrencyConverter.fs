namespace FSharpPlayground.Tests

module CurrencyConverter = 
    let private withCommission = (+) 200.0<gbp>
    
    let convertTo = function
        | EUR -> withCommission >> eur.fromGbp >> Eur
        | CHF -> withCommission >> chf.fromGbp >> Chf
        | GBP -> Gbp