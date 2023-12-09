// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day8;

const string start = "AAA";
const string end = "ZZZ";

var inputLines = File.ReadAllLines("input.txt");
var moves = inputLines[0];
var mapEntries = CreateMap(inputLines.Skip(2));
var map = new Map(mapEntries);

var stopWatch = new Stopwatch();
stopWatch.Start();

var result = map.CountMoves(start, end, moves);

stopWatch.Stop();

Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
return ;

IEnumerable<MapEntry> CreateMap(IEnumerable<string> lines)
{
    foreach (var line in lines)
    {
        var split = line.Split(" = ");
        var code = split[0];
        var destinations = split[1].Replace("(", string.Empty).Replace(",", string.Empty).Replace(")", string.Empty).Split(" ");
        yield return new MapEntry(code, destinations[1], destinations[0]);
    }
}