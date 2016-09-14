using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficiencyTest
{
    class EfficiencyTest
    {
        Dictionary<string, bool> stopwordList = new Dictionary<string, bool>();
        PorterStemmerAlgorithm.PorterStemmer myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
        private void initializeStopWords()
        {
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

        EfficiencyTest()
        {
        
        }

        /// <summary>
        /// Sends the thread to sleep and outputs the sleep time
        /// </summary>
        public void TestLinguisticProcessingEfficiency(string text){
            //Console.WriteLine("Sending system to sleep...zzzz");
            DateTime start = System.DateTime.Now;
            //System.Threading.Thread.Sleep(millisleep);
            string[] strs = TokeniseString(text);
            DateTime end = System.DateTime.Now;
            Console.WriteLine("Time to tokenize: " + (end - start));

            DateTime start2 = System.DateTime.Now;
            //System.Threading.Thread.Sleep(millisleep);
            string[] strsNonStopWords = RemoveStopWords(strs);
            DateTime end2 = System.DateTime.Now;
            Console.WriteLine("Time to removes stopWords: " + (end2 - start2));

            DateTime start3 = System.DateTime.Now;
            //System.Threading.Thread.Sleep(millisleep);
            StemTokens(strsNonStopWords);
            DateTime end3 = System.DateTime.Now;
            Console.WriteLine("Time to stem: " + (end3 - start3));

        }

        public string[] TokeniseString(string text)
        {
            text = text.ToLower();
            return text.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
        }


        public string[] RemoveStopWords(string[] tokens)
        {
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


        public string[] StemTokens(string[] tokens)
        {
            List<string> array = new List<string>();
            foreach (string token in tokens)
            {
               // Console.WriteLine("Orginal: " + token);
                string temp = myStemmer.stemTerm(token);
                array.Add(temp);
               // Console.WriteLine("Stems: " + temp);
            }
            return array.ToArray();
        }

        /// <summary>
        /// Sends the thread to sleep and outputs the sleep time
        /// </summary>
        public void testSleepEfficiency(int millisleep)
        {
            Console.WriteLine("Sending system to sleep...zzzz");
            DateTime start = System.DateTime.Now;
            System.Threading.Thread.Sleep(millisleep);
            DateTime end = System.DateTime.Now;
            Console.WriteLine("The sleep lasted for " + (end - start) + "\n");
        }

        static void Main(string[] args)
        {
            EfficiencyTest test = new EfficiencyTest();

            test.testSleepEfficiency(2000);
            test.TestLinguisticProcessingEfficiency(AliceText.Text);
            
            Console.ReadLine();
        }



    }
}
