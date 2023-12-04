namespace Day4;

public class Game
{
    private IEnumerable<int> WinningNumbers { get; }
    private IEnumerable<int> PlayerNumbers { get; }
    public  int CopyCount { get; private set; }

    public Game(IEnumerable<int> winningNumbers, IEnumerable<int> playerNumbers)
    {
        WinningNumbers = winningNumbers;
        PlayerNumbers = playerNumbers;
        CopyCount = 1;
    }

    public int GetPoints()
    {
        var matchingNumber = WinningNumbers.Intersect(PlayerNumbers).ToList();
        return matchingNumber.Count < 2 ? matchingNumber.Count : (int)Math.Pow(2, matchingNumber.Count - 1);
    }
    public int GetMatching() => WinningNumbers.Intersect(PlayerNumbers).Count();

    public void AddCopies(int newCopies)
    {
        CopyCount += newCopies;
    }
}