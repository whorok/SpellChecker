using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpellChecker
{
    public class SpellChecker
    {
        private ISaver Saver { get; set; }
        private ILoader Loader { get; set; }
        private ITextFormatter TextFormatter { get; set; }
        private ISpellMistakesHandler SpellMistakesHandler { get; set; }
        private readonly TextWriter _textWriter;
        private WordsDictionary<string> _dictionary;
        private string[] _text;


        public int MaxWordLength { get; set; } = 50;

        public SpellChecker(TextWriter textWriter) : this(new SaverLoader(), new SaverLoader(), new TextFormatter(),
            new SpellMistakesHandler(), textWriter)
        {
        }

        public SpellChecker(ISaver saver, ILoader loader, ITextFormatter textFormatter,
            ISpellMistakesHandler spellMistakesHandler, TextWriter textWriter)
        {
            Saver = saver;
            Loader = loader;
            TextFormatter = textFormatter;
            SpellMistakesHandler = spellMistakesHandler;
            _textWriter = textWriter;
        }

        public SpellChecker LoadTextFromDefaultPath()
        {
            _text = TextFormatter.SplitText(Loader.Load());
            return this;
        }

        public SpellChecker FillDictionary()
        {
            if (_text == null || _text.Length < 2)
                throw new Exception(
                    "Wrong input. Make sure to load the text first.\nKeep in mind that sections must be separated with  \"===\"");
            var words = TextFormatter.SplitWords(_text[0]);
            CheckWordsLength(words);
            _dictionary = new WordsDictionary<string>(TextFormatter.SplitWords(_text[0]));
            _textWriter.WriteLine("Dictionary filled successfully");
            return this;
        }

        

        public void FixSpellingMistakesWith(Func<string, string, int> checkAlgorithm)
        {
            if (_dictionary == null)
                throw new Exception("Fill the dictionary first.");
            var text = TextFormatter.SplitWords(_text[1]);
            CheckWordsLength(text);
            var errorWords = SpellMistakesHandler.FindErrorWords(text, _dictionary);
            var pairs = SpellMistakesHandler.FindPairsToErrorWords(errorWords, _dictionary, checkAlgorithm);
            var result = TextFormatter.ReplaceWords(_text[1].Remove(0, 2), pairs);
            Saver.Save(result);
            _textWriter.WriteLine("Done!");
        }
        
        private void CheckWordsLength(string[] words)
        {
            if (!IsWordsLengthCorrect(words))
                throw new Exception($"Sorry, max word length is {MaxWordLength}");
        }

        private bool IsWordsLengthCorrect(IEnumerable<string> text)
        {
            return text.Any(x => x.Length > 50);
        }
    }
}