using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Internal;

namespace SpellChecker
{
    public class TextFormatter : ITextFormatter
    {
        private char[] WordSeparators { get; } = { ' ', '\n', '\r' };
        private string[] TextSeparators { get; } = { "===" };

        public string[] SplitText(string text)
        {
            var splitText = text.Split(TextSeparators, StringSplitOptions.RemoveEmptyEntries);
            return splitText;
        }

        public string[] SplitWords(string text)
        {
            return text.Split(WordSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        public string ReplaceWords(string text, IEnumerable<(string word, IEnumerable<string> corrections)> pairs)
        {
            var sb = new StringBuilder(text);

            foreach (var (word, corrections) in pairs)
            {
                var enumerable = corrections.ToList();
                if (enumerable.Count > 1)
                    sb.Replace(word, $"{{{string.Join(" ", enumerable)}}}");
                else if (enumerable.Count == 1)
                    sb.Replace(word, enumerable.First());
                else
                    sb.Replace(word, $"{{{word}?}}");
            }

            return sb.ToString();
        }
    }
}