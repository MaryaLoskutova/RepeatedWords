using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordsStatistic
    {
        private WordsOrderList _wordsOrder;
        private Dictionary<string, WordNode> _wordsDictionary;

        public WordsStatistic()
        {
            _wordsOrder = new WordsOrderList();
            _wordsDictionary = new Dictionary<string, WordNode>();
        }

        public void AddWord(string word, IEnumerable<string> neighbours = null)
        {
            IncrementWord(word);
            _wordsOrder.Actualize(_wordsDictionary[word]);
            if (neighbours != null)
            {
                AddNeighbours(word, neighbours);
            }
        }

        public bool Any() => _wordsDictionary.Any();

        public List<WordInfo> SelectTop(int count)
        {
            return _wordsOrder.SelectTop(count);
        }

        private void AddNeighbours(string word, IEnumerable<string> neighbours)
        {
            foreach (var neighbour in neighbours)
            {
                _wordsDictionary[word].Value.AddNeighbour(neighbour);
            }
        }

        private void IncrementWord(string word)
        {
            if (!_wordsDictionary.ContainsKey(word))
            {
                var wordStatistic = new WordInfo(word);
                _wordsDictionary[word] = new WordNode(wordStatistic);
            }

            _wordsDictionary[word].Value.WordFrequency++;
        }
    }
}