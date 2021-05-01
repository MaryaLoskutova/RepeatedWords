using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordsStatistic
    {
        private Dictionary<string, WordNode> _wordsDictionary;
        private Dictionary<int, WordsOrderList> _countDictionary;

        public WordsStatistic()
        {
            _wordsDictionary = new Dictionary<string, WordNode>();
            _countDictionary = new Dictionary<int, WordsOrderList>();
        }

        public void AddWord(string word, IEnumerable<string> neighbours = null)
        {
            IncrementWord(word);

            if (neighbours != null)
            {
                AddNeighbours(word, neighbours);
            }
        }

        public bool Any() => _wordsDictionary.Any();

        public List<WordInfo> SelectTop(int count)
        {
            var result = new List<WordInfo>();
            var restCount = count;
            foreach (var pair in _countDictionary.OrderByDescending(x => x.Key))
            {
                result.AddRange(pair.Value.SelectTop(restCount));
                restCount = count - result.Count;
                if (restCount <= 0)
                {
                    return result;
                }
            }

            return result;
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
            var wordInfo = ExtractWord(word);
            wordInfo.Value.WordFrequency++;
            PlaceToNewPosition(wordInfo);
        }

        private void PlaceToNewPosition(WordNode wordInfo)
        {
            if (!_countDictionary.ContainsKey(wordInfo.Value.WordFrequency))
            {
                _countDictionary[wordInfo.Value.WordFrequency] = new WordsOrderList();
            }

            _countDictionary[wordInfo.Value.WordFrequency].AddToTail(wordInfo);
        }

        private WordNode ExtractWord(string word)
        {
            if (!_wordsDictionary.ContainsKey(word))
            {
                var wordStatistic = new WordInfo(word);
                _wordsDictionary[word] = new WordNode(wordStatistic);
                return _wordsDictionary[word];
            }

            var wordInfo = _wordsDictionary[word];
            var newFrequency = wordInfo.Value.WordFrequency;
            _countDictionary[newFrequency].Remove(wordInfo);
            if (_countDictionary[newFrequency].Empty())
            {
                _countDictionary.Remove(newFrequency);
            }

            return wordInfo;
        }
    }
}