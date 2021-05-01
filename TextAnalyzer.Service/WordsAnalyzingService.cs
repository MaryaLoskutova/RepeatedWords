using System.Collections.Generic;
using System.Linq;
using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.Service
{
    public class WordsAnalyzingService : IWordsAnalyzingService
    {
        public WordInfo[] SelectTopWordsInfo(string[] words, int top)
        {
            var statistic = new WordsStatistic();
            for (var i = 0; i < words.Length; i++)
            {
                var neighbours = SelectNeighbours(words, i);
                statistic.AddWord(words[i], neighbours);
            }

            return statistic.SelectTop(top).ToArray();
        }

        private static IEnumerable<string> SelectNeighbours(string[] words, int index)
        {
            var neighbours = new List<string>();
            if (!NeedSkip(words, index, NeighbourSide.Left))
            {
                neighbours.Add(words[index - 1]);
            }

            if (!NeedSkip(words, index, NeighbourSide.Right))
            {
                neighbours.Add(words[index + 1]);
            }

            return neighbours.Except(new[] {words[index]});
        }

        private static bool NeedSkip(string[] words, int index, NeighbourSide side)
        {
            var neighbourIndex = side == NeighbourSide.Left ? index - 1 : index + 1;
            return NeedSkip(words, index, neighbourIndex) || IsBetweenSameWords(words, index, side);
        }

        private static bool IsBetweenSameWords(string[] words, int index, NeighbourSide side)
        {
            if (side == NeighbourSide.Right)
            {
                return false;
            }

            var neighbourIndex = index - 2;
            if (neighbourIndex < 0)
            {
                return false;
            }

            return words[index].GetHashCode() == words[neighbourIndex].GetHashCode();
        }

        private static bool NeedSkip(string[] words, int index, int neighbourIndex)
        {
            if (neighbourIndex < 0
                || neighbourIndex >= words.Length
                || words[index].GetHashCode() == words[neighbourIndex].GetHashCode())
            {
                return true;
            }

            return false;
        }
    }
}