using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.PatternMatching.Result
{
    public partial class MatchingResult
    {
        public MatchingResult(string primary, string secondary)
        {
            Primary = primary;
            Secondary = secondary;
            Value = "";
        }

        [JsonPropertyName("isOverlapping")]
        public bool IsOverlapping { get; set; }

        [JsonPropertyName("primary")]
        public string Primary { get; set; }

        [JsonPropertyName("secondary")]
        public string Secondary { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("expected")]
        public List<string> Occurrences { get; set; } = new List<string>();
    }
}
