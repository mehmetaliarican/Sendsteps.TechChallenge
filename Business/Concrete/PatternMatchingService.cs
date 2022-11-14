using Business.Abstract;
using Domain.PatternMatching.Request;
using Domain.PatternMatching.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PatternMatchingService : IPatternMatchingService
    {
        private readonly ILogger<PatternMatchingService> _logger;

        public PatternMatchingService(ILogger<PatternMatchingService> logger)
        {
            this._logger = logger;
        }
        public MatchingResult Match(PatternMatchingRequest input)
        {
            var result = new MatchingResult(input.Primary, input.Secondary);
            Task.WaitAll(SetOverLappingValueAsync(result, input),
                    SetOccurencesAsync(result, input));

            result.IsOverlapping = result.Occurrences.Any(x => x == result.Value);
            return result;
        }

        public async Task<MatchingResult> MatchAsync(PatternMatchingRequest input)
        {
            var result = new MatchingResult(input.Primary, input.Secondary);
            await Task.WhenAll(
                    SetOverLappingValueAsync(result, input),
                    SetOccurencesAsync(result, input)
                 );

            result.IsOverlapping = result.Occurrences.Any(x => x == result.Value);
            return result;
        }

        public Task SetOverLappingValueAsync(MatchingResult result, PatternMatchingRequest input)
        {
            try
            {
                result.Value = FindOverlappingWord(input.Primary, input.Secondary);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetOverlapping error");
                return Task.CompletedTask;
            }
        }

        public async Task SetOccurencesAsync(MatchingResult result, PatternMatchingRequest input)
        {
            try
            {
               var results =  await Task.WhenAll(
                          Task.FromResult(FindOccurringWords(input.Primary, input.Secondary)),
                          Task.FromResult(FindOccurringWords(input.Secondary, input.Primary))
                        );

                foreach (var collection in results)
                {
                    result.Occurrences.AddRange(collection);
                }
                var max = result.Occurrences.Max(x => x.Length);
                result.Occurrences = result.Occurrences
                        .Where(x => x.Length == max)
                        .Distinct()
                        .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetExpected error");
            }
        }


        public string FindOverlappingWord(string primary, string secondary)
        {
            var possibilities = new List<string>();
            for (int i = 0; i < primary.Length; i++)
            {
                var pc = primary[i];
                if (pc == secondary[0])
                {
                    var possible = "";
                    for (int j = 0; j < Math.Min(secondary.Length, primary.Length - i); j++)
                    {
                        var sc = secondary[j];
                        if (primary[j + i] == sc)
                        {
                            possible += sc;
                        }
                        else
                        {
                            break;
                        }
                    }
                    possibilities.Add(possible);
                }

            }
            return possibilities.Count > 0 ? possibilities.OrderByDescending(x => x.Length).First() : "";
        }


        //ABABCADCBDADA

        public List<string> FindOccurringWords(string primary, string secondary)
        {

            var possibilities = new List<string>();
            var indexCharDict = new Dictionary<int, char>();
            for (int i = 0; i < secondary.Length; i++)
            {
                indexCharDict.Clear();
                for (int j = i; j < secondary.Length; j++)
                {
                    var indexes = primary.Select((v, i) =>
                    {
                        if (v == secondary[j]) return i;
                        return -1;
                    }).Where(x => x != -1).ToArray();

                    if (indexCharDict.All(di => indexes.Any(x => di.Key < x)) && indexes.Length > 0)
                    {
                        var index = indexes.First(x => indexCharDict.All(di => di.Key < x));
                        indexCharDict.Add(index, secondary[j]);

                    }
                }
                var v = string.Join("", indexCharDict.Select(x => x.Value));
                if (!possibilities.Contains(v))
                    possibilities.Add(v);
            }
            var maxLength = possibilities.Max(x => x.Length);
            return possibilities.Where(x => x.Length == maxLength).ToList();
        }

    }
}
