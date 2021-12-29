using System;

namespace SpellChecker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new SpellChecker(Console.Out).LoadTextFromDefaultPath().FillDictionary()
                .FixSpellingMistakesWith(new CheckAlgorithms().GetDefaultDistance);
            Console.ReadKey();
        }
    }
}