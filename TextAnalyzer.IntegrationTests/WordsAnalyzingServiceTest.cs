using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TextAnalyzer.Service;

namespace TextAnalyzer.IntegrationTests
{
    public class WordsAnalyzingServiceTest
    {
        private IWordsAnalyzingService _wordsAnalyzingService;

        [SetUp]
        public void Setup()
        {
            _wordsAnalyzingService = new WordsAnalyzingService();
        }

        [TestCaseSource(nameof(SelectTopWordsTestData))]
        public void SelectTopWordsInfoTest(string[] words, int top, string[] expectedStrings)
        {
            var resultWords = _wordsAnalyzingService.SelectTopWordsInfo(words, top);
            Assert.AreEqual(expectedStrings, resultWords.Select(x => x.ToString()).ToArray());
        }

        private static IEnumerable<TestCaseData> SelectTopWordsTestData()
        {
            yield return new TestCaseData(Array.Empty<string>(), 5, Array.Empty<string>()).SetName("No words");
            yield return new TestCaseData(new[] {"copy"}, 5, new[] {"copy (1)"}).SetName("One words");
            yield return new TestCaseData(
                    new[] {"copy", "paste"}, 5,
                    new[] {"copy (1) Neighbours: paste (1)", "paste (1) Neighbours: copy (1)"})
                .SetName("Two different words");
            yield return new TestCaseData(
                    new[] {"copy", "copy"}, 5,
                    new[] {"copy (2)"})
                .SetName("Two identical words");
            yield return new TestCaseData(
                    new[] {"copy", "paste", "delete"}, 5,
                    new[]
                    {
                        "copy (1) Neighbours: paste (1)",
                        "paste (1) Neighbours: copy (1), delete (1)",
                        "delete (1) Neighbours: paste (1)",
                    })
                .SetName("Three different words");
            yield return new TestCaseData(
                    new[] {"copy", "paste", "delete", "enter", "alter"}, 2,
                    new[]
                    {
                        "copy (1) Neighbours: paste (1)",
                        "paste (1) Neighbours: copy (1), delete (1)"
                    })
                .SetName("Five different words. Two words returned");
            yield return new TestCaseData(
                    new[] {"copy", "paste", "delete", "enter", "delete"}, 2,
                    new[]
                    {
                        "delete (2) Neighbours: paste (1), enter (1)",
                        "copy (1) Neighbours: paste (1)"
                    })
                .SetName("Five words with repeats. Two words returned"); 
            yield return new TestCaseData(
                    new[] {"delete", "delete", "delete", "delete", "delete"}, 2,
                    new[]
                    {
                        "delete (5)"
                    })
                .SetName("Five identical with repeats. Two words returned");
        }
    }
}