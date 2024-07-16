using System.ComponentModel.DataAnnotations;

namespace BookReview.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}