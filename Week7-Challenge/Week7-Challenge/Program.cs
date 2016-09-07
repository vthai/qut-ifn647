using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Week7_Challenge
{
    class Bigram
    {
        public List<string> bigrams(string sentence)
        {
            string[] tokens = sentence.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            List<string> bigrams = new List<string>();

            for (int i = 0; i < tokens.Length - 1; i++)
            {
                bigrams.Add(tokens[i] + " " + tokens[i + 1]);
            }

            return bigrams;
        }

        public void printBigrams(List<string> bigrams)
        {
            foreach (string bigram in bigrams)
            {
                Console.WriteLine(bigram);
            }
        }

        static void Main(string[] args)
        {
            String sentence = "I love information retrieval";

            Bigram bigramProg = new Bigram();
            List<string> bigrams = bigramProg.bigrams(sentence);

            bigramProg.printBigrams(bigrams);

            Console.WriteLine("\nNow work on Alice text\n");

            string text = System.IO.File.ReadAllText("./AliceText.txt");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bigrams = bigramProg.bigrams(text);
            stopWatch.Stop();

            Console.WriteLine(bigrams.Count + " bigrams found, it took " + stopWatch.Elapsed.TotalSeconds + " seconds");

            //bigramProg.printBigrams(bigrams);

            Console.ReadKey();
        }
    }
}
