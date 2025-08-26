using Education.Models;
using Education.Data;
using Education.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.BLL
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return new CourseDto { Id = course.Id, Title = course.Title, Description = course.Description, Videos = new List<CourseVideoDto>() };
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.Include(c => c.Videos).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return null;
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Videos = course.Videos.Select(v => new CourseVideoDto { Id = v.Id, VideoUrl = v.VideoUrl, Description = v.Description }).ToList()
            };
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            return await _context.Courses.Include(c => c.Videos)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Videos = c.Videos.Select(v => new CourseVideoDto { Id = v.Id, VideoUrl = v.VideoUrl, Description = v.Description }).ToList()
                }).ToListAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CourseVideoDto> AddVideoAsync(int courseId, AddVideoDto dto)
        {
            var course = await _context.Courses.Include(c => c.Videos).FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null) return null;
            var video = new CourseVideo { VideoUrl = dto.VideoUrl, Description = dto.Description, CourseId = courseId };
            course.Videos.Add(video);
            await _context.SaveChangesAsync();
            return new CourseVideoDto { Id = video.Id, VideoUrl = video.VideoUrl, Description = video.Description };
        }
    }
}

