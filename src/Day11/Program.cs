// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day11;

var inputLines = File.ReadAllLines("input.txt");

var stopWatch = new Stopwatch();
stopWatch.Start();
var extraColumns = new Dictionary<int, int>();
var extraRows = new Dictionary<int, int>();
var galaxies = CreateGalaxies(inputLines).ToArray();
long result = CountAllDifferences(1);

stopWatch.Stop();
Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
stopWatch.Restart();

result = CountAllDifferences(1000000);
Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");

return;


IEnumerable<Galaxy> CreateGalaxies(string[] lines)
{
    extraColumns = new Dictionary<int, int>();
    var extraColumn = 0;
    for (var column = 0; column < lines[0].Length; column++)
    {
        if (lines.Any(row => row[column] == '#'))
        {
            extraColumns[column] = extraColumn;
        }
        else
        {
            extraColumn++;
            extraColumns[column] = extraColumn;
        }
    }

    var extraLine = 0;
    for (var row = 0; row < lines.Length; row++)
    {
        var line = lines[row];
        if (line.All(item => item == '.'))
        {
            extraLine++;
            extraRows[row] = extraLine;
            continue;
        }

        extraRows[row] = extraLine;

        for (var column = 0; column < line.Length; column++)
        {
            if (line[column] == '#')
            {
                yield return new Galaxy(column, row);
            }
        }
    }
}

long CountAllDifferences(int extraValue)
{
    long res = 0;
    for (var i = 0; i < galaxies.Length - 1; i++)
    {
        var currentGalaxy = galaxies[i];
        for (var j = i + 1; j < galaxies.Length; j++)
        {
            res += currentGalaxy.GetDifference(galaxies[j], extraRows, extraColumns, extraValue);
        }
    }

    return res;
}