using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.Service
{
    public class FileAnalyzingService : IFileAnalyzingService
    {
        private readonly IWordsAnalyzingService _wordsAnalyzingService;

        public FileAnalyzingService(IWordsAnalyzingService wordsAnalyzingService)
        {
            _wordsAnalyzingService = wordsAnalyzingService;
        }

        public Result<WordInfo[]> AnalyzeWords(string fileName, int topWords)
        {
            if (!File.Exists(fileName))
            {
                return Result<WordInfo[]>.Fail("Incorrect file path");
            }
            
            var text = File.ReadAllText(fileName);
            var words = Regex.Split(text.ToLower().Trim(), @"[^a-zа-я]")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            var wordsInfo = !words.Any() 
                ? Array.Empty<WordInfo>() 
                : _wordsAnalyzingService.SelectTopWordsInfo(words, topWords);
            return Result<WordInfo[]>.Ok(wordsInfo);
        }
    }
}