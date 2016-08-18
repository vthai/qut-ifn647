using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneInvertedList
{
    class Program
    {
        Dictionary<string, string> Result= new Dictionary<string, string>(); // for Activity 5
        
        Dictionary<Dictionary<string, string>, int> Dic_inter = new Dictionary<Dictionary<string, string>, int>();

        //--build Documents
        Dictionary<string, string> Doc1 = new Dictionary<string, string>() { { "the the magical world of oz", "0" } };
        Dictionary<string, string> Doc2 = new Dictionary<string, string>() { { "the mad, mad, mad, world", "1" } };
        Dictionary<string, string> Doc3 = new Dictionary<string, string>() { { "possum Magic , test ,test , test ", "2" } };
        Dictionary<Dictionary<string, string>, int> Documents;
        Dictionary<Dictionary<string, string>, int> Dic_returned;


        bool firstTime = true;

        static void Main(string[] args)
        {
            Program InverterdList = new Program();

            InverterdList.Documents = new Dictionary<Dictionary<string, string>, int>() { { InverterdList.Doc1, 0 } };
            InverterdList.Documents.Add(InverterdList.Doc2, 1);
            InverterdList.Documents.Add(InverterdList.Doc3, 2);


         //interative the Documents ((Dictionary<Dictionary<string, string>, int>)) and get the FinalResult Dictionary<string, string>
            foreach (var doc in InverterdList.Documents)
            {
                int doc_value = doc.Value;
                Dictionary<string, string> doc_test = doc.Key;
                string str = doc_test.Keys.ElementAt(0);
                InverterdList.Dic_returned = InverterdList.CountOccuances(str, doc.Value);

                #region first time Get the splited Dic ((Dictionary<Dictionary<string, string>, int>)) and change to  Restult ( Dictionary<string, string> ) 
                //first time add the return to the Result
                if (InverterdList.firstTime == true)
                {
                    InverterdList.Dic_inter = InverterdList.Dic_returned;
                    InverterdList.firstTime = false;
                    //Dic_inter:{{{"word",Doc_num},Doc_num}  ,{{"word",Doc_num},Doc_num}  ,{{"word",Doc_num},Doc_num}  }
                    //nees dic_inter_simple: {{"word",Doc_nun},{"word",Doc_nun}}

                    foreach (var dic in InverterdList.Dic_inter)//change to dic_inter_simple( Dictionary<string, string> ) from Dic_inter (Dictionary<Dictionary<string, string>, int>)
                    {
                        Dictionary<string, string> doc_test2 = dic.Key;
                        InverterdList.Result.Add(doc_test2.Keys.ElementAt(0), doc_test2.Values.ElementAt(0));
                        //reuslt = {{"word",Doc_nun},{"word",Doc_nun}}
                    }
                }
                #endregion
                //-----
                else
                {
                    #region get Returned ( Dictionary<string, string>) from (Dictionary<Dictionary<string, string>, int>)
                    Dictionary<string, string> Returned = new Dictionary<string, string>();
                    foreach (var dic in InverterdList.Dic_returned)//change to dic_inter_simple from Dic_inter
                    {
                        Dictionary<string, string> doc_test3 = dic.Key;
                        Returned.Add(doc_test3.Keys.ElementAt(0), doc_test.Values.ElementAt(0));
                        //Returned = {{"word",Doc_nun},{"word",Doc_nun}}
                    }
                    #endregion

                    #region compare Retrun and Result; exist add value; NOT exist assign value
                    foreach (var Doc in Returned)
                    {
                        string test2;
                        if (InverterdList.Result.TryGetValue(Doc.Key, out test2))//
                        {
                            InverterdList.Result[Doc.Key] += ";"+Doc.Value;
                        }
                        else
                        {
                            InverterdList.Result[Doc.Key]= Doc.Value;
                        }
                    }
                    #endregion
                }

            }



            foreach (var dic in InverterdList.Result)
            {
                Console.WriteLine(dic.Key + "----->" + dic.Value);
            }
            Console.ReadKey();


        }


        public string[] TokeniseString(string text)
        {
            text = text.ToLower();
            return text.Split(new char[] { ' ', '?', ',', '.', '!', '-', '\'', '"', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        //
        public Dictionary<Dictionary<string, string>, int> CountOccuances(string text , int current_doc_num)
        {
            Dictionary<string, int> mid_dic = new Dictionary<string, int>();

            Dictionary<Dictionary<string, string>, int> return_value = new Dictionary<Dictionary<string, string>, int>();
            string[] tokens = TokeniseString(text);
           int counter;
            
            foreach (string token in tokens)
            {
                if (!mid_dic.TryGetValue(token, out counter))
                {
                    mid_dic[token] = current_doc_num;  
                    return_value.Add(new Dictionary<string, string>() { { token, current_doc_num.ToString() }}, current_doc_num);
                }
            }
            return return_value; 
        }
    }
}
