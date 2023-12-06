namespace Day6;

public record Race(long Time, long Distance)
{
    public long GetWinningCount()
    {
        var winning = 0;
        for (long speed = 0; speed <= Time; speed++)
        {
            var travelTime = Time - speed;
            var travelDistance = travelTime *  speed;
            if (travelDistance > Distance)
            {
                winning++;
            }
        }

        return winning;
    }
}