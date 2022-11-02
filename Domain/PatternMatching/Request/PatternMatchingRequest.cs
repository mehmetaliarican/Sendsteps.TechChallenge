using System.ComponentModel.DataAnnotations;

namespace Domain.PatternMatching.Request
{
    public class PatternMatchingRequest
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Please provide text value to be compared with the given word")]
        public string Text { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a word to execute matching within given text")]
        public string Word { get; set; }
    }
}
