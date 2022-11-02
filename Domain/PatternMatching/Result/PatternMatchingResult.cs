using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PatternMatching.Result
{
    public class PatternMatchingResult
    {
        public PatternMatchingResult()
        {
            Overlappings = new List<Overlap>();
            Occurences = new List<OccurenceBundle>();
        }
        public List<Overlap> Overlappings { get; private set; }

        public List<OccurenceBundle> Occurences { get; private set; }


        public void AddorUpdateBundle(string word,Occurence occurence)
        {
            if (Occurences.Any(x => x.Word == word))
            {
                Occurences.Find(match: c => c.Word == word)?.Occurences.Add(occurence);
            }
            else
            {
                var bundle = new OccurenceBundle(word);
                bundle.Occurences.Add(occurence);
                Occurences.Add(bundle);
            }
        }
    }
}
