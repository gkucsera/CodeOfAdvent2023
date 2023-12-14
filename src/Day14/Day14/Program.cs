// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;

var inputLines = File.ReadAllLines("input.txt");
var map = CreateMap();

Rotate(Rotation.North);
Console.WriteLine($"first: {GetMapValue(map)}");
map = CreateMap();

//second
var cache = new Dictionary<string, long>();
var stopwatch = new Stopwatch();
stopwatch.Start();

var next = Rotation.North;
var found = false;
var index = 1L;
var count = 4_000_000_000;
var extraRound = 0L;
while (!found)
{
    var key = GetStringValue(map);
    if (cache.TryGetValue(key, out var matchedIndex))
    {
        var loopDiff = index - matchedIndex;
        var remaining = count - index;
        extraRound = remaining % loopDiff;
        found = true;
    }
    else
    {
        cache[GetStringValue(map)] = index;
        Rotate(next);
        next += 3;
        next = (Rotation)((int)next % 4);
        index++;
    }
}

for (int i = 0; i < extraRound; i++)
{
    Rotate(next);
    next += 3;
    next = (Rotation)((int)next % 4);
}

stopwatch.Stop();
Console.WriteLine($"second:{GetMapValue(map)} - {stopwatch.Elapsed}");

return;

char[,] CreateMap()
{
    var result = new char[inputLines.Length, inputLines.First().Length];

    for (var column = 0; column < inputLines.First().Length; column++)
    {
        for (var row = 0; row < inputLines.Length; row++)
        {
            var currentChar = inputLines[row][column];
            result[row, column] = currentChar;
        }
    }

    return result;
}

void Rotate(Rotation rotation)
{
    switch (rotation)
    {
        case Rotation.North:
            RotateNorth();
            break;
        case Rotation.East:
            RotateEast();
            break;
        case Rotation.South:
            RotateSouth();
            break;
        case Rotation.West:
            RotateWest();
            break;
    }
}

void RotateNorth()
{
    var emptySpaces = new Queue<int>();
    for (var column = 0; column < map.GetLength(0); column++)
    {
        emptySpaces.Clear();
        for (var row = 0; row < map.GetLength(1); row++)
        {
            var currentChar = map[row, column];
            switch (currentChar)
            {
                case 'O':
                    if (emptySpaces.TryDequeue(out var empty))
                    {
                        map[empty, column] = 'O';
                        map[row, column] = '.';
                        emptySpaces.Enqueue(row);
                    }

                    break;
                case '#':
                    emptySpaces.Clear();
                    break;
                case '.':
                    emptySpaces.Enqueue(row);
                    break;
            }
        }
    }
}

void RotateSouth()
{
    var emptySpaces = new Queue<int>();
    for (var column = 0; column < map.GetLength(0); column++)
    {
        emptySpaces.Clear();
        for (var row = map.GetLength(1) - 1; row >= 0; row--)
        {
            var currentChar = map[row, column];
            switch (currentChar)
            {
                case 'O':
                    if (emptySpaces.TryDequeue(out var empty))
                    {
                        map[empty, column] = 'O';
                        map[row, column] = '.';
                        emptySpaces.Enqueue(row);
                    }
                    break;
                case '#':
                    emptySpaces.Clear();
                    break;
                case '.':
                    emptySpaces.Enqueue(row);
                    break;
            }
        }
    }
}

void RotateEast()
{
    for (var row = 0; row < map.GetLength(1); row++)
    {
        var emptySpaces = new Queue<int>();
        for (var column = map.GetLength(0) - 1; column >= 0; column--)
        {
            var currentChar = map[row, column];
            switch (currentChar)
            {
                case 'O':
                    if (emptySpaces.TryDequeue(out var empty))
                    {
                        map[row, empty] = 'O';
                        map[row, column] = '.';
                        emptySpaces.Enqueue(column);
                    }
                    break;
                case '#':
                    emptySpaces.Clear();
                    break;
                case '.':
                    emptySpaces.Enqueue(column);
                    break;
            }
        }
    }
}

void RotateWest()
{
    for (var row = 0; row < map.GetLength(1); row++)
    {
        var emptySpaces = new Queue<int>();
        for (var column = 0; column < map.GetLength(0); column++)
        {
            var currentChar = map[row, column];
            switch (currentChar)
            {
                case 'O':
                    if (emptySpaces.TryDequeue(out var empty))
                    {
                        map[row, empty] = 'O';
                        map[row, column] = '.';
                        emptySpaces.Enqueue(column);
                    }
                    break;
                case '#':
                    emptySpaces.Clear();
                    break;
                case '.':
                    emptySpaces.Enqueue(column);
                    break;
            }
        }
    }

}

string GetStringValue(char[,] characters)
{
    var stringBuilder = new StringBuilder();
    for (var i = 0; i < characters.GetLength(0); i++)
    {
        for (var j = 0; j < characters.GetLength(1); j++)
        {
            var character = characters[i, j];
            if (character == 0)
            {
                character = '.';
            }

            stringBuilder.Append(character);
        }
    }

    return stringBuilder.ToString();
}
int GetMapValue(char[,] characters)
{
    var ret = 0;
    for (var i = 0; i < characters.GetLength(0); i++)
    {
        for (var j = 0; j < characters.GetLength(1); j++)
        {
            var character = characters[i, j];
            if (character == 'O')
            {
                ret += characters.GetLength(0) - i;
            }
        }
    }

    return ret;
}

enum Rotation
{
    North,
    East,
    South,
    West
}