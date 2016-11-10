open System
open System.IO
open System.Collections.Generic

// Map 
let inputFile = @"big-dataset.csv"
let mapUserTransactions datasetFile =
  let fileReader datasetFile = 
    seq { use fileReader = new StreamReader(File.OpenRead(datasetFile))
      while not fileReader.EndOfStream do
        yield fileReader.ReadLine() }    

  let parseLines = 
    let line = fileReader inputFile 
    line
    |> Seq.filter (fun line -> not (line.StartsWith("#")))
    |> Seq.map (fun line -> line.Split [|','|])
    |> Seq.map (fun line -> line.[0], Int32.Parse line.[1])
    |> Seq.toArray
  parseLines

// Reduce 
let userTransactions = mapUserTransactions inputFile
let reduceFile = 
  Array.fold
    (fun (acc : Map<string, int>) ((user, num) : string * int) ->
      if Map.containsKey user acc then
        let total = acc.[user]
        Map.add user (total + num) acc
      else
        Map.add user num acc)
    Map.empty
    userTransactions

// Show 5 users w/ highest transasction totals
let topUsersOutput reduceOutput = 
  let sortedResults = 
    reduceFile
    |> Map.toSeq
    |> Seq.sortBy (fun (ip, total) -> -total) 
    |> Seq.take 5
  sortedResults
  |> Seq.iter(fun (user, total) ->
    printfn "%s, %d" user total);;

reduceFile |> topUsersOutput