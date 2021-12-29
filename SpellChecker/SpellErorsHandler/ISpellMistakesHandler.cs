using System;
using System.Collections.Generic;

namespace SpellChecker
{
    public interface ISpellMistakesHandler
    {
        IEnumerable<string> FindErrorWords(IEnumerable<string> text, WordsDictionary<string> wordsDictionary);

        IEnumerable<(string, IEnumerable<string>)> FindPairsToErrorWords(IEnumerable<string> errorWords,
            WordsDictionary<string> wordsDictionary, Func<string, string, int> checkAlgorithm);
    }
}