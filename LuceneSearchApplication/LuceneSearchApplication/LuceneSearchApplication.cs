﻿using System;
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

namespace LuceneApplication
{
    class LuceneApplication
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;

        Lucene.Net.Search.IndexSearcher searcher;
        Lucene.Net.QueryParsers.QueryParser parser;
        TopDocs topDocs;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        const string TEXT_FN = "Text";

        public LuceneApplication()
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
            parser = new QueryParser(VERSION, TEXT_FN, analyzer);
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
                string myFiledValue = doc.Get(TEXT_FN).ToString();
                Console.WriteLine("Rank no. "+i+": "+myFiledValue);
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
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer,true, mfl);

        }

        /// <summary>
        /// Indexes the given text
        /// </summary>
        /// <param name="text">Text to index</param>
        public void IndexText(string text)
        {
            Lucene.Net.Documents.Field field = new Field(TEXT_FN, text, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(field);
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


        static void Main(string[] args)
        {

            LuceneApplication myLuceneApp = new LuceneApplication();

            // TODO: ADD PATHNAME
            string indexPath = @"./indexfolder/";

            myLuceneApp.CreateIndex(indexPath);

            System.Console.WriteLine("Adding Documents to Index");

            List<string> l = new List<string>();
            l.Add("The magical world of oz");
            l.Add("The mad, mad, mad, mad world");
            l.Add("Possum magic");

            foreach (string s in l)
            {

                System.Console.WriteLine("Adding " + s + "  to Index");
                myLuceneApp.IndexText(s);
            }
            
            System.Console.WriteLine("All documents added.");

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
                else {
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
    }
}