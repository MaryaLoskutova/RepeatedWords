using System;
using System.IO;
using TextAnalyzer.Service;
using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.ConsoleApp
{
    public class App
    {
        private readonly IFileAnalyzingService _fileAnalyzingService;

        public App(IFileAnalyzingService fileAnalyzingService)
        {
            _fileAnalyzingService = fileAnalyzingService;
        }

        public void Run(string[] args)
        {
            var path = GetPath();
            var topCount = GetTopCount();

            var words = _fileAnalyzingService.AnalyzeWords(path, topCount);
            WriteResult(words);
        }

        private static void WriteResult(Result<WordInfo[]> result)
        {
            Console.WriteLine("\r\nРезультат");
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Message);
                return;
            }

            foreach (var word in result.Value)
            {
                Console.WriteLine(word);
            }
        }

        private static int GetTopCount()
        {
            Console.WriteLine("Введите количество слов (default 20):");
            var countString = Console.ReadLine();
            var topCount = string.IsNullOrEmpty(countString) ? 20 : int.Parse(countString);
            return topCount;
        }

        private static string GetPath()
        {
            Console.WriteLine("Введите путь к файлу (default \"voyna-i-mir.txt\"):");
            var path = Console.ReadLine();
            path = string.IsNullOrEmpty(path) ? Directory.GetCurrentDirectory() + "\\Example\\voyna-i-mir.txt" : path;
            return path;
        }
    }
}