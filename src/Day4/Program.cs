// See https://aka.ms/new-console-template for more information

using Day4;

const string splitCharacter = ": ";
const string numbersSplit = " | ";
const char numbersSeparate = ' ';

var inputLines = File.ReadAllLines("input.txt");

var games = inputLines.Select(ParseGame).ToArray();
var totalPoints = games.Select(item => item.GetPoints()).Sum();

Console.WriteLine($"first: {totalPoints}");


for (var index = 0; index < games.Length; index++)
{
    var currentGame = games[index];
    var currentGameCopyCount = games[index].CopyCount;
    var currentGameValue = currentGame.GetMatching();

    for (var copyIndex = index + 1; copyIndex <= index + currentGameValue && copyIndex < games.Length; copyIndex++)
    {
        games[copyIndex].AddCopies(currentGameCopyCount);
    }
}

var totalNumberOfGames = games.Select(item => item.CopyCount).Sum();
Console.WriteLine($"second: {totalNumberOfGames}");

return;

Game ParseGame(string input)
{
    var gameData = input.Split(splitCharacter)[1].Split(numbersSplit);
    var winningNumbers = gameData[0].Split(numbersSeparate).Where(item => !string.IsNullOrWhiteSpace(item)).Select(int.Parse);
    var playerNumbers = gameData[1].Split(numbersSeparate).Where(item => !string.IsNullOrWhiteSpace(item)).Select(int.Parse);

    return new Game(winningNumbers, playerNumbers);
}

