using System;
using System.IO;
using System.Text;

namespace SpellChecker
{
    public class SaverLoader : ISaver, ILoader
    {
        public string DefaultPath { get; private set; } = "c:\\TestApp\\";
        private string _input = "Input.txt";
        private string _output = "Output.txt";

        public void Save(string outputText)
        {
            CheckAndCreateDirectory();
            using (var streamWriter = new StreamWriter(DefaultPath + _output))
            {
                streamWriter.WriteLine(outputText);
            }
        }

        public string Load()
        {
            string inputText;
            if (File.Exists(DefaultPath + _input))
            {
                inputText = File.ReadAllText(DefaultPath + _input);
            }
            else
            {
                CheckAndCreateDirectory();
                File.Create(DefaultPath + _input);
                return string.Empty;
            }

            return inputText;
        }

        private void CheckAndCreateDirectory()
        {
            if (!Directory.Exists(DefaultPath))
                Directory.CreateDirectory(DefaultPath);
        }
    }
}