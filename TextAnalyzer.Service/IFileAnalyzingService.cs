using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.Service
{
    public interface IFileAnalyzingService
    {
        WordInfo[] AnalyzeWords(string fileName, int topWords);
    }
}