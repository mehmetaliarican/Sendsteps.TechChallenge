using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PatternMatching.Result
{
    public record Overlap(string word,byte overlappingCharacterCount);

    public record Occurence(char character,int index);
}
