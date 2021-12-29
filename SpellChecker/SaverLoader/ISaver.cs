namespace SpellChecker
{
    public interface ISaver
    {
        string DefaultPath { get; }
        void Save(string text);
    }
}