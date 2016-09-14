using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week8Challenge
{
    public partial class Form1 : Form
    {
        List<string> docs = new List<string>()
        {
            "Friday I'll be over you ",
            "Friday I'm in love with ",
            "Friday Night ",
            "Friday on my mind ",
            "Monday morning  ",
            "Monday mourning ",
            "Monday Monday ",
            "Sunday morning ",
            "Sunday night ",
            "Sunday papers ",
            "Sunday sunday  "
        };
        StringBuilder stringBuilder= new StringBuilder();


        public Form1()
        {
            InitializeComponent();
        }

        private void suggestionBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
           string searchStr= searchBox.Text;
            //if (!searchStr.Equals(" "))
            //{
            //    stringBuilder.Append(searchStr);
            //}
            if (searchStr.Substring(searchStr.Length - 1).Equals(" "))
            {
                List<string> words = new List<string>();
                foreach (string doc in docs)
                {
                    string word = doc.Substring(0, doc.IndexOf(" "));
                    if (word.ToLower().Equals(searchStr.Trim().ToLower()))
                    {
                        words.Add(doc);
                    }

                }
                suggestionBox.DataSource = words;
            }

            //else
            //{
            //    List<string> words = new List<string>();
            //    foreach (string doc in docs)
            //    {
            //        string word = doc.Substring(0, doc.IndexOf(" "));
            //        if (word.Equals(stringBuilder.ToString()))
            //        {
            //            words.Add(word);
            //        }

            //    }
            //    suggestionBox.DataSource = words;
            //}


            
        }
    }
}
