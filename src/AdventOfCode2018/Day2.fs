module Day2


let Problem1 =

    //let input = "abcdef bababc abbcde abcccd aabcdd abcdee ababab" |> Common.Words

    let input = LargeInputs.Day2 |> Common.Lines

    let hasCount cnt v =
        let chars = v |> Common.Chars
        let unique = chars |> Seq.distinct
        let count c =
            chars |> Seq.filter (fun f -> f = c) |> Seq.length
        let counts = unique |> Seq.map count
        counts |> Seq.exists (fun f -> f = cnt)

    let twos = input |> Seq.filter (hasCount 2) |> Seq.length
    let threes = input |> Seq.filter (hasCount 3) |> Seq.length

    twos * threes

let Problem2 =

    //let input = "abcdef bababc abbcde abcccd aabcdd abcdee ababab" |> Common.Words

    let input = LargeInputs.Day2 |> Common.Lines

    let closeEnough a b =
        let ac = a |> Common.Chars |> Seq.toArray
        let bc = b |> Common.Chars |> Seq.toArray
        let count = {0..(ac |> Array.length) - 1}
        count |> Seq.filter (fun f -> ac.[f] <> bc.[f]) |> Seq.length = 1

    let test seq =
        let tests = input |> Seq.filter (fun f -> f <> seq) |> Seq.tryFind (closeEnough seq)
        tests

    let clear s =
        seq {
            for n in s do
                match n with
                    | Some v -> yield v
                    | None -> ()
        }
        
    let matched = input |> Seq.map test |> clear |> Seq.toArray

    let frst = matched.[0]
    let scnd = matched.[1]
    
    let fc = frst |> Common.Chars |> Seq.toArray
    let sc = scnd |> Common.Chars |> Seq.toArray
    let cc = {0..(fc |> Array.length) - 1}
    cc |> Seq.filter (fun f -> fc.[f] = sc.[f]) |> Seq.map (fun f -> fc.[f] |> string) |> String.concat ""
