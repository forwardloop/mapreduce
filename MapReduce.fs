module MapReduce

open System
open System.IO
open System.Collections.Generic

// Map 
let mapDriverParameters datasetFile =
  let fileReader datasetFile = 
    seq { use fileReader = new StreamReader(File.OpenRead(datasetFile))
      while not fileReader.EndOfStream do
        yield fileReader.ReadLine() }    

  let parseLines = 
    let line = fileReader datasetFile 
    line
    |> Seq.filter (fun line -> not (line.StartsWith("#")))
    |> Seq.map (fun line -> line.Split [|','|])
    |> Seq.map (fun line -> line.[0], Double.Parse line.[1])
  parseLines

// Reduce 
let reduceFile driverParameters = 
  Seq.fold
    (fun (acc : Map<string, double * int>) ((driver, num) : string * double) ->
      let (total, count) = match acc |> Map.tryFind driver with
                  | Some (x, y) -> (x, y)
                  | None -> (0.0, 0) 
      Map.add driver (total + num, count + 1) acc)  
    Map.empty
    driverParameters

// Print
let driversWithHighestParameterAverages inputFile = 
  let sortedResults = 
    mapDriverParameters inputFile
    |> reduceFile 
    |> Map.toSeq
    |> Seq.map (fun (driver, (total, count)) -> (driver, total/(double)count)) 
    |> Seq.sortBy (fun (driver, paramAvg) -> -paramAvg) 
    |> Seq.take 3
  sortedResults
  |> Seq.iter(fun (driver, total) ->
    printfn "%s, %f" driver total);;