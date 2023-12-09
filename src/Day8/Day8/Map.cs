using System.Numerics;

namespace Day8;

public class Map
{
    private readonly Dictionary<string, MapEntry> _mapEntries;
    public Map(IEnumerable<MapEntry> mapEntries)
    {
        _mapEntries = mapEntries.ToDictionary(item => item.Code, item => item);
    }

    public int CountMoves(string start, string end, string moves)
    {
        var result = 0;
        var move = 0;
        var mapEntry = _mapEntries[start];

        while (mapEntry.Code != end)
        {
            var direction = moves[move];
            mapEntry = direction == 'R' ? _mapEntries[mapEntry.Right] : _mapEntries[mapEntry.Left];
            result++;
            move++;
            move %= moves.Length;
        }

        return result;
    }
    public long CountMultipleMoves(char start, char end, string moves)
    {
        var mapEntries = _mapEntries.Where(item => item.Key.EndsWith(start)).ToList();
        var distances = mapEntries.Select(mapEntry => FindLoop(end, moves, mapEntry.Value)).ToArray();

        var lcm = GetLeastCommonMultiple(distances);

        return lcm;
    }

    private long FindLoop(char end, string moves, MapEntry mapEntry)
    {
        long result = 0;
        var move = 0;

        while (!mapEntry.Code.EndsWith(end))
        {
            var direction = moves[move];
            mapEntry = direction == 'R' ? _mapEntries[mapEntry.Right] : _mapEntries[mapEntry.Left];
            result++;
            move++;
            move %= moves.Length;
        }
        return result;
    }

    private long GetLeastCommonMultiple(long[] numbers)
    {
        var result = numbers[0] * numbers[1] / GetGreatestCommonDivisor(numbers[0], numbers[1]);
        for (var i = 2; i < numbers.Length; i++)
        {
            result = result * numbers[i] / GetGreatestCommonDivisor(result, numbers[i]);
        }

        return result;
    }
    
    private int GetGreatestCommonDivisor(long one, long other)
    {
        return (int)BigInteger.GreatestCommonDivisor(new BigInteger(one), new BigInteger(other));
    }
}