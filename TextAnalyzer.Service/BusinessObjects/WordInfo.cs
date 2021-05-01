namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordInfo
    {
        public string Word { get; set; }
        public int WordFrequency { get; set; }
        public WordsStatistic Neighbours { get; set; }
    }
}