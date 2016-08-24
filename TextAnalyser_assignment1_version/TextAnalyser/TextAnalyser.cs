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
        Dictionary<string, bool> stopwords;
        public TextAnalyser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            tokenCount = new Dictionary<string,int>();
            stopwords = new Dictionary<string, bool>() {
                {"a",true},
                 {"an",true},
                 {"and",true},
                 {"are",true},
                 {"as",true},
                 {"at",true},
                 {"be",true},
                 {"but",true},
                 {"by",true},
                {"for",true},
                 {"if",true},
                 {"in",true},
                 {"into",true},
                {"is",true},
                 {"it",true},
                {"no",true},
                 {"not",true},
                 {"of",true},
                 {"on",true},
                 {"or",true},
                 {"such",true},
                {"that",true},
                 {"the",true},
                 {"their",true},
                 {"then",true},
                {"there",true},
                 {"these",true},
                {"they",true},
                 {"this",true},
                 {"to",true},
                 {"was",true},
                 {"will",true},
                 {"with",true}
            };
        }

        //Activity 3
        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            Boolean test;
            //stopwords.TryGetValue("a",out test);
            // stub
            //low case 
            //seplit
            text.ToLower();
            
            return text.Split(new char[] { ' ', ',', '"', '\'', '-', '!','\n','\r' ,'?'},StringSplitOptions.RemoveEmptyEntries); ;
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

        public string[] StemTokens( string text)
        {
            string[] tokens =  TokeniseString(text);
           // List<string> stemedtokens = new List<string>;
            List<string> stemedtokens = new List<string>();
            //myStemmer.stem(tokens);
            //string token="";
            // foreach token in tokens;
            string stemed_token;
            foreach (string token in tokens)
            {
                Console.WriteLine("Origial: {0}", token);
                stemed_token = myStemmer.stemTerm(token);
               stemedtokens.Add(stemed_token);
                Console.WriteLine("step0\nstems:\n{0}", stemed_token);
            }
            //stemedtokens.ToArray();
            return stemedtokens.ToArray();


        }


        public string StemTokens_6(string text)
        {
            string[] tokens = TokeniseString(text);
            // List<string> stemedtokens = new List<string>;
            List<string> stemedtokens = new List<string>();
            //myStemmer.stem(tokens);
            //string token="";
            // foreach token in tokens;
            string stemed_token;
           
            foreach (string token in tokens)
            {
                Console.WriteLine("Origial: {0}", token);
                stemed_token = myStemmer.stemTerm(token);
                stemedtokens.Add(stemed_token);
                Console.WriteLine("stems:\n{0}", stemed_token);
            }
            //stemedtokens.ToArray();
            //return stemedtokens.ToArray();
            string text_after_stem="";
            foreach (string token in stemedtokens.ToArray())
            {
                text_after_stem += (token + " ");
            }
            return text_after_stem;
            //int[] i = new int[3] { 1,2,3};
            //i[1] = 3;
            //i[2] = 4;
            //i[0] = 3;

        }



        public string OutputStems()//I didn'd  use this fuctuon I realize the printing function in steamtokens.
        {
            string output_result="";
            for (int i = 0; i < tokenCount.Count-1; i++)
            {
                Console.WriteLine("Token is {0} number of occurances are {1}", tokenCount.Keys.ElementAt(i), tokenCount.Values.ElementAt(i));

                output_result += ("Token is " + tokenCount.Keys.ElementAt(i) + " number of occurances are " + tokenCount.Values.ElementAt(i)+"\r\n");
            }
            return output_result;

        }

        public void CountOccuances(string text)//counter will use dictionary. key-string and vaule-int. the value will recoard the number of the times it happen.(if we can get the idem again ,we add 1)
        {

            string[] tokens = TokeniseString(text);
            int counter;
            foreach (string token in tokens)
            {
                if (tokenCount.TryGetValue(token, out counter))
                {
                    tokenCount[token] += 1;
                }
                else {
                    tokenCount.Add(token, 1);
                }

            }

        }

        public void OutputTokenCount(string text) {//use the previous method we can get a dictionary with full info. so this method will print it out. dict.count means the number of items in the dictionary. dict.keys,ElementAt(i) can get the dictionary value by index.
            Console.WriteLine("Origianl:{0}",text);
            CountOccuances(text);
            for (int i = 0; i < tokenCount.Count; i++)
            {
                Console.WriteLine("Token is {0} number of occurances are {1}", tokenCount.Keys.ElementAt(i), tokenCount.Values.ElementAt(i));
            } 
        }

        public void ProcessText(string input_path, string output_path)//, string output_path
        {
            string text = System.IO.File.ReadAllText(input_path);

            OutputTokenCount(text);
            CountOccuances( StemTokens_6(text));
            string output_result=OutputStems();


           System.IO.File.WriteAllText(output_path, output_result);

        }


        public void ProcessText_7(string input_path, string output_path)//, string output_path
        {
            Boolean stop_7;
            string text = System.IO.File.ReadAllText(input_path);

            string[] output_7 = TokeniseString(text);
            string after_stop_words = "";
            foreach (string token in output_7)
            {
                if (!stopwords.TryGetValue(token,out stop_7))
                {
                    after_stop_words += (token + " ");
                }
            }
          //  Console.WriteLine(after_stop_words);
            //OutputTokenCount(after_stop_words);
            CountOccuances(StemTokens_6(after_stop_words));
            string output_result = OutputStems();

             
            System.IO.File.WriteAllText(output_path, output_result);

        }
        static void Main(string[] args)
        {


            TextAnalyser textAnalyser = new TextAnalyser();

            //System.Console.WriteLine("Activity 3");
            //string text1 = "Tokenising, even in english, is a difficult problem. It's even harder in other languages - such as Chinese!";
            //textAnalyser.OutputTokens(text1);

            string test2 = "The,Daily,Star,The,Daily,Planet,Daily,News,News,of,the,Day,New,New,York,News";
            string test3 = "organisation";
         textAnalyser.StemTokens(test2);

            //System.IO.File.WriteAllText(output_path, test);
           // activity 5
            //string test31 = "the word \"the\" is the most common word in the English language";
            //textAnalyser.OutputTokenCount(test3);

            //activity 6
            //string input_path = "./AlicePara.txt";
            //string output_path = "./outputResult.txt";
            //textAnalyser.ProcessText(input_path, output_path);


            ////activity7
            //string input_path_7 = "./AlicePara.txt";
            //string output_path_7 = "./outputResult_without_stop_words.txt";
            //textAnalyser.ProcessText_7(input_path_7, output_path_7);

            System.Console.ReadKey();

        }



    }
}
