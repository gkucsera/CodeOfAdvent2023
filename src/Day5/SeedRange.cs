namespace Day5;

public struct SeedRange
{
    public long Start { get; }
    public long Range { get; }

    public SeedRange(long start, long range)
    {
        Start = start;
        Range = range;
    }
};