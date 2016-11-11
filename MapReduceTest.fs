module MapReduceTest

open NUnit.Framework
open FsUnit

let User1 = "usr1"
let User2 = "usr2"

[<Test>]
let ``Reducing [(User1, 1); (User2, 1); (User1, 1)] should produce Map((User1, 2),(User2, 1))``() = 
    let input = [(User1, 1); (User2, 1); (User1, 1)]
    let expectOutput = Map.empty.Add(User1, 2).Add(User2, 1)
    MapReduce.reduceFile input |> should equal expectOutput