using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{

    class Poem
    {
        List<string> list;
        int i;

        public Poem()
        {
            list = new List<string>();
            list.Add("Ring a ring a rosey");
            list.Add("A pocket full of posey"); 
            list.Add("A-tishooo A-tishooo"); 
            list.Add("We all fall down");
            i = 0;
        }

        public string getNextLine(){
            string line  = list[i];
            i++;
            if(i == list.Count) i =0;
            return line;
        }
    }
}
