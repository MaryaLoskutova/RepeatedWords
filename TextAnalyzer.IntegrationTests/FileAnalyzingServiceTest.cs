using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TextAnalyzer.Service;
using TextAnalyzer.Service.BusinessObjects;

namespace TextAnalyzer.IntegrationTests
{
    public class FileAnalyzingServiceTest
    {
        private IFileAnalyzingService _fileAnalyzingService;

        [SetUp]
        public void Setup()
        {
            _fileAnalyzingService = new FileAnalyzingService(new WordsAnalyzingService());
        }

        [TestCaseSource(nameof(AnalyzeWordsTestData))]
        public void AnalyzeWordsTest(string path, int top, string[] expected)
        {
            var result = _fileAnalyzingService.AnalyzeWords(path, top);
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(null, result.Message);
            Assert.AreEqual(expected, result.Value.Select(x => x.ToString()).ToArray());
        }
        
        [Test]
        public void AnalyzeWordsWhenIncorrectFilePathTest()
        {
            var expected = Result<WordInfo[]>.Fail("Incorrect file path");
            var result = _fileAnalyzingService.AnalyzeWords("path", 10);
            Assert.AreEqual("Incorrect file path", result.Message);
            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual(null, result.Value);
        }

        private static IEnumerable<TestCaseData> AnalyzeWordsTestData()
        {
            yield return new TestCaseData(
                    "TestData\\Empty.txt",
                    5,
                    Array.Empty<string>())
                .SetName("Empty file");

            yield return new TestCaseData(
                    "TestData\\SomeWords.txt",
                    2,
                    new[] {"если (1) Neighbours: бы (1)", "бы (1) Neighbours: если (1), история (1)"})
                .SetName("Some words file");

            yield return new TestCaseData(
                    "TestData\\SomeWords.txt",
                    2,
                    new[] {"если (1) Neighbours: бы (1)", "бы (1) Neighbours: если (1), история (1)"})
                .SetName("Some words with symbols file");

            yield return new TestCaseData(
                    "TestData\\WarAndPeace.txt",
                    5,
                    new[]
                    {
                        "и (21421) Neighbours: не (722), в (492), как (485), он (476), что (443)",
                        "в (11111) Neighbours: и (492), том (294), то (269), его (254), москве (176)",
                        "не (8749) Neighbours: и (722), он (492), было (365), я (345), ничего (343)",
                        "что (8362) Neighbours: то (1119), он (883), и (442), потому (395), том (313)",
                        "он (7493) Neighbours: что (886), сказал (563), не (492), и (477), как (333)"
                    }
                )
                .SetName("War and Peace"); 
            yield return new TestCaseData(
                    "TestData\\WithNumbers.txt",
                    5,
                    new[]
                    {
                        "я (1) Neighbours: бы (1)",
                        "бы (1) Neighbours: я (1), спросил (1)",
                        "спросил (1) Neighbours: бы (1), сказал (1)",
                        "сказал (1) Neighbours: спросил (1), виконт (1)",
                        "виконт (1) Neighbours: сказал (1), как (1)"
                    }
                )
                .SetName("With Numbers");
        }
    }
}