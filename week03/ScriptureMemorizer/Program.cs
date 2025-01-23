using System;
using System.Collections.Generic;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Reference reference = new Reference("Proverbs", 3, 5, 6);
            Scripture scripture = new Scripture(reference, "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
                }
                
                                scripture.HideRandomWords();
                                if (scripture.AllWordsHidden())
                                {
                                    Console.Clear();
                                    Console.WriteLine(scripture.GetDisplayText());
                                    Console.WriteLine("\nAll words are hidden. Program will exit now.");
                                    break;
                            }
                    }

        }
    }

    class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int StartVerse { get; }
        public int EndVerse { get; }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            if (StartVerse == EndVerse)
            {
                return $"{Book} {Chapter}:{StartVerse}";
            }
            else
            {
                return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
            }
        }
    }

    class Scripture
    {
        private Reference _reference;
        private string _text;
        private List<string> _words;
        private Random _random;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _text = text;
            _words = new List<string>(_text.Split(' '));
            _random = new Random();
        }

        public string GetDisplayText()
        {
            return $"{_reference}\n{string.Join(" ", _words)}";
        }

        public void HideRandomWords()
        {
            int wordsToHide = _random.Next(1, _words.Count / 2);
            for (int i = 0; i < wordsToHide; i++)
            {
                int index = _random.Next(_words.Count);
                _words[index] = new string('_', _words[index].Length);
            }
        }

        public bool AllWordsHidden()
        {
            foreach (string word in _words)
            {
                if (word.Contains('_') == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
    
}
