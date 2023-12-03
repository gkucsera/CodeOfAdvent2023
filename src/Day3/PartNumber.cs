namespace Day3
{
    public record PartNumber(int Value, int Line, int StartIndex, int EndIndex)
    {
        const char PeriodSymbol = '.';
    
        public bool IsValid(string[] lines)
        {
            return CheckTop(lines) || CheckLeft(lines) || CheckRight(lines) || CheckBottom(lines);
        }

        private bool CheckTop(string[] lines)
        {
            if (Line == 0)
            {
                return false;
            }

            var upperLine = lines[Line - 1];
            var start = StartIndex == 0 ? 0 : StartIndex - 1;

            var end = upperLine.Length > EndIndex + 1 ? EndIndex + 1 : EndIndex;

            for (var i = start; i < end + 1; i++)
            {
                if (CheckCharacter(upperLine[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckLeft(string[] lines)
        {
            if (StartIndex == 0)
            {
                return false;
            }

            var currentChar = lines[Line][StartIndex - 1];
            return CheckCharacter(currentChar);
        }
        private bool CheckRight(string[] lines)
        {
            if (EndIndex == lines[Line].Length - 1)
            {
                return false;
            }

            var currentChar = lines[Line][EndIndex + 1];
            return CheckCharacter(currentChar);
        }
    
        private bool CheckBottom(string[] lines)
        {
            if (Line == lines.Length - 1)
            {
                return false;
            }

            var lowerLine = lines[Line + 1];
            var start = StartIndex == 0 ? 0 : StartIndex - 1;
            if (start >= lowerLine.Length)
            {
                return false;
            }

            var end = lowerLine.Length > EndIndex + 1 ? EndIndex + 1 : EndIndex;

            for (var i = start; i < end + 1; i++)
            {
                if (CheckCharacter(lowerLine[i]))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCharacter(char character) => character != PeriodSymbol && !char.IsNumber(character);
    }}