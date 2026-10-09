// Advent of Code 2015 - Day 1
#load "../AoC.Lib/Input.fs"
open AoC.Lib

// Your code here
let input = Input.read "Inputs/day01.txt"

let up = input |> String.filter (fun x -> x = '(') |> String.length
let down = input |> String.filter (fun x -> x = ')') |> String.length
let stop = up - down

printfn "Part one: %d" stop

let incrementFloor f = f + 1
let decrementFloor f = f - 1
let stay f = f + 0

let step c =
    if c = '(' then incrementFloor
    else if c = ')' then decrementFloor
    else stay

let isBasement f = f < 0

let day2 =
    input
    |> Seq.toList
    |> List.scan (fun acc c -> (step c) acc) 0
    |> List.findIndex isBasement

printfn "Part two: %i" day2

