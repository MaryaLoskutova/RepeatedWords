using System.Text;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordInfo
    {
        private WordsStatistic _neighbours;
        private int _topNeighboursCount = 5;

        public string Word { get; set; }
        public int WordFrequency { get; set; }

        public void AddNeighbour(string neighbour)
        {
            _neighbours.AddWord(neighbour);
        }

        public override string ToString()
        {
            var info = new StringBuilder($"{Word} ({WordFrequency})");
            if (!_neighbours.Any())
            {
                return info.ToString();
            }

            info.AppendLine("Neighbours: ");
            _neighbours.SelectTop(_topNeighboursCount)
                .ForEach(x =>
                {
                    info.Append(x);
                    info.Append(", ");
                });
            var lastSeparatorLength = 4;
            info.Remove(info.Length - lastSeparatorLength, lastSeparatorLength);
            return info.ToString();
        }
    }
}