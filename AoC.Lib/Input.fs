namespace AoC.Lib

module Input =
    let read path = System.IO.File.ReadAllText(path)

    let readLines path = System.IO.File.ReadAllLines(path)


