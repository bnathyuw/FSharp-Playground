namespace FSharpPlayground.Tests
open NUnit.Framework

[<Measure>] type gbp
[<Measure>] type eur
[<Measure>] type chf
type Currency = GBP | EUR | CHF
[<Measure>] type cm
[<Measure>] type g

[<TestFixture>]
module ``Postage calculator should`` = 
    let smallPackagePrice = 120.0<gbp>
    [<Literal>] 
    let maximumSmallWeight = 60<g>
    [<Literal>]
    let maximumSmallHeight = 229<cm>
    [<Literal>]
    let maximumSmallWidth = 162<cm>
    [<Literal>]
    let maximumSmallDepth = 25<cm>
    let inEur = (*) 1.25<eur/gbp>
    let inChf = (*) 1.36<chf/gbp>
    let withCommission = (+) 200.0<gbp>

    let calculate weight height width depth currency =
        match currency with
            | EUR -> (smallPackagePrice |> withCommission |> inEur) / 1.0<eur> //Hack
            | CHF -> (smallPackagePrice |> withCommission |> inChf) / 1.0<chf> //Hack
            | _ -> smallPackagePrice / 1.0<gbp> //Hack

    [<TestCase(1, 1, 1, 1)>]
    [<TestCase(maximumSmallWeight, 1, 1, 1)>]
    [<TestCase(1, 1, 1, 1)>]
    [<TestCase(1, maximumSmallWeight, 1, 1)>]
    [<TestCase(1, 1, maximumSmallHeight, 1)>]
    [<TestCase(1, 1, 1, maximumSmallDepth)>]
    [<TestCase(maximumSmallWeight, maximumSmallWeight, maximumSmallWidth, maximumSmallDepth)>]
    let ``Charge a flat rate for a small package``(weight, height, width, depth) =
        Assert.That((calculate weight height width depth GBP), Is.EqualTo(smallPackagePrice))

    [<Test>]
    let ``Add commission to EUR prices``() =
        Assert.That((calculate 1 1 1 1 EUR), Is.EqualTo(smallPackagePrice |> withCommission |> inEur))

    [<Test>]
    let ``Add commission to CHF prices``() =
        Assert.That((calculate 1 1 1 1 CHF), Is.EqualTo(smallPackagePrice |> withCommission |> inChf))