// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.Diagnostics;
using Day13;

var inputLines = File.ReadAllLines("input.txt");
var maps = CreateMaps().ToList();
var stopWatch = new Stopwatch();
stopWatch.Start();

var result = maps.Select(item => item.GetReflectionCount(false)).Sum();

stopWatch.Stop();
Console.WriteLine($"first - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
stopWatch.Reset();
stopWatch.Start();

var results = new ConcurrentBag<long>();
Parallel.ForEach(maps, map =>
{
    var res = map.GetReflectionCount(true);
    results.Add(res);
});
result = results.Sum();

stopWatch.Stop();
Console.WriteLine($"second - {result} - {stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
return;

IEnumerable<Map> CreateMaps()
{
    var map = new List<string>();
    foreach (var line in inputLines)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            yield return new Map(map.ToArray());
            map.Clear();
            continue;
        }

        map.Add(line);
    }
    yield return new Map(map.ToArray());
}