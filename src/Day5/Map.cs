namespace Day5;

public record Map(IEnumerable<MapEntry> MapEntries)
{
    public long GetDestination(long source)
    {
        foreach (var mapEntry in MapEntries)
        {
            var result = mapEntry.GetDestination(source);
            if (result.HasValue)
            {
                return result.Value;
            }
        }

        return source;
    }
}