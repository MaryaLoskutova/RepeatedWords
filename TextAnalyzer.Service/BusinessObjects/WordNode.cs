namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordNode
    {
        public WordNode(WordInfo value)
        {
            Value = value;
        }

        public WordInfo Value { get; }
        public WordNode Next { get; set; } 
        public WordNode Previous { get; set; }
    }
}