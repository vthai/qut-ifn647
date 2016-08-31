using System;
using System.Collections.Generic;
using System.Text;

namespace InvertedList
{
    class InvertedList
    {
        private Dictionary<string, HashSet<int>> invertedList = new Dictionary<string, HashSet<int>>();

        private Dictionary<string, Dictionary<int, int>> invertedListComplex = new Dictionary<string, Dictionary<int, int>>();

        private HashSet<string> stopWords = new HashSet<string>();

        private PorterStemmerAlgorithm.PorterStemmer porterStemmer;

        public InvertedList() {
            porterStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            initializeStopWords();
        }

        private void initializeStopWords() {
            System.IO.StreamReader file = new System.IO.StreamReader(@"./english.stop");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                stopWords.Add(line);
            }

            file.Close();
        }

        private string[] tokenize(String document) {
            // Terms are folded to lower case
            document = document.ToLower();
            // Punctuation and non-alphanumeric characters are removed
            document = document.Replace(",", String.Empty)
                .Replace(")", String.Empty)
                .Replace("(", String.Empty);
            
            string[] tokens = document.Split(new char[] {' ', '\r', '\n'});

            // stopwords are removed using the SMART stopword list
            tokens = RemoveStopWords(tokens);
            // The Porter Stemmer algorithm is applied 
            tokens = StemTokens(tokens);

            return tokens;
        }

        public void createInvertedList(List<string> documents) {
            int documentIndex = 0;
            foreach (string document in documents) {

                string[] tokens = tokenize(document);
                
                foreach (string token in tokens) {
                    HashSet<int> posting;
                    if (invertedList.TryGetValue(token, out posting)) {
                        posting.Add(documentIndex);
                    } else {
                        posting = new HashSet<int>();
                        posting.Add(documentIndex);
                        invertedList.Add(token, posting);
                    }
                }
                documentIndex++;
            }
        }

        private string[] StemTokens(string[] tokens)
        {
            List<string> array = new List<string>();
            foreach (string token in tokens)
            {
                string temp = porterStemmer.stemTerm(token);
                array.Add(temp);
            }
            return array.ToArray();
        }

        private string[] RemoveStopWords(string[] tokens)
        {
            List<string> filteredList = new List<string>();

            foreach (string token in tokens)
            {
                if (token.Length > 2)
                {
                    if (!stopWords.Contains(token))
                    {
                        filteredList.Add(token);
                    }
                }
            }

            return filteredList.ToArray();
        }

        public void createInvertedListComplex(List<string> documents)
        {
            int documentIndex = 0;
            foreach (string document in documents)
            {
                string[] tokens = tokenize(document);

                foreach (string token in tokens)
                {
                    Dictionary<int, int> posting;
                    if (invertedListComplex.TryGetValue(token, out posting))
                    {
                        int docTermCount;
                        if (posting.TryGetValue(documentIndex, out docTermCount))
                        {
                            posting[documentIndex] = docTermCount + 1;
                        } else {
                            posting.Add(documentIndex, 1);
                        }
                    }
                    else
                    {
                        posting = new Dictionary<int, int>();
                        posting.Add(documentIndex, 1);
                        invertedListComplex.Add(token, posting);
                    }
                }
                documentIndex++;
            }
        }

        public void printInvertedList() {
            foreach (var pair in invertedList) {
                System.Console.Write(pair.Key + "->");
                foreach (int docId in pair.Value) {
                    Console.Write(docId + ";");
                }
                Console.WriteLine();
            }
        }

        public void printInvertedListSorted() {
            List<string> keys = new List<string>(invertedList.Keys);
            keys.Sort();

            foreach (string key in keys) {
                Console.Write(key + "->");
                HashSet<int> docIds;
                invertedList.TryGetValue(key, out docIds);
                foreach (int docId in docIds) {
                    Console.Write((docId+1) + ";");
                }
                Console.WriteLine();
            }
        }

        public void printInvertedListComplexSorted()
        {
            List<string> keys = new List<string>(invertedListComplex.Keys);
            keys.Sort();

            foreach (string key in keys)
            {
                int totalOccurrence = 0;
                Dictionary<int, int> docIdsMap;
                invertedListComplex.TryGetValue(key, out docIdsMap);
                StringBuilder postingInfo = new StringBuilder();
                foreach (var pair in docIdsMap)
                {
                    postingInfo.Append((pair.Key + 1));
                    postingInfo.Append(":");
                    postingInfo.Append(pair.Value);
                    postingInfo.Append(";");
                    totalOccurrence += pair.Value;
                    //Console.Write((docId + 1) + ";");
                }
                Console.WriteLine(key + ":" + totalOccurrence + "," + postingInfo.ToString());
            }
        }

        static void Main(string[] args)
        {
            List<string> documents = new List<string>();
            //documents.Add("The magical world of Oz");
            //documents.Add("The mad, mad, mad world");
            //documents.Add("Possum Magic");

            documents.Add("Love to love you, baby");
            documents.Add("Love me, love you, baby");
            documents.Add("Nobody Loves No Baby (Like My Baby Loves Me)");

            InvertedList invertedList = new InvertedList();
            //invertedList.createInvertedList(documents);
            //invertedList.printInvertedListSorted();
            invertedList.createInvertedListComplex(documents);
            invertedList.printInvertedListComplexSorted();

            Console.ReadKey();
        }
    }
}
