using System;
using System.IO;
using System.Text;

namespace SpellChecker
{
    public class SaverLoader : ISaver, ILoader
    {
        public string DefaultPath { get; set; }
        private string _input = "Input.txt";
        private string _output = "Output.txt";

        public SaverLoader(string path)
        {
            if (!Uri.IsWellFormedUriString(path, UriKind.Absolute))
                DefaultPath = path;
            else
                throw new Exception($"Sorry couldn't find the folder:\n{path}");
        }

        public void Save(string outputText)
        {
            File.WriteAllText(DefaultPath + _output,outputText);
        }

        public string Load()
        {
            string inputText = null;
            if (File.Exists(DefaultPath + _input))
            {
                inputText = File.ReadAllText(DefaultPath + _input);
            }

            return inputText;
        }
    }
}