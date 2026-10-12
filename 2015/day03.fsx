// Advent of Code 2015 - Day 3
#load "../AoC.Lib/Input.fs"
open AoC.Lib

type Coor = { x: int; y: int }
type TwoSantas = { santa: Coor; robo: Coor }

type Instruction =
    | Up
    | Down
    | Left
    | Right

let toInstruction c =
    match c with
    | '^' -> Up
    | 'v' -> Down
    | '<' -> Left
    | '>' -> Right
    | _ -> failwith "Invalid instruction"

let nextCoordinate coor i =
    match i with
    | Up -> { x = coor.x; y = coor.y + 1 }
    | Down -> { x = coor.x; y = coor.y - 1 }
    | Left -> { x = coor.x - 1; y = coor.y }
    | Right -> { x = coor.x + 1; y = coor.y }

let findTwoSantas (santas: TwoSantas) (index, instruction) =
    if index % 2 = 0 then
        { santa = nextCoordinate santas.santa instruction
          robo = santas.robo }
    else
        { santa = santas.santa
          robo = nextCoordinate santas.robo instruction }

let flatten santas = [ santas.santa; santas.robo ]


let main =
    let input = Input.read "Inputs/day03.txt"

    let part1 =
        input
        |> List.ofSeq
        |> List.map toInstruction
        |> List.scan nextCoordinate { x = 0; y = 0 }
        |> List.distinct
        |> List.length

    printfn "Part 1: %d" part1

    let part2 =
        input
        |> List.ofSeq
        |> List.map toInstruction
        |> List.indexed
        |> List.scan
            findTwoSantas
            { santa = { x = 0; y = 0 }
              robo = { x = 0; y = 0 } }
        |> List.collect flatten
        |> List.distinct
        |> List.length

    printfn "Part 2: %d" part2
    ()

main
