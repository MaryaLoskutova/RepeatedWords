using System.Collections.Generic;

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

        public void AddWord(string word)
        {
            IncrementWord(word);
            _wordsOrder.Actualize(_wordsDictionary[word]);
        }
        
        public bool TryAddWordNeighbours(string word, IEnumerable<string> neighbours)
        {
            if (!_wordsDictionary.ContainsKey(word))
            {
                return false;
            }

            foreach (var neighbour in neighbours)
            {
                _wordsDictionary[word].Value.Neighbours.AddWord(neighbour);
            }

            return true;
        }

        public List<WordInfo> SelectTopWords(int count)
        {
            return _wordsOrder.SelectTop(count);
        }

        private void IncrementWord(string word)
        {
            if (!_wordsDictionary.ContainsKey(word))
            {
                var wordStatistic = new WordInfo {Word = word};
                _wordsDictionary[word] = new WordNode(wordStatistic);
            }

            _wordsDictionary[word].Value.WordFrequency++;
        }
    }
}