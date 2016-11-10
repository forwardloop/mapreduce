module MapReduceTest

open NUnit.Framework
open FsUnit

[<Test>]
let ``When reducing [|("usr1", 1); ("usr2", 1); ("usr1", 1)|] expect Map(("usr1", 2),("usr2", 1))``() = 
    let inputArr = [|("usr1", 1); ("usr2", 1); ("usr1", 1)|]
    let expect = Map.empty.Add("usr1", 2).Add("usr2", 1)
    MapReduce.reduceFile inputArr |> should equal expect
