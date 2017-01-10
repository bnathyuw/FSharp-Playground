namespace FSharpPlayground.Tests

module PostageCalculator =
    let calculate weight height width depth currency =
        PackagePricer.priceOf weight height width depth |> CurrencyConverter.convertTo currency