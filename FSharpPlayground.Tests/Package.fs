namespace FSharpPlayground.Tests

[<Measure>] type cm
[<Measure>] type g
[<Measure>] type ml = cm ^ 3

module Package =
    let private maximumSmallWeight = 60.0<g>
    let private maximumSmallHeight = 229.0<cm>
    let private maximumSmallWidth = 162.0<cm>
    let private maximumSmallDepth = 25.0<cm>
    let private maximumMediumWeight = 500.0<g>
    let private maximumMediumHeight = 324.0<cm>
    let private maximumMediumWidth = 229.0<cm>
    let private maximumMediumDepth = 100.0<cm>

    type T = { Weight:float<g>; Height:float<cm>; Width:float<cm>; Depth:float<cm> } with
         member this.Volume = this.Height * this.Width * this.Depth / 1000.0

    let create weight height width depth =
        { Weight = weight; Height = height; Width = width; Depth = depth }
    
    let private isSmall package = 
        package.Weight <= maximumSmallWeight 
            && package.Height <= maximumSmallHeight 
            && package.Width <= maximumSmallWidth 
            && package.Depth <= maximumSmallDepth

    let private isMedium package =
        package.Weight <= maximumMediumWeight 
            && package.Height <= maximumMediumHeight 
            && package.Width <= maximumMediumWidth 
            && package.Depth <= maximumMediumDepth

    let private isLargeLight package =
        package.Weight / 1.0<g> <= package.Volume / 1.0<ml>

    type Sized = Small | Medium of float<g> | LargeLight of float<ml> | LargeHeavy of float<g>

    let (|Small|Medium|LargeLight|LargeHeavy|) =
        function
            | package when isSmall package -> Small
            | package when isMedium package -> Medium package.Weight
            | package when isLargeLight package -> LargeLight package.Volume
            | package -> LargeHeavy package.Weight