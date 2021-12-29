namespace SpellChecker
{
    public interface ILoader
    {
        string DefaultPath { get; }
        string Load();
    }
}