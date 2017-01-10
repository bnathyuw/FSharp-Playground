namespace FSharpPlayground.Tests

module Package =
    let private maximumSmallWeight = 60.0<g>
    let private maximumSmallHeight = 229.0<cm>
    let private maximumSmallWidth = 162.0<cm>
    let private maximumSmallDepth = 25.0<cm>
    let private maximumMediumWeight = 500.0<g>
    let private maximumMediumHeight = 324.0<cm>
    let private maximumMediumWidth = 229.0<cm>
    let private maximumMediumDepth = 100.0<cm>

    type T = { Weight:float<g>; Height:float<cm>; Width:float<cm>; Depth:float<cm> }
    
    let create weight height width depth =
        { Weight = weight; Height = height; Width = width; Depth = depth }
    
    let isSmall package = 
        package.Weight <= maximumSmallWeight 
            && package.Height <= maximumSmallHeight 
            && package.Width <= maximumSmallWidth 
            && package.Depth <= maximumSmallDepth

    let isMedium package =
        package.Weight <= maximumMediumWeight 
            && package.Height <= maximumMediumHeight 
            && package.Width <= maximumMediumWidth 
            && package.Depth <= maximumMediumDepth

    let volume package = package.Height * package.Width * package.Depth / 1000.0

    let isLargeLight package =
        package.Weight / 1.0<g> <= (package |> volume) / 1.0<cm ^ 3>
