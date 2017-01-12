namespace FSharpPlayground.Tests
open NUnit.Framework
open FSharpPlayground.Tests

[<TestFixture>]
module ``Postage calculator should`` = 
    let smallPackagePrice = 120.0<gbp>
    [<Literal>] 
    let MaximumSmallWeight = 60.0<g>
    [<Literal>]
    let MaximumSmallHeight = 229.0<cm>
    [<Literal>]
    let MaximumSmallWidth = 162.0<cm>
    [<Literal>]
    let MaximumSmallDepth = 25.0<cm>
    [<Literal>]
    let MinimumMediumWeight = 61.0<g>
    [<Literal>]
    let MinimumMediumHeight = 230.0<g>
    [<Literal>]
    let MinimumMediumWidth = 163.0<g>
    [<Literal>]
    let MinimumMediumDepth = 26.0<g>
    [<Literal>] 
    let MaximumMediumWeight = 500.0<g>
    [<Literal>]
    let MaximumMediumHeight = 324.0<cm>
    [<Literal>]
    let MaximumMediumWidth = 229.0<cm>
    [<Literal>]
    let MaximumMediumDepth = 100.0<cm>
    [<Literal>] 
    let MinimumLargeWeight = 501.0<g>
    [<Literal>]
    let MinimumLargeHeight = 325.0<cm>
    [<Literal>]
    let MinimumLargeWidth = 230.0<cm>
    [<Literal>]
    let MinimumLargeDepth = 101.0<cm>

    let inEur = (*) 1.25<eur/gbp> >> Eur
    let inChf = (*) 1.36<chf/gbp> >> Chf
    let withCommission = (+) 200.0<gbp>

    [<TestCase(1, 1, 1, 1)>]
    [<TestCase(MaximumSmallWeight, 1, 1, 1)>]
    [<TestCase(1, MaximumSmallHeight, 1, 1)>]
    [<TestCase(1, 1, MaximumSmallWidth, 1)>]
    [<TestCase(1, 1, 1, MaximumSmallDepth)>]
    [<TestCase(MaximumSmallWeight, MaximumSmallHeight, MaximumSmallWidth, MaximumSmallDepth)>]
    let ``Charge a flat rate for a small package``(weight, height, width, depth) =
        Assert.That((PostageCalculator.calculate weight height width depth GBP), Is.EqualTo(Gbp smallPackagePrice))

    [<TestCase(MinimumMediumWeight, 1, 1, 1)>]
    [<TestCase(1, MinimumMediumHeight, 1, 1)>]
    [<TestCase(1, 1, MinimumMediumWidth, 1)>]
    [<TestCase(1, 1, 1, MinimumMediumDepth)>]
    [<TestCase(MinimumMediumWeight, MinimumMediumHeight, MinimumMediumWidth, MinimumMediumDepth)>]
    [<TestCase(MaximumMediumWeight, MinimumMediumHeight, MinimumMediumWidth, MinimumMediumDepth)>]
    [<TestCase(MinimumMediumWeight, MaximumMediumHeight, MinimumMediumWidth, MinimumMediumDepth)>]
    [<TestCase(MinimumMediumWeight, MinimumMediumHeight, MaximumMediumWidth, MinimumMediumDepth)>]
    [<TestCase(MinimumMediumWeight, MinimumMediumHeight, MinimumMediumWidth, MaximumMediumDepth)>]
    [<TestCase(MaximumMediumWeight, MaximumMediumHeight, MaximumMediumWidth, MaximumMediumDepth)>]
    let ``Price a medium package by weight``(weight, height, width, depth) =
        Assert.That((PostageCalculator.calculate weight height width depth GBP), Is.EqualTo(weight * 4.0<gbp/g> |> Gbp))

    [<TestCase(MinimumLargeWeight, 1, 1, 1)>]
    [<TestCase(1, MinimumLargeHeight, 1, 1)>]
    [<TestCase(1, 1, MinimumLargeWidth, 1)>]
    [<TestCase(1, 1, 1, MinimumLargeDepth)>]
    let ``Price a large heavy package by weight``(weight, height, width, depth) =
        Assert.That((PostageCalculator.calculate weight height width depth GBP), Is.EqualTo(weight * 6.0<gbp/g> |> Gbp))

    [<TestCase(1, 1001, 1, 1)>]
    [<TestCase(1, 1, 1001, 1)>]
    [<TestCase(1, 1, 1, 1001)>]
    let ``Price a large light package by volume``(weight, height, width, depth) =
        let multiplier = height * width * depth / 1000.0<cm ^ 3>
        Assert.That((PostageCalculator.calculate weight height width depth GBP), Is.EqualTo(height * width * depth / 1000.0<cm ^ 3> * 6.0<gbp> |> Gbp))

    [<Test>]
    let ``Add commission to EUR prices``() =
        Assert.That((PostageCalculator.calculate 1.0<g> 1.0<cm> 1.0<cm> 1.0<cm> EUR), Is.EqualTo(smallPackagePrice |> withCommission |> inEur))

    [<Test>]
    let ``Add commission to CHF prices``() =
        Assert.That((PostageCalculator.calculate 1.0<g> 1.0<cm> 1.0<cm> 1.0<cm> CHF), Is.EqualTo(smallPackagePrice |> withCommission |> inChf))