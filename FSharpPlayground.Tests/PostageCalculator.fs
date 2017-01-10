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

    type Package = Small | Medium of float<g> | LargeLight of float<l> | LargeHeavy of float<g>

    let (|Small|Medium|LargeLight|LargeHeavy|) (weight:float<g>, height:float<cm>, width:float<cm>, depth:float<cm>) =
        let volume = height * width * depth / 1000.0
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
        else if float weight <= float volume
            then LargeLight volume
        else LargeHeavy weight

    let private postageInGbp weight height width depth =
        match (weight, height, width, depth) with
            | Small -> smallPackagePrice 
            | Medium weight -> weight |> float |> (*) 4.0<gbp>
            | LargeHeavy weight -> weight |> float |> (*) 6.0<gbp>
            | LargeLight volume -> volume |> float |> (*) 6.0<gbp>
            | _ -> 0.0<gbp>

    let private convertTo currency priceInGbp = 
        match currency with
            | EUR -> priceInGbp |> withCommission |> eur.fromGbp |> eur.asMoney
            | CHF -> priceInGbp |> withCommission |> chf.fromGbp |> chf.asMoney
            | GBP -> priceInGbp |> gbp.asMoney

    let calculate weight height width depth currency =
        postageInGbp weight height width depth |> convertTo currency