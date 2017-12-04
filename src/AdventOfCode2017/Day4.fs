module Day4


let Problem1 =
    
    let input = 
        LargeInputs.Day4
    
    let lines =
        input |> Common.Lines
    
    let isValid str =
    
        let words =
            str |> Common.Words
        
        let count =
            words |> Array.length
        
        let distinctcount =
            words 
            |> Array.distinct 
            |> Array.length
        
        if count = distinctcount then
            1
        else
            0
    
    lines |> Array.sumBy isValid
    
    
let Problem2 =
    
    let input =
        LargeInputs.Day4
    
    let lines =
        input |> Common.Lines
    
    let isValid str =
    
        let words =
            str |> Common.Words
        
        let sortword w =
            w
            |> Common.Chars
            |> Seq.map int
            |> Seq.sort
            |> Seq.map string
            |> Seq.toArray
            |> fun e -> System.String.Join("", e)
        
        let sortedwords =
            words 
            |> Seq.map sortword
        
        let count =
            sortedwords |> Seq.length
        
        let distinctcount =
            sortedwords 
            |> Seq.distinct 
            |> Seq.length
        
        if count = distinctcount then
            1
        else
            0
    
    lines |> Array.sumBy isValid

