using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SpellChecker
{
    public class WordsDictionary<T> : IEnumerable<T>
    {
        private readonly List<T> _dictionary;

        public WordsDictionary() : this(new List<T>())
        {
        }

        public WordsDictionary(T[] dictionary): this(dictionary.ToList())
        {
        }

        public WordsDictionary(List<T> dictionary)
        {
            if (dictionary != null)
                _dictionary = dictionary;
        }

        public void FillDictionary(params T[] dictionary)
        {
            _dictionary.AddRange(dictionary);
        }

        public void ClearDictionary()
        {
            _dictionary.Clear();
        }

        public bool Contains(T input)
        {
            return _dictionary.Contains(input);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_dictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}