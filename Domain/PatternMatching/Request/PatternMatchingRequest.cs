using System.ComponentModel.DataAnnotations;

namespace Domain.PatternMatching.Request
{
    public class PatternMatchingRequest
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Please provide primary value to be compared with the given word")]
        public string Primary { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide primary value to be compared with the given word")]
        public string Secondary { get; set; }
    }
}
