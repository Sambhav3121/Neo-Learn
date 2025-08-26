using System.Collections.Generic;
using System.Threading.Tasks;
using Education.DTO;

namespace Education.BLL
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<bool> DeleteCourseAsync(int id);
        Task<CourseVideoDto> AddVideoAsync(int courseId, AddVideoDto dto);
    }
}
