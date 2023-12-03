// See https://aka.ms/new-console-template for more information

const char idSeparator = ':';
const char bagSeparator = ';';
const char cubeSeparator = ',';

const int maxBlue = 14;
const int maxGreen = 13;
const int maxRed = 12;

var inputLines = File.ReadAllLines("input.txt");
var gameRounds = inputLines.Select(ParseGameRound).ToList();

var possibleRounds = gameRounds.Where(round => round.CubeSets.All(cubeSet => cubeSet.Blue <= maxBlue && cubeSet.Green <= maxGreen && cubeSet.Red <= maxRed));

var firstSolution = possibleRounds.Select(item => item.Id).Sum();
Console.WriteLine($"first: {firstSolution}");

var secondSolution = gameRounds.Select(item => item.GetPower()).Sum();
Console.WriteLine($"second: {secondSolution}");

return;

GameRound ParseGameRound(string input)
{
    var splitIdAndBags = input.Split(idSeparator);
    var idString = splitIdAndBags[0];
    var roundString = splitIdAndBags[1];

    var id = int.Parse(idString.Replace("Game", string.Empty).Trim());

    return new GameRound(id, roundString
        .Split(bagSeparator)
        .Select(ParseBag).ToList());
}

CubeSet ParseBag(string input)
{
    var cubes = input
        .Trim()
        .Split(cubeSeparator)
        .Select(item => item.Trim().Split(" "))
        .Select(item => new { Color = item[1], Value = int.Parse(item[0]) })
        .ToList();

    var blue = cubes.FirstOrDefault(item => item.Color == "blue")?.Value ?? 0;
    var red = cubes.FirstOrDefault(item => item.Color == "red")?.Value ?? 0;
    var green = cubes.FirstOrDefault(item => item.Color == "green")?.Value ?? 0;
    return new CubeSet(blue, red, green);
}

record CubeSet(int Blue, int Red, int Green);

record GameRound(int Id, List<CubeSet> CubeSets)
{
    public int GetPower()
    {
        var blue = CubeSets.MaxBy(item => item.Blue)?.Blue ?? 0;
        var green = CubeSets.MaxBy(item => item.Green)?.Green ?? 0;
        var red = CubeSets.MaxBy(item => item.Red)?.Red ?? 0;

        return blue * green * red;
    }
}