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
    public int CountMultipleMoves(char start, char end, string moves)
    {
        var result = 0;
        var move = 0;
        var mapEntries = _mapEntries.Where(item => item.Key.EndsWith(start));

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
}