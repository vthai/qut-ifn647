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

namespace QueryExpansionLucene
{
    class QueryExpansionLucene
    {

        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        IndexSearcher searcher;
        QueryParser parser;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TEXT_FN = "Text";

        public QueryExpansionLucene()
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
        /// Searches the index for the querytext and displays a ranked list of results to the screen
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public void SearchAndDisplayResults(string querytext)
        {

            System.Console.WriteLine("Searching for " + querytext);
            querytext = querytext.ToLower();
            Query query = parser.Parse(querytext);

            TopDocs results = searcher.Search(query, 100);

            int rank = 0;
            foreach (ScoreDoc scoreDoc in results.ScoreDocs)
            {
                rank++;
                Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                string myFieldValue = doc.Get(TEXT_FN).ToString();
                Console.WriteLine("Rank " + rank + " text " + myFieldValue);
            }

        }



        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }

        /// <summary>
        /// Creates a Thesuaris of stems
        /// </summary>
        /// <returns>A a Thesuaris of stems in the form: <stem,list of words> </returns>
        public Dictionary<string, string[]> CreateThesaurus()
        {
            Dictionary<string, string[]> thesaurus = new Dictionary<string, string[]>();

            //TODO: Add terms to the thesaurus
            thesaurus.Add("walk", new String[] { "walk", "walked", "walking" });
            thesaurus.Add("run", new String[] {"run", "running" });
            thesaurus.Add("love", new String[] { "love", "lovely", "loving" });


            return thesaurus;
        }

        /// <summary>
        /// Expands the query with terms in the thesaurus
        /// </summary>
        /// <param name="thesaurus">A thesaurus of stems and associated terms</param>
        /// <param name="query">a query to stem</param>
        /// <returns>the query expanded with words that share the stem</returns>
        public string GetExpandedQuery(Dictionary<string, string[]> thesaurus, string queryTerm)
        {
            string expandedQuery = "";
            string[] terms = thesaurus[queryTerm];
            foreach(string s in terms)
            {
                expandedQuery += s+" ";
            }
            

            //TODO: Expand the query with shared terms

            return expandedQuery;   
        }
        
      public string GetWeightedExpandedQuery(Dictionary<string, string[]> thesaurus, string queryTerm)
        {
            string WeightedExpandedQuery = "";
            string[] terms = thesaurus[queryTerm];
            bool firstword = true;
            foreach (string s in terms)
            {
                if (firstword== true)
                {
                    WeightedExpandedQuery += s + "^5" + " ";
                    firstword = false;
                }
                else
                {
                    WeightedExpandedQuery += s+ " ";
                }
                
            }


            //TODO: Expand the query with shared terms

            return WeightedExpandedQuery;
        }
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello Lucene.Net");

            QueryExpansionLucene myLuceneApp = new QueryExpansionLucene();

            // source collection
            List<string> l = new List<string>();
            l.Add("All my loving");
            l.Add("Band on the run");
            l.Add("Born to run");
            l.Add("Can\'t stop loving you");
            l.Add("Can\'t stop running");
            l.Add("Long train running");
            l.Add("Love will tear us apart");
            l.Add("Love makes the world go round");
            l.Add("Lovely Rita");
            l.Add("These boots were made for walking");
            l.Add("Take a walk on the wild side");
            l.Add("Reverse Running");
            l.Add("Run to the hills");
            l.Add("Walk this way");
            l.Add("Walk on in");
            l.Add("Walk like a man");
            l.Add("Walking on sunshine");
            l.Add("We\'ve lost that loving feeling");
            l.Add("You\'ll never walk alone");



            // Index code
            string indexPath = @"c:\temp\Week10Index";
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

            Dictionary<string, string[]> thesaurus = myLuceneApp.CreateThesaurus();
           //string expandQuery = myLuceneApp.GetExpandedQuery(thesaurus, "walk");
            string expandQuery = myLuceneApp.GetWeightedExpandedQuery(thesaurus, "walk");
            myLuceneApp.SearchAndDisplayResults(expandQuery);
            //expandQuery = myLuceneApp.GetExpandedQuery(thesaurus, "run");
            expandQuery = myLuceneApp.GetWeightedExpandedQuery(thesaurus, "run");
            myLuceneApp.SearchAndDisplayResults(expandQuery);
            //expandQuery = myLuceneApp.GetExpandedQuery(thesaurus, "love");
            expandQuery = myLuceneApp.GetWeightedExpandedQuery(thesaurus, "run");
            myLuceneApp.SearchAndDisplayResults(expandQuery);
            


            Console.ReadLine();
            myLuceneApp.CleanUpSearcher();


        }
    }
}
