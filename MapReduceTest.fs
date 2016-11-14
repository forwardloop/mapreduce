module MapReduceTest

open NUnit.Framework
open FsUnit

let Driver1 = "Nasr"
let Driver2 = "Sainz"

[<Test>]
let ``Reducing [(Driver1, 1.0); (Driver2, 1.0); (Driver1, 2.0)] should produce Map((Driver1, 1.5),(Driver2, 1.0))``() = 
    let input = [(Driver1, 1.0); (Driver2, 1.0); (Driver1, 2.0)]
    let expectOutput = Map.empty.Add(Driver1, (3.0, 2)).Add(Driver2, (1.0,1))
    MapReduce.reduceFile input |> should equal expectOutput