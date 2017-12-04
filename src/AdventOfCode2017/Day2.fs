module Day2


let Problem1 =
    
    let input = LargeInputs.Day2
    
    let lines = 
        input |> Common.Lines
    
    let linediff (line : string) =
    
        let numbers = 
            line 
            |> Common.Words 
            |> Array.map int
    
        let max = 
            numbers |> Array.max
    
        let min = 
            numbers |> Array.min
        
        max - min

    lines |> Array.sumBy linediff


let Problem2 =
    
    let input = LargeInputs.Day2
    
    let lines = 
        input |> Common.Lines
    
    let lineresult (line : string) =
        
        let numbers =
            line
            |> Common.Words
            |> Array.map int
        
        let check (num1 : int) (num2 : int) =
            if num1 % num2 = 0 && num1 > num2 then
                num1 / num2
            else
                0
        
        numbers
        |> Array.sumBy (fun a -> numbers |> Array.sumBy (check a))
    
    lines |> Array.sumBy lineresult