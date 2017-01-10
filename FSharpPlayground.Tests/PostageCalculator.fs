namespace FSharpPlayground.Tests

module PostageCalculator =
    let calculate weight height width depth currency =
        let package = Package.create weight height width depth
        PackagePricer.priceOf package |> CurrencyConverter.convertTo currency