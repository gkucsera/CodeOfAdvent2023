var numberStrings = new (string name, int value)[]
{
    ("one", 1),
    ("two", 2),
    ("three", 3),
    ("four", 4),
    ("five", 5),
    ("six", 6),
    ("seven", 7),
    ("eight", 8),
    ("nine", 9)
};
var inputLines = File.ReadLines("input.txt").ToList();
First();
Second();
return;

void First()
{
    var totalSum = 0;
    foreach (var inputLine in inputLines)
    {
        var first = inputLine.First(char.IsNumber);
        var last = inputLine.Reverse().First(char.IsNumber);
        var currentNumber = int.Parse(string.Join("", first, last));
        totalSum += currentNumber;
    }

    Console.WriteLine($"First part: {totalSum}");
}


void Second()
{
    var totalSum = 0;
    foreach (var inputLine in inputLines)
    {
        var firstNumberCharacter = inputLine.First(char.IsNumber);
        var firstNumberPosition = inputLine.IndexOf(firstNumberCharacter);

        var firstNumberString = numberStrings.Select(item =>new { index = inputLine.IndexOf(item.name, StringComparison.Ordinal), item.value })
            .Where(item => item.index != -1)
            .MinBy(item => item.index);
        int firstNumber;
        if (firstNumberString == null || firstNumberPosition < firstNumberString.index)
        {
            firstNumber = int.Parse(firstNumberCharacter.ToString()) * 10;
        } 
        else if (firstNumberString.index < firstNumberPosition)
        {
            firstNumber = firstNumberString.value * 10;
        }
        else
        {
            throw new Exception();
        } 
        
        var secondNumberCharacter = inputLine.Last(char.IsNumber);
        var secondNumberPosition = inputLine.LastIndexOf(secondNumberCharacter);

        var secondNumberString = numberStrings.Select(item =>new { index = inputLine.LastIndexOf(item.name, StringComparison.Ordinal), item.value })
            .Where(item => item.index != -1)
            .MaxBy(item => item.index);
        int secondNumber;
        if (secondNumberString == null || secondNumberPosition > secondNumberString.index)
        {
            secondNumber = int.Parse(secondNumberCharacter.ToString());
        } 
        else if (secondNumberString.index > secondNumberPosition)
        {
            secondNumber = secondNumberString.value;
        }
        else
        {
            throw new Exception();
        }

        var currentNumber = firstNumber + secondNumber;
        totalSum += currentNumber;
    }

    Console.WriteLine($"Second part: {totalSum}");
    Console.ReadKey();
}
