module Day3

let Problem1 =
    let input = 289326
    let looplength num =
        if num = 0 then 
            1
        else
            (num * 2) * 4
    let loop num =
        if num = 1 then
            [1]
        else
            let max = num * 2
            let min = num
            let count = looplength num
            let rec s (num : int) (dir : int) = seq {
                let newdir =
                    if dir = 1 then
                        if num = max then -1 else 1
                    else
                        if num = min then 1 else -1
                yield num
                yield! s (num + newdir) newdir
            }        
            s (max - 1) -1 |> Seq.take count |> Seq.toList
    let looptotallengths upto =
        let rec s (num : int) (total: int) = seq {
            let this =
                looplength num
            let newtotal =
                total + this
            yield newtotal
            if newtotal < upto then 
                yield! s (num + 1) newtotal
        }
        s 0 0 |> Seq.toList
    let activeloops =
        looptotallengths input
    let activeloop =
        loop ((activeloops |> List.length) - 1)
    let positioninloop =
        activeloops |> List.takeWhile (fun a -> a < input) |> List.max |> (fun e -> input - e)
    let steps =
        activeloop |> List.skip (positioninloop - 1) |> List.head
    steps
               
