namespace FSharpPlayground.Tests

module PackagePricer =
    let private smallPackagePrice = 120.0<gbp>

    let private withCommission = (+) 200.0<gbp>

    type SizedPackage = Small | Medium of float<g> | LargeLight of float<cm ^ 3> | LargeHeavy of float<g>

    let (|Small|Medium|LargeLight|LargeHeavy|) package =
        if package |> Package.isSmall then Small
        else if package |> Package.isMedium then Medium package.Weight
        else if package |> Package.isLargeLight then LargeLight (package |> Package.volume)
        else LargeHeavy package.Weight

    let priceOf package =
        match package with
            | Small -> smallPackagePrice 
            | Medium weight -> weight |> float |> (*) 4.0<gbp>
            | LargeHeavy weight -> weight |> float |> (*) 6.0<gbp>
            | LargeLight volume -> volume |> float |> (*) 6.0<gbp>