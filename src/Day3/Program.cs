// See https://aka.ms/new-console-template for more information

using Day3;

var inputLines = File.ReadAllLines("input.txt");

var partNumbers = inputLines.SelectMany(CreatePartNumbers);

var validPartNumbers = partNumbers.Where(item => item.IsValid(inputLines)).ToList();
Console.WriteLine($"first: {validPartNumbers.Select(item => item.Value).Sum()}");

var gears = inputLines.SelectMany((item, index) => CreateGears(item, index, validPartNumbers));
Console.WriteLine($"second: {gears.Select(item => item.GetGearValue).Sum()}");
return;

IEnumerable<PartNumber> CreatePartNumbers(string inputLine, int line)
{
    var currentPartNumber = string.Empty;
    var startIndex = 0;
    for (var index = 0; index < inputLine.Length; index++)
    {
        if (!char.IsNumber(inputLine[index]))
        {
            if (!string.IsNullOrEmpty(currentPartNumber))
            {
                yield return new PartNumber(int.Parse(currentPartNumber), line, startIndex, index - 1);
                startIndex = 0;
                currentPartNumber = string.Empty;
            }

            continue;
        }

        if (string.IsNullOrEmpty(currentPartNumber))
        {
            startIndex = index;
        }

        currentPartNumber += inputLine[index];
    }

    if (!string.IsNullOrEmpty(currentPartNumber))
    {
        yield return new PartNumber(int.Parse(currentPartNumber), line, startIndex, inputLine.Length - 1);
    }
}

IEnumerable<Gear> CreateGears(string inputLine, int line, List<PartNumber> partNumberList)
{
    for (var index = 0; index < inputLine.Length; index++)
    {
        if (inputLine[index] == Gear.GearSymbol)
        {
            var adjacentPartNumbers = partNumberList.Where(item =>
                {
                    var start = item.StartIndex - 1;
                    var end = item.EndIndex + 1;
                    return
                        (item.Line == line - 1 && start <= index && end >= index) ||
                        (item.Line == line && (start == index || end == index)) ||
                         (item.Line == line + 1 && start <= index && end >= index);
                }
            ).ToList();
            if (adjacentPartNumbers.Count == 2)
            {
                yield return new Gear(adjacentPartNumbers.First(), adjacentPartNumbers.Last());
            }
        }
    }
}