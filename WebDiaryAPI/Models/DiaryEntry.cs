using System.ComponentModel.DataAnnotations;

namespace WebDiaryAPI.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a title!")]
        public string Title { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Please add some content!")]
        public string Content { get; set; } 
        [Required(ErrorMessage = "Please select a date!")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
