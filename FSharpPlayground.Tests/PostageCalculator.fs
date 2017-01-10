namespace FSharpPlayground.Tests

open PackagePricer
open CurrencyConverter

module PostageCalculator =
    let calculate weight height width depth currency =
        Package.create weight height width depth |> price |> convertTo currency