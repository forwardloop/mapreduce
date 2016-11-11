module MapReduce

open System
open System.IO
open System.Collections.Generic

// Map 
let mapUserTransactions datasetFile =
  let fileReader datasetFile = 
    seq { use fileReader = new StreamReader(File.OpenRead(datasetFile))
      while not fileReader.EndOfStream do
        yield fileReader.ReadLine() }    

  let parseLines = 
    let line = fileReader datasetFile 
    line
    |> Seq.filter (fun line -> not (line.StartsWith("#")))
    |> Seq.map (fun line -> line.Split [|','|])
    |> Seq.map (fun line -> line.[0], Int32.Parse line.[1])
  parseLines

// Reduce 
let reduceFile userTransactions = 
  Seq.fold
    (fun (acc : Map<string, int>) ((user, num) : string * int) ->
      let total = match acc |> Map.tryFind user with
                  | Some x -> x
                  | None -> 0  
      Map.add user (total + num) acc)  
    Map.empty
    userTransactions

// Print
let usersWithHighestTransactions inputFile = 
  let sortedResults = 
    mapUserTransactions inputFile
    |> reduceFile 
    |> Map.toSeq
    |> Seq.sortBy (fun (user, total) -> -total) 
    |> Seq.take 5
  sortedResults
  |> Seq.iter(fun (user, total) ->
    printfn "%s, %d" user total);;