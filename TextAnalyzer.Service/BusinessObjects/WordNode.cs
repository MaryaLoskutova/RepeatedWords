namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordNode
    {
        public WordNode(WordStatistic value)
        {
            Value = value;
        }

        public WordStatistic Value { get; set; }
        public WordNode Next { get; set; } 
        public WordNode Previous { get; set; }
    }
}