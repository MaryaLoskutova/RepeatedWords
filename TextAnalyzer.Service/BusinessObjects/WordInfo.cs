using System.Text;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordInfo
    {
        private int _topNeighboursCount = 5;

        public WordInfo(string word)
        {
            Word = word;
            Neighbours = new WordsStatistic();
        }

        public string Word { get; }
        public int WordFrequency { get; set; }
        private WordsStatistic Neighbours { get; }


        public void AddNeighbour(string neighbour)
        {
            Neighbours.AddWord(neighbour);
        }

        public override string ToString()
        {
            var info = new StringBuilder($"{Word} ({WordFrequency})");
            if (!Neighbours.Any())
            {
                return info.ToString();
            }

            info.Append(" Neighbours: ");
            Neighbours.SelectTop(_topNeighboursCount)
                .ForEach(x =>
                {
                    info.Append(x);
                    info.Append(", ");
                });
            var lastSeparatorLength = 2;
            info.Remove(info.Length - lastSeparatorLength, lastSeparatorLength);
            return info.ToString();
        }
    }
}