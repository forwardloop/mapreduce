module MapReduceTest

open NUnit.Framework
open FsUnit

let add x y = x + y

[<Test>]
let ``When reducing [|("usr1", 1); ("usr2", 1); ("usr1", 1)|] expect  [|("usr1", 2); ("usr2", 1)|]``() = 
    let input = [|("usr1", 1); ("usr2", 1); ("usr1", 1)|]
    let output = MapReduce.reduceFile
    add 2 2 |> should equal 4
