using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Education.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<CourseVideo> Videos { get; set; } = new List<CourseVideo>();
    }
}
