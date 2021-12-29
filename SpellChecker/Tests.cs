using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SpellChecker
{
    [TestFixture]
    public class Tests
    {
        private readonly string dictionaryInput = "rain spain plain plaint pain main mainly the in on fall falls his was";
        private readonly string textInput = "hte rame in pain fells mainy oon teh lain was hints pliant";
        
        [Test]
        public void FindErrorWordsWorksCorrectly()
        {
            var dictionaryList =
                new TextFormatter().SplitWords(dictionaryInput);
            var dictionary = new WordsDictionary<string>(dictionaryList);
            var textList = new TextFormatter().SplitWords(textInput);
            var spellChecker = new SpellMistakesHandler();
            var expected = new List<string> { "hte", "rame", "fells", "mainy", "oon", "teh", "lain", "hints", "pliant" };
            Assert.AreEqual(expected, spellChecker.FindErrorWords(textList, dictionary));
        }
        
        [Test]
        public void PairFinderWorksCorrectly()
        {
            var spellChecker = new SpellMistakesHandler();
            var dictionaryList =
                new TextFormatter().SplitWords(dictionaryInput);
            var textList = new TextFormatter().SplitWords(textInput);
            var dictionary = new WordsDictionary<string>(dictionaryList);
            var errorList = spellChecker.FindErrorWords(textList, dictionary);
            var expected = new List<string> { "the", "falls", "main", "mainly", "on", "the", "plain", "plaint" };

            var result = spellChecker.FindPairsToErrorWords(errorList, dictionary,
                new CheckAlgorithms().GetDefaultDistance).SelectMany(x => x.Item2);
            Assert.AreEqual(expected, result);
        }
    }
}