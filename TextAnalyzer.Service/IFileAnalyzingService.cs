using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.Service
{
    public interface IFileAnalyzingService
    {
        Result<WordInfo[]> AnalyzeWords(string fileName, int topWords);
    }
}