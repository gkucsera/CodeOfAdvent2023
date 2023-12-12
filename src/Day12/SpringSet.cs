namespace Day12
{
    public class SpringSet
    {
        private readonly string _line;
        private readonly int[] _springs;

        private const char Spring = '#';
        private const char Blank = '.';
        private const char Replace = '?';

        public SpringSet(string line, int[] springs)
        {
            _line = line;
            _springs = springs;
        }

        public long NumberOfValidSets2(bool isUnfolded)
        {
            var characters = isUnfolded ? Unfolded : _line;
            var adjacents = isUnfolded ? UnfoldedSprings().ToArray() : _springs;

            var result = DoCheck(characters.ToCharArray(), 0, adjacents, 0, 0, false, []);

            return result;
        }


        private long DoCheck(char[] line, int index, int[] adjacents, int currentAdjacentIndex, int currentAdjacent, bool isPreviousSequence,
            Dictionary<(int, int, int, bool), long> cache)
        {
            var cacheKey = (index, currentAdjacentIndex, currentAdjacent, isPreviousSequence);

            if (cache.TryGetValue(cacheKey, out var cacheValue)) return cacheValue;

            if (index == line.Length)
            {
                if ((currentAdjacentIndex == adjacents.Length && currentAdjacent == 0) ||
                    (currentAdjacentIndex == adjacents.Length - 1 && adjacents[currentAdjacentIndex] == currentAdjacent)) return 1;
                return 0;
            }

            if (line[index] == Blank) return HandleBlank(line, index, adjacents, currentAdjacentIndex, currentAdjacent, isPreviousSequence, cache);

            if (line[index] == Spring)
            {
                var result = HandleSpring(line, index, adjacents, currentAdjacentIndex, currentAdjacent, isPreviousSequence, cache);
                cache[cacheKey] = result;
                return result;
            }

            if (line[index] == Replace)
            {
                line[index] = Spring;
                var result = HandleSpring(line, index, adjacents, currentAdjacentIndex, currentAdjacent, isPreviousSequence, cache);
                line[index] = Blank;
                result += HandleBlank(line, index, adjacents, currentAdjacentIndex, currentAdjacent, isPreviousSequence, cache);
                line[index] = Replace;
                cache[cacheKey] = result;
                return result;
            }

            throw new Exception("failed brackets");
        }

        private long HandleSpring(char[] line, int index, int[] adjacents, int currentAdjacentIndex, int currentAdjacent, bool isPreviousSequence,
            Dictionary<(int, int, int, bool), long> cache)
        {
            if (isPreviousSequence)
            {
                currentAdjacent++;
                if (currentAdjacentIndex < adjacents.Length && currentAdjacent <= adjacents[currentAdjacentIndex])
                    return DoCheck(line, index + 1, adjacents, currentAdjacentIndex, currentAdjacent, true, cache);
                return 0;
            }

            if (currentAdjacentIndex >= adjacents.Length) return 0;

            return DoCheck(line, index + 1, adjacents, currentAdjacentIndex, 1, true, cache);
        }

        private long HandleBlank(char[] line, int index, int[] adjacents, int currentAdjacentIndex, int currentAdjacent, bool isPreviousSequence,
            Dictionary<(int, int, int, bool), long> cache)
        {
            if (isPreviousSequence)
            {
                currentAdjacentIndex++;
                if (adjacents[currentAdjacentIndex - 1] != currentAdjacent) return 0L;

                return DoCheck(line, index + 1, adjacents, currentAdjacentIndex, 0, false, cache);
            }

            return DoCheck(line, index + 1, adjacents, currentAdjacentIndex, currentAdjacent, false, cache);
        }

        public string Unfolded => $"{_line}?{_line}?{_line}?{_line}?{_line}";

        public IEnumerable<int> UnfoldedSprings()
        {
            for (var i = 0; i < 5; i++)
                foreach (var spring in _springs)
                    yield return spring;
        }
    }
}