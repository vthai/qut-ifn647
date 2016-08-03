using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyser
{
    class TextAnalyser
    {
        PorterStemmerAlgorithm.PorterStemmer myStemmer; // for Activity 4,5
        System.Collections.Generic.Dictionary<string, int> tokenCount; // for Activity 5
        Dictionary<string, int> frequency = new Dictionary<string, int>();

        Dictionary<string, bool> stopwordList = new Dictionary<string, bool>();

        public TextAnalyser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            tokenCount = new Dictionary<string,int>();
            initializeStopWords();
        }

        private void initializeStopWords() {
            stopwordList.Add("a", true);
            stopwordList.Add("an", true);
            stopwordList.Add("and", true);
            stopwordList.Add("are", true);
            stopwordList.Add("as", true);
            stopwordList.Add("at", true);
            stopwordList.Add("be", true);
            stopwordList.Add("but", true);
            stopwordList.Add("by", true); 
            stopwordList.Add("for", true);
            stopwordList.Add("if", true);
            stopwordList.Add("in", true);
            stopwordList.Add("into", true);
            stopwordList.Add("is", true);
            stopwordList.Add("it", true); 
            stopwordList.Add("no", true);
            stopwordList.Add("not", true);
            stopwordList.Add("of", true);
            stopwordList.Add("on", true);
            stopwordList.Add("or", true);
            stopwordList.Add("such", true);
            stopwordList.Add("that", true);
            stopwordList.Add("the", true);
            stopwordList.Add("their", true);
            stopwordList.Add("then", true);

            stopwordList.Add("there", true);
            stopwordList.Add("these", true);
            stopwordList.Add("they", true);
            stopwordList.Add("this", true);
            stopwordList.Add("to", true);
            stopwordList.Add("was", true);
            stopwordList.Add("will", true);
            stopwordList.Add("with", true);
        }

        public string[] StemTokens(string[] tokens) 
        {
            List<string> array = new List<string>();
            foreach (string token in tokens)
            {
                Console.WriteLine("Orginal: " + token);
                string temp = myStemmer.stemTerm(token);
                array.Add(temp);
                Console.WriteLine("Stems: " + temp);
            }
            return array.ToArray();
        }

        public void OutputStems(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);

            string[] stemStr = StemTokens(tokens);
            //for (int i = 0; i < tokens.Length; i++) {
            //    Console.WriteLine("Orginal: " + tokens[i] + "\nStems: " + stemStr[i]);
            //}
        }

        //Activity 3
        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            // stub
            text = text.ToLower();
            return text.Split(new char[] {' ', '?', ',', '.', '!', '-', '\'', '"', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Prints out tokens for a given text string
        /// </summary>
        /// <param name="str">a string of text</param>
        public void OutputTokens(string str)
        {
            System.Console.WriteLine("Orginal: \"" + str + "\"");
            string[] tokens = TokeniseString(str);
            Console.WriteLine("Tokens: ");
            foreach (string t in tokens)
            {
                System.Console.WriteLine(t);
            }
        }

        public void CountOccurance(string[] tokens)
        {   
            foreach (string token in tokens)
            {
                int occurrence;
                if (frequency.TryGetValue(token, out occurrence)) {
                    occurrence++;
                    frequency[token] = occurrence;
                } else {
                    frequency.Add(token, 1);
                }
            }
        }

        public void OutputTokenCount(string str, string outputPath) 
        {
            string[] tokens = TokeniseString(str);
            tokens = RemoveStopWords(tokens);
            tokens = StemTokens(tokens);
            CountOccurance(tokens);
            int count = 0;
            string outputString = "";
            foreach(var pair in frequency)
            {
                count++;
                string output = pair.Key + " occurs " + pair.Value + " times\r\n";
                Console.WriteLine(output);
                outputString += output;
            }
            Console.WriteLine("There are " + count + " terms");
            System.IO.File.WriteAllText(outputPath, outputString);
        }

        public string[] RemoveStopWords(string[] tokens) {
            List<string> filteredList = new List<string>();

            foreach (string token in tokens)
            {
                if (token.Length > 2)
                {
                    if (!stopwordList.ContainsKey(token))
                    {
                        filteredList.Add(token);
                    }
                }
            }   

            return filteredList.ToArray();
        }

        public void ProcessText(string path)
        {
            string text = System.IO.File.ReadAllText(path);
            OutputTokenCount(text, @"./output_result.txt");
        }

        public void ProcessTextNoStopWords(string inputFile, string outputFile) {
            string text = System.IO.File.ReadAllText(inputFile);
            OutputTokenCount(text, outputFile);
        }

        static void Main(string[] args)
        {
            TextAnalyser textAnalyser = new TextAnalyser();

            System.Console.WriteLine("Activity 3");
            //string text1 = "Tokenising, even in english, is a difficult problem. It's even harder in other languages - such as Chinese!";
            //string text1 = "abc, def";
            //textAnalyser.OutputTokens(text1);

            //string text2 = "hypothetically desctructiveness adjustment";
            //textAnalyser.OutputStems(text2);

            //string text3 = "The word \"the\" is the most common word in the English language";
            //textAnalyser.OutputTokenCount(text3);

            //textAnalyser.ProcessText(@"./AlicePara.txt");

            textAnalyser.ProcessTextNoStopWords(@"./AlicePara.txt", @"./Alice_Stem_stopword.txt");

            System.Console.ReadLine();

        }



    }
}
