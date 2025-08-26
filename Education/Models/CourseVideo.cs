using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Models
{
    public class CourseVideo
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string VideoUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
