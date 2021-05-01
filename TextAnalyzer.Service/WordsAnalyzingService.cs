using System.Collections.Generic;
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
            if (index > 0)
            {
                neighbours.Add(words[index - 1]);
            }

            if (index < words.Length - 1)
            {
                neighbours.Add(words[index + 1]);
            }

            return neighbours;
        }
    }
}