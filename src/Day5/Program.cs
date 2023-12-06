// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using Day5;

var inputLines = File.ReadAllLines("input.txt");
var seeds = CreateSeeds(inputLines[0]);
var maps = CreateMaps(inputLines).ToList();

var destinations = seeds.Select(seed => GetSeedDestination(seed, maps)).ToList();

Console.WriteLine($"first: {destinations.Min()}");

var seedRanges = CreateSeedRanges(inputLines[0]);

var rangeDestinations = new ConcurrentBag<long>();
Parallel.ForEach(seedRanges, seedRange =>
{
    var minDestination = long.MaxValue;
        for(var seed = seedRange.Start; seed < seedRange.Start + seedRange.Range; seed++)
    {
        var destination = GetSeedDestination(seed, maps);
        if (destination < minDestination)
        {
            minDestination = destination;
        }
    }
    rangeDestinations.Add(minDestination);
});

Console.WriteLine($"second: {rangeDestinations.Min()}");

return;

IEnumerable<long> CreateSeeds(string line) => line.Split(':')[1].Trim().Split(' ').Select(long.Parse);

IEnumerable<SeedRange> CreateSeedRanges(string line)
{
    var split = line.Split(':')[1].Trim().Split(" ");
    for (var index = 0; index < split.Length; index += 2)
    {
        var start = long.Parse(split[index]);
        var count = long.Parse(split[index + 1]);
        yield return new SeedRange(start, count);
    }
}

IEnumerable<Map> CreateMaps(IEnumerable<string> lines)
{
    var mapEntries = new List<MapEntry>();
    foreach (var line in lines.Skip(3))
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        if (line.Contains("-to-"))
        {
            yield return new Map(mapEntries);
            mapEntries = new List<MapEntry>();
            continue;
        }

        var splitMap = line.Split(" ");
        var source = long.Parse(splitMap[1]);
        var destination = long.Parse(splitMap[0]);
        var range = long.Parse(splitMap[2]);
        mapEntries.Add(new MapEntry(source, destination, range));
    }

    yield return new Map(mapEntries);
}

long GetSeedDestination(long i, List<Map> list)
{
    var input = i;
    var destination = i;
    foreach (var map in list)
    {
        destination = map.GetDestination(input);
        input = destination;
    }

    return destination;
}