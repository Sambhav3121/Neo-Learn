using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Education.BLL;
using Education.DTO;
using System.Threading.Tasks;

namespace Education.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto courseDto)
        {
            var course = await _courseService.CreateCourseAsync(courseDto);
            return Ok(course);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpPost("{courseId}/add-video")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> AddVideo(int courseId, [FromBody] AddVideoDto dto)
        {
            var video = await _courseService.AddVideoAsync(courseId, dto);
            if (video == null) return NotFound();
            return Ok(video);
        }
    }
}
