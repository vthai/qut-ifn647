using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser

namespace LuceneInteractive
{

    
    class LuceneInteractive
    {

        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        IndexSearcher searcher;
        QueryParser parser;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";

        public LuceneInteractive()
        {
            luceneIndexDirectory = null;
            writer = null;
            analyzer = new Lucene.Net.Analysis.SimpleAnalyzer();
            parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, TEXT_FN, analyzer);
        }


        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        public void CreateIndex(string indexPath)
        {
            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
        }


        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        public void IndexText(string text)
        {

            Lucene.Net.Documents.Field field = new Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(field);
            writer.AddDocument(doc);
        }


        /// <summary>
        /// Flushes the buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }


        /// <summary>
        /// Creates the searcher object
        /// </summary>
        public void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
        }

        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public TopDocs SearchText(string querytext)
        {

            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);

            TopDocs results = searcher.Search(query, 100);

            return results;
        }


        /// <summary>
        /// Displays a ranked list of results to the screen
        /// </summary>
        /// <param name="results">A set of results</param>
        public int DisplayResults(TopDocs results)
        {

            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                Console.WriteLine("Rank " + rank + " text " + myFieldValue);
            }
            return rank;
        }
        public void DisplayOneResult(TopDocs topDoc, int i)
        {
            Document oneDoc = searcher.Doc( topDoc.ScoreDocs[i].Doc);
            string resultValue = oneDoc.Get(TEXT_FN).ToString();
            Console.WriteLine("Task 7 Doc " + (i+1) + " text " + resultValue);
        }


        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }


         static void Main(string[] args)
        {
            System.Console.WriteLine("Hello Lucene.Net");

            LuceneInteractive myLuceneApp = new LuceneInteractive();

            // source collection
            List<string> l = new List<string>();
            l.Add("The magic world of oz");
            l.Add("The mad, mad, mad, mad world");
            l.Add("Possum magic");
            

            // Index code
            string indexPath = @"c:\temp\Week8Index";
            myLuceneApp.CreateIndex(indexPath);
            System.Console.WriteLine("Adding Documents to Index");
            foreach (string s in l)
            {
                myLuceneApp.IndexText(s);
            }
            System.Console.WriteLine("All documents added.");
            myLuceneApp.CleanUpIndexer();

            // Searching Code
            myLuceneApp.CreateSearcher();

            TopDocs results = myLuceneApp.SearchText("magic world");
            int maxRank = myLuceneApp.DisplayResults(results);

            //myLuceneApp.DisplayOneResult(results, 1);
            //myLuceneApp.DisplayOneResult(results, 2);
            string i="ab";
            while (!i.Equals(""))
            {
                Console.Write("Please enter the rank index :  ");
                i = Console.ReadLine();

                if (!i.Equals("") && int.Parse(i) <= maxRank && int.Parse(i) > 0  )
                {
                    myLuceneApp.DisplayOneResult(results, int.Parse(i) - 1);
                }
            }
            


            Console.ReadKey();
            myLuceneApp.CleanUpSearcher();


        
        }
    }
}
