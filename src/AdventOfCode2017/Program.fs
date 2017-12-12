[<EntryPoint>]
let main argv = 

    let x = Day12.Problem2
        
    printfn "%A" x

    System.Diagnostics.Trace.WriteLine(x.ToString())

    System.Console.ReadKey() |> ignore

    0