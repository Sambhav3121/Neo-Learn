using System;

namespace Education.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
