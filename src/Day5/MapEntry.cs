namespace Day5;

public record MapEntry
{
    private long SourceStart { get; }
    private long SourceEnd { get; }
    private long DestinationStart { get; }
    public MapEntry(long source, long destination, long range)
    {
        SourceStart = source;
        SourceEnd = source + range;
        DestinationStart = destination;
    }

    public long? GetDestination(long source)
    {
        if (source >= SourceStart && source < SourceEnd)
        {
            var diff = source - SourceStart;
            return DestinationStart + diff;
        }

        return null;
    }
}