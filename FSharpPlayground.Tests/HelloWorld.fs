namespace FSharpPlayground.Tests
open NUnit.Framework



type Currency = Gbp
type Money = { Currency:Currency; Amount:decimal }
[<Measure>] type cm
[<Measure>] type g

[<TestFixture>]
module ``Postage calculator should`` = 
    let smallPackagePrice = 120m
    [<Literal>] 
    let maximumSmallWeight = 60<g>
    [<Literal>]
    let maximumSmallHeight = 229<cm>
    [<Literal>]
    let maximumSmallWidth = 162<cm>
    [<Literal>]
    let maximumSmallDepth = 25<cm>

    let calculate a b c d e = { Currency = Gbp; Amount = smallPackagePrice }

    [<TestCase(1, 1, 1, 1)>]
    [<TestCase(maximumSmallWeight, 1, 1, 1)>]
    [<TestCase(1, 1, 1, 1)>]
    [<TestCase(1, maximumSmallWeight, 1, 1)>]
    [<TestCase(1, 1, maximumSmallHeight, 1)>]
    [<TestCase(1, 1, 1, maximumSmallDepth)>]
    [<TestCase(maximumSmallWeight, maximumSmallWeight, maximumSmallWidth, maximumSmallDepth)>]
    let ``Charge a flat rate for a small package``(weight, height, width, depth) =
        Assert.That((calculate weight height width depth Gbp), Is.EqualTo({ Currency = Gbp; Amount = smallPackagePrice }))