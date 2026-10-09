open System
open System.Net.Http
open System.IO

let inputPath year day =
    Path.Combine(year.ToString(), "Inputs", $"day{day:D2}.txt")

let scriptPath year day =
    Path.Combine(year.ToString(), $"day{day:D2}.fsx")

let url year day =
    $"https://adventofcode.com/{year}/day/{day}/input"

let session =
    Environment.GetEnvironmentVariable("AOC_SESSION")
    |> Option.ofObj
    |> Option.defaultWith (fun () -> failwith "AOC_SESSION environment variable is not set.")

let args = fsi.CommandLineArgs |> Array.skip 1

let year =
    args
    |> Array.tryItem 0
    |> Option.map int
    |> Option.defaultValue DateTime.Now.Year

let day =
    args
    |> Array.tryItem 1
    |> Option.map int
    |> Option.defaultValue DateTime.Now.Day

if File.Exists(inputPath year day) then
    failwith "Input path already exists."

if File.Exists(scriptPath year day) then
    failwith "Script path already exists."

printfn "Downloading input for year %i day %i" year day

let client = new HttpClient()
client.DefaultRequestHeaders.Add("Cookie", $"session={session}")

let input =
    client.GetStringAsync(url year day).Result
    |> Option.ofObj
    |> Option.defaultWith (fun () -> failwith "Failed to fetch input from Advent of Code.")

Directory.CreateDirectory(Path.Combine(year.ToString(), "Inputs")) |> ignore
File.WriteAllText(inputPath year day, input)

File.WriteAllText(
    scriptPath year day,
    $"// Advent of Code {year} - Day {day}\n#load \"../AoC.Lib/Input.fs\"\nopen AoC.Lib\n\n// Your code here\nlet input = Input.read \"Inputs/day{day:D2}.txt\""
)

printfn "Downloaded... %s" (inputPath year day)
printfn "   Created... %s" (scriptPath year day)
