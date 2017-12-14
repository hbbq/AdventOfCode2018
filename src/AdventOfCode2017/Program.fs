[<EntryPoint>]
let main argv = 

    let x = Day14.Problem2
        
    printfn "%A" x

    System.Diagnostics.Trace.WriteLine(x.ToString())

    System.Console.ReadKey() |> ignore

    0
