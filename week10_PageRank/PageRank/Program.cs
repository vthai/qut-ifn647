using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRank
{
    class Program
    {
        static void Main(string[] args)
        {
            Program myLuceneApp = new Program();
            string indexPath = @"./indexfolder/";
            myLuceneApp.CreateIndex(indexPath);
            List<string[]> l = new List<string[]>();
            l.Add(new string[] { "1", "Stephen Hawking", "A Brief History of Time" ,"5"});
            l.Add(new string[] { "2", "Stephen Hawking", "The nature of space and time", "1" });
            l.Add(new string[] { "3", "Albert Einstein", "The Influence of the Expansion of Space on the  Gravitation Fields Surrounding the Individual Stars ", "200" });
            l.Add(new string[] { "4", "Richard Feynman", "Space-Time Approach to Non Relativistic Quantum Mechanics", "10" });
            l.Add(new string[] { "5", "Alan Woodley", "Space is cool!", "15" });

            foreach (string[] s in l)
            {
                myLuceneApp. IndexText( s[1],s[2],int.Parse(s[3]));
            }

            // clean up
            myLuceneApp.CleanUpIndexer();

            myLuceneApp.CreateSearcher();
            myLuceneApp.CreateParser();
            string line = null;
            do
            {
                Console.Write("Input the search term: ");
                line = Console.ReadLine();
                if (line == "")
                {
                    break;
                }
                else
                {
                    TopDocs topDocs = myLuceneApp.SearchIndex(line);
                    myLuceneApp.DisplayResults(topDocs);
                    Console.WriteLine();
                }

            } while (line != null);


            // clean up
            myLuceneApp.CleanUpIndexer();
            myLuceneApp.CleanUpSearcher();

            System.Console.ReadLine();




        }

        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;

        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        TopDocs topDocs;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        const string AUTHOR = "Author";
        const string TITLE = "Title";
       // const string TEXT_FN = "Text";

        public Program()
        {
            luceneIndexDirectory = null;
            analyzer = null;
            writer = null;
        }

        public void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
        }

        public void CreateParser()
        {
            //parser = new QueryParser(VERSION, TEXT_FN, analyzer);
            parser = new QueryParser(VERSION, TITLE, analyzer);
        }
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }
        public TopDocs SearchIndex(string query_pa)
        {
            Query query = parser.Parse(query_pa);
            topDocs = searcher.Search(query, 100);
            // int i = topDocs.TotalHits;
            Console.WriteLine("Number of results is " + topDocs.TotalHits);
            return topDocs;
        }

        public void DisplayResults(TopDocs topDocs)
        {
            int i = 1;
            foreach (ScoreDoc scoreDoc in topDocs.ScoreDocs)
            {
                Document doc = searcher.Doc(scoreDoc.Doc);
                string myTitleValue = doc.Get(TITLE).ToString();
                string myAuthorValue = doc.Get(AUTHOR).ToString();
                Console.WriteLine("Rank no. " + i + ": title: " + myTitleValue + " author: "+ myAuthorValue);
                i++;

            }
        }

        /// <summary>
        /// Creates the index at indexPath
        /// </summary>
        /// <param name="indexPath">Directory path to create the index</param>
        public void CreateIndex(string indexPath)
        {
            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);

        }

        /// <summary>
        /// Indexes the given text
        /// </summary>
        /// <param name="text">Text to index</param>
        public void IndexText(string author,string title,int pageRank)
        {
            
            Lucene.Net.Documents.Field field1 = new Field(AUTHOR, author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);

            Lucene.Net.Documents.Field field2 = new Field(TITLE, title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);

            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(field1);
            doc.Add(field2);
            doc.Boost = pageRank;
            writer.AddDocument(doc);
        }

        /// <summary>
        /// Flushes buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }
    }
}
