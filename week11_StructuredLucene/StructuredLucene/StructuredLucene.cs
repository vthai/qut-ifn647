using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for analyser
using Lucene.Net.Analysis.Standard; // for standard analyser
using Lucene.Net.Documents; // for document
using Lucene.Net.Index; //for index writer
using Lucene.Net.QueryParsers; // for query parser
using Lucene.Net.Search;
using Lucene.Net.Store; //for Directory


namespace StructuredLucene
{
    class LuceneApplication
    {

        Lucene.Net.Store.Directory directory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        IndexSearcher searcher;
        QueryParser parser;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string PUBLISHER_FN = "Publisher";

        public LuceneApplication()
        {
            directory = null;
            writer = null;
            searcher = null;
            parser = null;

            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION); ;

        }
        /// <summary>
        /// Creates the index at indexPath
        /// </summary>
        /// <param name="indexPath">Directory path to create the index</param>
        public void CreateIndex(string indexPath)
        {

            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            directory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            writer = new Lucene.Net.Index.IndexWriter(directory, analyzer, true, mfl);

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
        /// Indexes information relating to books
        /// </summary>
        /// <param name="author">The Book's author</param>
        /// <param name="title">The Book's title</param>
        /// <param name="publisher">The Book's publisher</param>
        /// 

        //public void IndexBook(string author, string title, string publisher){
        //    // TODO: ADD CODE

        //    Field titleField = new Field(TITLE_FN, title,Field.Store.YES,Field.Index.ANALYZED);
        //    Field authorField = new Field(AUTHOR_FN, author, Field.Store.YES, Field.Index.ANALYZED);
        //   // authorField.Boost = 15;
        //    Field publisherField = new Field(PUBLISHER_FN, publisher, Field.Store.NO, Field.Index.ANALYZED);
        //    Document doc = new Document();
        //    doc.Add(titleField);
        //    doc.Add(authorField);
        //    doc.Add(publisherField);
        //    writer.AddDocument(doc);
        //}


        public void IndexBookWithObject(Book book)
        {
            // TODO: ADD CODE

            Field titleField = new Field(TITLE_FN, book.Title, Field.Store.YES, Field.Index.ANALYZED);
            Field authorField = new Field(AUTHOR_FN, book.Author, Field.Store.YES, Field.Index.ANALYZED);
            // authorField.Boost = 15;
            Field publisherField = new Field(PUBLISHER_FN, book.Publisher, Field.Store.NO, Field.Index.ANALYZED);
            Document doc = new Document();
            doc.Add(titleField);
            doc.Add(authorField);
            doc.Add(publisherField);
            writer.AddDocument(doc);
        }


        /// <summary>
        /// Creates objects to start up the search
        /// </summary>
        public void SetupSearch()
        {
            searcher = new IndexSearcher(directory);
            // parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, PUBLISHER_FN, analyzer);
            IDictionary<string, float> boosts = new Dictionary<string, float>();
            boosts.Add(AUTHOR_FN, 10);
            boosts.Add(TITLE_FN, 1);
            string[] fields = new string[] { AUTHOR_FN, TITLE_FN };
            parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fields, analyzer, boosts);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CleanupSearch()
        {
            searcher.Dispose();
        }


        public void SearchAndDisplayResults(string querytext)
        {
            // TODO: ADD CODE
                
            Query query = parser.Parse(querytext);
            TopDocs  topDocs = searcher.Search(query, 100);
            int i = 0;
            foreach (ScoreDoc scoreDoc in topDocs.ScoreDocs)
            {
                
                Document doc = searcher.Doc(scoreDoc.Doc);
                string myFiledValue = doc.Get(AUTHOR_FN).ToString()+ " ";
                // myFiledValue += doc.Get(PUBLISHER_FN).ToString() + " ";
                 myFiledValue += doc.Get(TITLE_FN).ToString() + " ";

                Console.WriteLine("Rank no. " + i + ": " + myFiledValue);
                i++;

            }

        }

        static void Main(string[] args)
        {


            System.Console.WriteLine("Hello Lucene.Net");

            LuceneApplication myLuceneApp = new LuceneApplication();

            string indexPath = @"c:\temp\Week9";

            myLuceneApp.CreateIndex(indexPath);

            System.Console.WriteLine("Adding documents to the index - Author, Title, Publisher");
            Book book1 = new Book();
            Book book2 = new Book();
            Book book3 = new Book();
            Book book4 = new Book();
            Book book5 = new Book();
            book1.Author = "James Jones";
            book1.Title = "Green grass";
            book1.Publisher = "Green Books Ltd";

            book2.Author = "James Jones";
            book2.Title = "Tomatoes are red";
            book2.Publisher = "Black Publishing Inc";

            book3.Author = "Bob Black";
            book3.Title = "I'm Black inside";
            book3.Publisher = "Black Publishing Inc";

            book4.Author = "Greg Green";
            book4.Title = "You say tomatoes, I also say tomatoes";
            book4.Publisher = "Green Books Ltd";

            book5.Author = "Greg Green";
            book5.Title = "I love green apples for lunch";
            book5.Publisher = "Black Publishing Inc";


            //myLuceneApp.IndexBook(@"James Jones", "Green grass", "Green Books Ltd");
            //myLuceneApp.IndexBook(@"James Jones", "Tomatoes are red", "Black Publishing Inc");
            //myLuceneApp.IndexBook(@"Bob Black", "I'm Black inside", "Black Publishing Inc");
            //myLuceneApp.IndexBook(@"Greg Green", "You say tomatoes, I also say tomatoes", "Green Books Ltd");
            //myLuceneApp.IndexBook(@"Greg Green", "I love green apples for lunch", "Black Publishing Inc");
            ///
            myLuceneApp.IndexBookWithObject(book1);
            myLuceneApp.IndexBookWithObject(book2);
            myLuceneApp.IndexBookWithObject(book3);
            myLuceneApp.IndexBookWithObject(book4); 
            myLuceneApp.IndexBookWithObject(book5);



            System.Console.WriteLine("All documents added.");

             myLuceneApp.CleanUpIndexer();


            myLuceneApp.SetupSearch();


            string queryText = "green";
            System.Console.WriteLine("Searching for text for " + queryText);                      
            myLuceneApp.SearchAndDisplayResults(queryText);

            myLuceneApp.CleanupSearch();


        
            System.Console.ReadKey();




        }
    }
}