namespace FSharpPlayground.Tests

module PostageCalculator =
    let private smallPackagePrice = 120.0<gbp>
    let private maximumSmallWeight = 60.0<g>
    let private maximumSmallHeight = 229.0<cm>
    let private maximumSmallWidth = 162.0<cm>
    let private maximumSmallDepth = 25.0<cm>
    let private maximumMediumWeight = 500.0<g>
    let private maximumMediumHeight = 324.0<cm>
    let private maximumMediumWidth = 229.0<cm>
    let private maximumMediumDepth = 100.0<cm>

    let private withCommission = (+) 200.0<gbp>

    type Package = Small | Medium of float<g> | LargeLight of float<cm ^ 3> | LargeHeavy of float<g>

    let (|Small|Medium|LargeLight|LargeHeavy|) (weight, height, width, depth) =
        let volume:float<cm ^ 3> = height * width * depth / 1000.0
        if weight <= maximumSmallWeight 
                && height <= maximumSmallHeight 
                && width <= maximumSmallWidth 
                && depth <= maximumSmallDepth 
            then Small
        else if weight <= maximumMediumWeight 
                && height <= maximumMediumHeight 
                && width <= maximumMediumWidth 
                && depth <= maximumMediumDepth 
            then Medium weight
        else if weight / 1.0<g> <= volume / 1.0<cm ^ 3>
            then LargeLight volume
        else LargeHeavy weight

    let private postageInGbp weight height width depth =
        match (weight, height, width, depth) with
            | Small -> smallPackagePrice 
            | Medium weight -> weight |> float |> (*) 4.0<gbp>
            | LargeHeavy weight -> weight |> float |> (*) 6.0<gbp>
            | LargeLight volume -> volume |> float |> (*) 6.0<gbp>

    let calculate weight height width depth currency =
        postageInGbp weight height width depth |> CurrencyConverter.convertTo currency