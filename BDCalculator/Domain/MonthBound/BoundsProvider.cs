using System.Collections.Generic;

namespace BDCalculator.Domain.MonthBound
{
    public static class BoundsProvider
    {
        public static Dictionary<int, int> GetBoundDates(int[][] sequences, int loopLength, int loopStartYear)
        {
            var boundDates = new Dictionary<int, int>();
            var index = 0;
            for (var i = 0; i < sequences.Length; i++)
            {
                var sequence = GetLongSequence(i, sequences, loopLength);
                for (var j = 0; j < loopLength; j++)
                {
                    boundDates.Add(loopStartYear + index, sequence[j]);
                    index++;
                }
            }

            return boundDates;
        }

        private static int[] GetLongSequence(int sequencesIndex, int[][] sequences, int loopLength)
        {
            var loopLengthSmall = loopLength / sequences[sequencesIndex].Length + 1;
            var sequence = new int[loopLength];
            var index = 0;
            for (var i = 0; i < loopLengthSmall; i++)
            for (var j = 0; j < sequences[sequencesIndex].Length; j++)
            {
                if (index > loopLength - 1)
                    break;
                sequence[index] = sequences[sequencesIndex][j];
                index++;
            }

            return sequence;
        }
    }
}