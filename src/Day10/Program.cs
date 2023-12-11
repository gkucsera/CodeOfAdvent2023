// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day10;

var inputLines = File.ReadAllLines("input.txt");
var grid = CreateGrid(inputLines);

var stopWatch = new Stopwatch();
stopWatch.Start();

var result = grid.GetFarthestPosition();

stopWatch.Stop();

Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");

stopWatch.Reset();
stopWatch.Start();
// result = grid.GetEnclosedCount();
stopWatch.Stop();
Console.WriteLine($"second - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
return;

Grid CreateGrid(string[] lines)
{
    var height = lines.Length;
    var width = lines[0].Length;
    var pipes = new Pipe[height, width];
    Pipe start = null;
    var id = 0;
    for (var i = 0; i < lines.Length; i++)
    {
        for (var j = 0; j < lines[i].Length; j++)
        {
            var type = ParseType(lines[i][j]);
            pipes[i, j] = new Pipe(type, new Position(j, i), id++);
            if (type == PipeType.Start)
            {
                start = pipes[i, j];
            }
        }
    }

    return new Grid(pipes, start ?? throw new Exception("start missing"));
}

PipeType ParseType(char character) =>
    character switch
    {
        'J' => PipeType.TopLeft,
        'L' => PipeType.TopRight,
        '7' => PipeType.BottomLeft,
        'F' => PipeType.BottomRight,
        '|' => PipeType.Vertical,
        '-' => PipeType.Horizontal,
        'S' => PipeType.Start,
        '.' => PipeType.None,
        _ => throw new Exception($"unknown character: {character}")
    };