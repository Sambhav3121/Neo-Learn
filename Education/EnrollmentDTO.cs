namespace Education.DTO
{
    public class EnrollmentCreateDto
    {
        public int CourseId { get; set; }
    }

    public class EnrollmentResponseDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public DateTime EnrolledAt { get; set; }
    }
}
