module Day1


let Problem1 =

    let input = LargeInputs.Day1

    let words = input |> Common.Lines
    
    let total = words |> Array.sumBy int

    total

    

let Problem2 =
    
    let input = LargeInputs.Day1

    input |> Common.Lines |> Array.map int |> csharp.Day1.Problem1
