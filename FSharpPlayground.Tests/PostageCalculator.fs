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

    let private (|SmallPackage|_|) (weight, height, width, depth) =
        if weight <= maximumSmallWeight 
            && height <= maximumSmallHeight 
            && width <= maximumSmallWidth 
            && depth <= maximumSmallDepth 
            then Some ()
        else None

    let private (|MediumPackage|_|) (weight, height, width, depth) =
        if weight <= maximumMediumWeight 
            && height <= maximumMediumHeight 
            && width <= maximumMediumWidth 
            && depth <= maximumMediumDepth 
            then Some weight
        else None

    let private (|HeavyLargePackage|_|) (weight:float<g>, height:float<cm>, width:float<cm>, depth:float<cm>) =
        let volume = height * width * depth / 1000.0
        if float weight > float volume
            then Some(weight)
        else None

    let private (|LightLargePackage|_|) (weight:float<g>, height:float<cm>, width:float<cm>, depth:float<cm>) =
        let volume = height * width * depth / 1000.0
        if float weight <= float volume
            then Some(volume)
        else None

    let private postageInGbp weight height width depth =
        match (weight, height, width, depth) with
        | SmallPackage _ -> smallPackagePrice 
        | MediumPackage weight -> weight |> float |> (*) 4.0<gbp>
        | HeavyLargePackage weight -> weight |> float |> (*) 6.0<gbp>
        | LightLargePackage volume -> volume |> float |> (*) 6.0<gbp>
        | _ -> 0.0<gbp>

    let private convertTo currency priceInGbp = 
        match currency with
            | EUR -> priceInGbp |> withCommission |> eur.fromGbp |> eur.asMoney
            | CHF -> priceInGbp |> withCommission |> chf.fromGbp |> chf.asMoney
            | _ -> priceInGbp |> gbp.asMoney

    let calculate weight height width depth currency =
        postageInGbp weight height width depth |> convertTo currency