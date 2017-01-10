namespace FSharpPlayground.Tests

open Package

module PackagePricer =
    let private smallPackagePrice = 120.0<gbp>
    let private applyMediumPackageMultiplier = (*) 4.0<gbp>
    let private applyLargePackageMultiplier = (*) 6.0<gbp>

    let price = function
        | Small -> smallPackagePrice 
        | Medium weight -> weight |> float |> applyMediumPackageMultiplier
        | LargeHeavy weight -> weight |> float |> applyLargePackageMultiplier
        | LargeLight volume -> volume |> float |> applyLargePackageMultiplier