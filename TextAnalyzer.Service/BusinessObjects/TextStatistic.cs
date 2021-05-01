using System.Collections.Generic;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class TextStatistic
    {
        private WordsOrderList _wordsOrder;
        private Dictionary<string, WordNode> _wordsDictionary;

        public TextStatistic()
        {
            _wordsOrder = new WordsOrderList();
            _wordsDictionary = new Dictionary<string, WordNode>();
        }

        public void AddWord(string word)
        {
            IncrementWord(word);
            _wordsOrder.Actualize(_wordsDictionary[word]);
        }
        

        private void IncrementWord(string word)
        {
            if (!_wordsDictionary.ContainsKey(word))
            {
                var wordStatistic = new WordStatistic {Word = word};
                _wordsDictionary[word] = new WordNode(wordStatistic);
            }

            _wordsDictionary[word].Value.WordFrequency++;
        }
    }
}