using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Education.DTO;

namespace Education.Services
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResponseDto> EnrollAsync(Guid userId, EnrollmentCreateDto dto);
        Task<IEnumerable<EnrollmentResponseDto>> GetMyCoursesAsync(Guid userId);
        Task<bool> UnenrollAsync(Guid userId, int courseId);
        Task<IEnumerable<EnrollmentResponseDto>> GetEnrolledStudentsAsync(int courseId);
    }
}
