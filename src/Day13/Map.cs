namespace Day13
{
    public class Map(string[] lines)
    {
        private int _previousColumn = -1;
        private int _previousRow = -1;

        public long GetReflectionCount(bool skipPrevious)
        {
            if (skipPrevious)
            {
                for (var row = 0; row < lines.Length; row++)
                {
                    var originalRow = lines[row].ToCharArray();
                    for (var column = 0; column < lines[row].Length; column++)
                    {
                        var previous = originalRow[column];
                        var current = previous == '.' ? '#' : '.';
                        originalRow[column] = current;
                        lines[row] = string.Join("", originalRow);
                        var result = GetResult(true);
                        if (result > 0)
                        {
                            return result;
                        }
                        else
                        {
                            originalRow[column] = previous;
                        }
                    }
                    lines[row] = string.Join("", originalRow);
                }
            }
            else
            {
                return GetResult(skipPrevious);
            }

            throw new Exception(" no result");
        }

        private long GetResult(bool skipPrevious)
        {
            var vertical = GetVertical(skipPrevious);
            if (vertical > 0)
            {
                return vertical;
            }

            var horizontal = GetHorizontal(skipPrevious);
            if (horizontal > 0)
            {
                return horizontal * 100;
            }

            return 0;
        }

        private long GetVertical(bool skipPrevious)
        {
            for (var i = 0; i < lines.First().Length - 1; i++)
            {
                if (skipPrevious && _previousColumn == i)
                {
                    continue;
                }

                var isValid = true;
                foreach (var line in lines)
                {
                    var left = i;
                    var right = i + 1;
                    while (isValid && left >= 0 && right < line.Length)
                    {
                        if (line[left] != line[right])
                        {
                            isValid = false;
                        }

                        left--;
                        right++;
                    }

                    if (!isValid)
                    {
                        break;
                    }
                }

                if (isValid)
                {
                    _previousColumn = i;
                    return i + 1;
                }
            }

            return 0;
        }

        private long GetHorizontal(bool skipPrevious)
        {
            for (var i = 0; i < lines.Length - 1; i++)
            {
                if (skipPrevious && _previousRow == i)
                {
                    continue;
                }

                var isValid = true;
                for (var column = 0; column < lines[i].Length; column++)
                {
                    var top = i;
                    var bottom = i + 1;
                    while (isValid && top >= 0 && bottom < lines.Length)
                    {
                        if (lines[top][column] != lines[bottom][column])
                        {
                            isValid = false;
                        }

                        top--;
                        bottom++;
                    }

                    if (!isValid)
                    {
                        break;
                    }
                }

                if (isValid)
                {
                    _previousRow = i;
                    return i + 1;
                }
            }

            return 0;
        }
    }
}