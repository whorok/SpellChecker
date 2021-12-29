using System.Collections.Generic;

namespace SpellChecker
{
    public interface ITextFormatter
    {
        string[] SplitText(string text);
        string[] SplitWords(string text);
        string ReplaceWords(string text, IEnumerable<(string word, IEnumerable<string> corrections)> pairs);
    }
}