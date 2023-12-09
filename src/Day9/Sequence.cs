namespace Day9
{
    public class Sequence(int[] items)
    {
        public int GetNextItem()
        {
            var currentSequence = items;
            var itemDifferences = new List<int[]>();
            PopulateSequenceDifferences(currentSequence, itemDifferences);
            itemDifferences.Reverse();
            var result = 0;
            foreach (var difference in itemDifferences)
            {
                result = difference.Last() + result;
            }

            return items.Last() + result;
        }

        public int GetPreviousItem()
        {
            var currentSequence = items;
            var itemDifferences = new List<int[]>();
            PopulateSequenceDifferences(currentSequence, itemDifferences);
            itemDifferences.Reverse();
            var result = 0;
            foreach (var difference in itemDifferences)
            {
                result = difference.First() - result;
            }

            return items.First() - result;
        }

        private void PopulateSequenceDifferences(int[] currentSequence, List<int[]> itemDifferences)
        {
            while (currentSequence.Any(item => item != 0))
            {
                var nextSequence = new int[currentSequence.Length - 1];

                for (var i = 0; i < nextSequence.Length; i++)
                {
                    nextSequence[i] = currentSequence[i + 1] - currentSequence[i];
                }

                itemDifferences.Add(nextSequence);
                currentSequence = nextSequence;
            }
        }
    }
}