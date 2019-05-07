using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initData();
        }

        private const string HomePath = "d:\\My Documents\\Visual Studio 2017\\Projects\\Coursework\\Coursework\\";
        private const string EnglishWordsPath = HomePath + "english-words.txt";
        private const string FrenchWordsPath = HomePath + "french-words.txt";
        private const string GermanWordsPath = HomePath + "german-words.txt";

        static public Dictionary<int, string> EnglishWords;
        static public Dictionary<int, string> FrenchWords;
        static public Dictionary<int, string> GermanWords;

        static private void initData()
        {
            EnglishWords = populateData(EnglishWordsPath);
            FrenchWords = populateData(FrenchWordsPath);
            GermanWords = populateData(GermanWordsPath);
        }

        static private Dictionary<int,string> populateData(string filename)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<string> lines = readData(filename);

            foreach (string line in lines)
            {
                string[] record = line.Split(',');

                int wordId = int.Parse(record[0]);
                string word = record[1];

                result.Add(wordId, word);
            }

            return result;
        }

        static private List<string> readData(string filename)
        {
            List<string> result = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return result;
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WordToTranslate.Text))
            {
                MessageBox.Show("Please, enter word to translate!");
            }
            else
            {
                string translateFrom = FromLanguageComboBox.Text;
                string translateTo = ToLanguageComboBox.Text;
                string wordToTranslate = WordToTranslate.Text;
                int wordId;
                string translatedWord = "";

                switch (translateFrom)
                {
                    case "English":
                        if (!isWordPresent(EnglishWords, wordToTranslate))
                            break;
                        wordId = getIdForWord(EnglishWords, wordToTranslate);
                        translatedWord = getTranslatedWord(translateTo, wordId);
                        break;
                    case "French":
                        if (!isWordPresent(FrenchWords, wordToTranslate))
                            break;
                        wordId = getIdForWord(FrenchWords, wordToTranslate);
                        translatedWord = getTranslatedWord(translateTo, wordId);
                        break;
                    case "German":
                        if (!isWordPresent(GermanWords, wordToTranslate))
                            break;
                        wordId = getIdForWord(GermanWords, wordToTranslate);
                        translatedWord = getTranslatedWord(translateTo, wordId);
                        break;
                    default:
                        break;
                }

                TranslatedWord.Text = translatedWord;
            }
        }

        private static bool isWordPresent(Dictionary<int, string> wordsDictionary, string word)
        {
            if (wordsDictionary.ContainsValue(word))
                return true;

            MessageBox.Show("Sorry, no such word in our dictionary.");
            return false;
        }

        private static int getIdForWord(Dictionary<int, string> wordsDictionary, string word)
        {
            foreach (int wordId in wordsDictionary.Keys)
            {
                if (wordsDictionary[wordId].Equals(word))
                {
                    return wordId;
                }
            }

            return -1;
        }

        private static string getTranslatedWord(string language, int wordId)
        {
            switch(language)
            {
                case "English":
                    return EnglishWords[wordId];
                case "French":
                    return FrenchWords[wordId];
                case "German":
                    return GermanWords[wordId];
                default:
                    return null;
            }
        }
    }
}
