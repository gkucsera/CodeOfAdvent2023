// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.Diagnostics;
using Day12;

var inputLines = File.ReadAllLines("input.txt");
var springSets = CreateSpringSets().ToList();

var stopWatch = new Stopwatch();
stopWatch.Start();

var result = springSets.Select(item => item.NumberOfValidSets2(false));

stopWatch.Stop();
Console.WriteLine($"first - {result.Sum()} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");

stopWatch.Reset();
stopWatch.Start();

var setResults = new ConcurrentBag<long>();
Parallel.ForEach(springSets, set =>
{
    var setResult = set.NumberOfValidSets2(true);
    setResults.Add(setResult);
});

Console.WriteLine($"second - {setResults.Sum()} - {stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
return ;


IEnumerable<SpringSet> CreateSpringSets()
{
    foreach (var line in inputLines)
    {
        var split = line.Split(" ");
        var springs = split[1].Split(",").Select(int.Parse).ToArray();
        yield return new SpringSet(split[0], springs);
    }
}