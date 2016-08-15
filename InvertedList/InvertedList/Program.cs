using System;
using System.Collections.Generic;

namespace InvertedList
{
    class InvertedList
    {
        private Dictionary<string, HashSet<int>> invertedList = new Dictionary<string, HashSet<int>>();

        private string[] tokenize(String document) {
            document = document.ToLower();
            document = document.Replace(",", String.Empty);
            string[] tokens = document.Split(new char[] {' ', '\r', '\n'});
            //string[] tokens = document.Split(' ');
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

        public void printInvertedList() {
            foreach (var pair in invertedList) {
                System.Console.Write(pair.Key + "->");
                foreach (int docId in pair.Value) {
                    Console.Write(docId + ";");
                }
                Console.WriteLine();
            }
        }
        
        static void Main(string[] args)
        {
            List<string> documents = new List<string>();
            documents.Add("The magical world of Oz");
            documents.Add("The mad, mad, mad world");
            documents.Add("Possum Magic");

            InvertedList invertedList = new InvertedList();
            invertedList.createInvertedList(documents);
            invertedList.printInvertedList();

            Console.ReadKey();
        }
    }
}
