namespace Day11;

public class Galaxy
{
    private const int ExtraValue = 10;
    private int PosX { get; }
    private int PosY { get; }

    public Galaxy(int posX, int posY)
    {
        PosX = posX;
        PosY = posY;
    }

    public int GetDifference(Galaxy other, Dictionary<int, int> extraRows, Dictionary<int, int> extraColumns)
    {
        var extraWidth = Math.Abs(extraColumns[other.PosX] - extraColumns[PosX]);
        var extraHeight = Math.Abs(extraRows[other.PosY] - extraRows[PosY]);
        var height =extraHeight + Math.Abs(other.PosX - PosX);
        var width = extraWidth + Math.Abs(other.PosY - PosY);
        return height + width;
    }
    
    public long GetDifference(Galaxy other, Dictionary<int, int> extraRows, Dictionary<int, int> extraColumns, long extraValue)
    {
        if (extraValue == 1)
        {
            return GetDifference(other, extraRows, extraColumns);
        }
        
        var extraWidth = Math.Abs(extraColumns[other.PosX] - extraColumns[PosX]);
        var extraHeight = Math.Abs(extraRows[other.PosY] - extraRows[PosY]);
        var width = Math.Abs(other.PosX - PosX) - extraWidth;
        var height = Math.Abs(other.PosY - PosY) - extraHeight;
        return height + width + extraWidth * extraValue + extraHeight  * extraValue;
    }
}