using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Education.Services;
using Education.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Education.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost("enroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentCreateDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);
            var result = await _service.EnrollAsync(userId, dto);
            if (result == null) return BadRequest("Already enrolled");
            return Ok(result);
        }

        [HttpGet("mycourses")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyCourses()
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);
            var result = await _service.GetMyCoursesAsync(userId);
            return Ok(result);
        }

        [HttpDelete("unenroll/{courseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Unenroll(int courseId)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);
            var result = await _service.UnenrollAsync(userId, courseId);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpGet("course/{courseId}/students")]
        [Authorize(Roles = "Instructor,Admin")]
        public async Task<IActionResult> GetEnrolledStudents(int courseId)
        {
            var result = await _service.GetEnrolledStudentsAsync(courseId);
            return Ok(result);
        }
    }
}
