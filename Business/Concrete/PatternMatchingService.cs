using Business.Abstract;
using Domain.PatternMatching.Request;
using Domain.PatternMatching.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PatternMatchingService : IPatternMatchingService
    {
        public PatternMatchingResult Match(PatternMatchingRequest input)
        {
            throw new NotImplementedException();
        }

        public async Task<PatternMatchingResult> MatchAsync(PatternMatchingRequest input)
        {
            var result =  new PatternMatchingResult();
            await Task.WhenAll(GetOverlappingsAsync(result, input), GetOccurencesAsync(result, input));
            return result;
        }

        private Task GetOverlappingsAsync(PatternMatchingResult result, PatternMatchingRequest input)
        {
            try
            {
                var words = input.Text?.Split(' ') ?? Array.Empty<string>();
                foreach (var word in words)
                {
                    if (word != null)
                    {
                        var firstOccurrenceIndex = 0;
                        byte count = 0;
                        if (word.Contains(input.Word[0]))
                        {
                            count++;
                            firstOccurrenceIndex = word.IndexOf(input.Word[0]);
                            for (int i = 1; i < input.Word.Length; i++)
                            {
                                if (word.Length > firstOccurrenceIndex + i && word[firstOccurrenceIndex + i] == input.Word[i])
                                    count++;
                                else
                                    break;
                            }
                        }
                        if (count > 0)
                        {
                            result.Overlappings.Add(new(word.Substring(firstOccurrenceIndex, count), count));
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
        private Task GetOccurencesAsync(PatternMatchingResult result, PatternMatchingRequest input)
        {
            try
            {
                var words = input.Text?.Split(' ') ?? Array.Empty<string>();
                foreach (var word in words)
                {
                    int lastIndex = 0;
                    foreach (var c in input.Word)
                    {
                        if (word.Substring(lastIndex).Contains(c))
                        {
                            var recent = lastIndex;
                            lastIndex = word.Substring(lastIndex).IndexOf(c);
                            result.AddorUpdateBundle(word, new(c, recent+lastIndex+1));
                            lastIndex = recent + lastIndex;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    }
}
