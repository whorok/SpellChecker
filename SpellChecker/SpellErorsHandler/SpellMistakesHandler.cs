using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace SpellChecker
{
    public class SpellMistakesHandler : ISpellMistakesHandler
    {
        private int MaxEditDistance { get; } = 2;

        public IEnumerable<string> FindErrorWords(IEnumerable<string> text, WordsDictionary<string> wordsDictionary)
        {
            return text.Where(x => !wordsDictionary.Contains(x, StringComparer.InvariantCultureIgnoreCase));
        }

        public IEnumerable<(string, IEnumerable<string>)> FindPairsToErrorWords(IEnumerable<string> errorWords,
            WordsDictionary<string> wordsDictionary, Func<string, string, int> checkAlgorithm)
        {
            return errorWords.Select(misspelledWord => (misspelledWord, FindCorrections(wordsDictionary, checkAlgorithm, misspelledWord)));
        }

        private IEnumerable<string> FindCorrections(WordsDictionary<string> wordsDictionary,
            Func<string, string, int> checkAlgorithm, string misspelledWord)
        {
            var maxEditDistance = int.MaxValue;
            var correctionList = new List<string>();
            foreach (var correctWord in wordsDictionary)
            {
                var editDistance = checkAlgorithm(misspelledWord.ToLower(), correctWord.ToLower());
                if (editDistance > maxEditDistance || editDistance > MaxEditDistance) continue;
                if (editDistance < maxEditDistance) correctionList.Clear();
                maxEditDistance = editDistance;
                correctionList.Add(correctWord);
            }

            return correctionList;
        }
    }
}