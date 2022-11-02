using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PatternMatching.Result
{
    public class OccurenceBundle
    {
        public OccurenceBundle(string word)
        {
            Word = word;
            Occurences = new List<Occurence>();
        }
        public string Word { get; set; }

        public List<Occurence> Occurences { get; set; }
    }
}
