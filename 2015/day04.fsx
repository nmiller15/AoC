// Advent of Code 2015 - Day 4
#load "../AoC.Lib/Input.fs"

open AoC.Lib
open System
open System.Text
open System.Security

let log (str: string) =
    Console.Write($"\r{str}")
    System.Console.Out.Flush()
    str

module MD5 =
    let checkHashForFiveZeros (hash: string) =
        if hash.StartsWith("00000") then
            log hash |> ignore
            printf "\n"
            Ok hash
        else
            Error "False"

    let checkHashForSixZeros (hash: string) =
        if hash.StartsWith("000000") then
            log hash |> ignore
            printf "\n"
            Ok hash
        else
            Error "False"


    let hash (str: string) =
        str
        |> Encoding.UTF8.GetBytes
        |> Cryptography.MD5.HashData
        |> Convert.ToHexString
        |> log

// Your code here
let secretKey = Input.read "Inputs/day04.txt" |> _.Trim()
let hashInput (num: int) = secretKey + (string num)

let rec findInput (num: int) (checker: string -> Result<'string, 'string>) =
    let valid = num |> hashInput |> MD5.hash |> checker
    if valid.IsOk then num else findInput (num + 1) checker


let main =
    // A little brute force...

    let part1 = findInput 0 MD5.checkHashForFiveZeros
    printfn "Part 1: %i" part1

    let part2 = findInput 0 MD5.checkHashForSixZeros
    printfn "Part 2: %i" part2

    ()

main
