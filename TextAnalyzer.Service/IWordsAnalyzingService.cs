using System.Collections.Generic;
using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.Service
{
    public interface IWordsAnalyzingService
    {
        WordInfo[] SelectTopWordsInfo(string[] words, int top);
    }
}