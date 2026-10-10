// Advent of Code 2015 - Day 2
#load "../AoC.Lib/Input.fs"
open AoC.Lib

type Box = { l: int; w: int; h: int }

let area x y = x * y
let perimeter x y = 2 * (x + y)
let volume x y z = x * y * z

module Box =
    let parse (line: string) =
        let arr = line.Split('x') |> Array.map int

        { l = arr[0]; w = arr[1]; h = arr[2] }

    let surfaceArea (box: Box) =
        2 * (area box.l box.w + area box.w box.h + area box.h box.l)

    let extraPaper (box: Box) =
        List.min [ area box.l box.w; area box.w box.h; area box.h box.l ]

    let wrappingPaper (box: Box) = surfaceArea box + extraPaper box

    let smallestPerimeter (box: Box) =
        List.min [ perimeter box.l box.w; perimeter box.w box.h; perimeter box.h box.l ]

    let ribbonLength (box: Box) =
        smallestPerimeter box + volume box.l box.w box.h

let main =
    let input = Input.readLines "Inputs/day02.txt"

    let part1 = input |> List.sumBy (Box.parse >> Box.wrappingPaper)
    printfn "Part 1: %i" part1

    let part2 = input |> List.sumBy (Box.parse >> Box.ribbonLength)
    printfn "Part 2: %i" part2

main
