using Lucene.Net.Search;
using FieldInvertState = Lucene.Net.Index.FieldInvertState;

namespace LuceneAdvancedSearchApplication
{
    public class NewSimilarity : DefaultSimilarity
    {
        public override float Tf(float freq)
        {
            return (float)System.Math.Sqrt(freq);
            //return 1;
        }
    }
}
