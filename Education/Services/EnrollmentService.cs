using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Education.DTO;
using Education.Models;
using Education.Data;

namespace Education.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentResponseDto> EnrollAsync(Guid userId, EnrollmentCreateDto dto)
        {
            var exists = await _context.Enrollments.FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == dto.CourseId);
            if (exists != null) return null;

            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = dto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            var course = await _context.Courses.FindAsync(dto.CourseId);

            return new EnrollmentResponseDto
            {
                EnrollmentId = enrollment.Id,
                CourseId = dto.CourseId,
                CourseTitle = course.Title,
                EnrolledAt = enrollment.EnrolledAt
            };
        }

        public async Task<IEnumerable<EnrollmentResponseDto>> GetMyCoursesAsync(Guid userId)
        {
            return await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .Select(e => new EnrollmentResponseDto
                {
                    EnrollmentId = e.Id,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course.Title,
                    EnrolledAt = e.EnrolledAt
                }).ToListAsync();
        }

        public async Task<bool> UnenrollAsync(Guid userId, int courseId)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
            if (enrollment == null) return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EnrollmentResponseDto>> GetEnrolledStudentsAsync(int courseId)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .Include(e => e.Course)
                .Select(e => new EnrollmentResponseDto
                {
                    EnrollmentId = e.Id,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course.Title,
                    EnrolledAt = e.EnrolledAt
                }).ToListAsync();
        }
    }
}
