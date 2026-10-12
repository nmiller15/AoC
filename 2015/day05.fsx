// Advent of Code 2015 - Day 5
#load "../AoC.Lib/Input.fs"
open AoC.Lib

// Your code here
module NiceStrings =
    let vowels = [ 'a'; 'e'; 'i'; 'o'; 'u' ]
    let forbidden = [ "ab"; "cd"; "pq"; "xy" ]

    let isVowel (c: char) = vowels |> List.contains c

    let hasDouble (str: string) =
        str |> Seq.pairwise |> Seq.exists (fun (a, b) -> a = b)

    let threeVowels (str: string) =
        str |> Seq.filter isVowel |> Seq.length >= 3

    let doesNotContain (str: string) (check: string) = str.Contains(check) |> not

    let hasNoForbiddenSubstr (str: string) =
        forbidden |> List.forall (doesNotContain str)

    let isNice (str: string) : bool =
        threeVowels str && hasDouble str && hasNoForbiddenSubstr str

    let containsPair (remainingPairs: (char * char) list) (pair: char * char) : bool =
        remainingPairs |> List.contains pair

    let pairsMinusFirst (pairs: (char * char) list) =
        if pairs.Length < 2 then [] else List.skip 1 pairs

    let rec hasNonOverlappingDoublePair pairs =
        match pairs with
        | [] -> false
        | head :: tail ->
            List.contains head (pairsMinusFirst tail)
            || hasNonOverlappingDoublePair tail

    let hasPairWithOneLetterBetween str =
        str
        |> Seq.windowed 3
        |> Seq.exists (fun window -> window.[0] = window.[2])

    let isNiceTwice str =
        let pairs = str |> Seq.pairwise |> Seq.toList

        hasNonOverlappingDoublePair pairs
        && hasPairWithOneLetterBetween str


let main =
    let input = Input.readLines "Inputs/day05.txt"

    let part1 = input |> List.filter NiceStrings.isNice |> List.length

    printfn "Part 1: %i" part1

    let part2 = input |> List.filter NiceStrings.isNiceTwice |> List.length

    printfn "Part 2: %i" part2

    ()

main
