using Architecture_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var results = await _courseRepository.GetAllCourseAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            try
            {
                if (course == null)
                {
                    return BadRequest("Course object is null.");
                }

                await _courseRepository.AddCourseAsync(course);

                return CreatedAtAction(nameof(GetAllCourses), new { id = course.CourseId }, course);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
        [HttpDelete]
        [Route("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(id);
                if (course == null)
                {
                    return NotFound();
                }

                await _courseRepository.DeleteCourseAsync(id);

                return Ok("Course deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
        [HttpPut]
        [Route("EditCourse/{id}")]
        public async Task<IActionResult> EditCourse(int id, [FromBody] Course course)
        {
            try
            {
                if (course == null || id != course.CourseId)
                {
                    return BadRequest("Invalid course data or ID mismatch.");
                }

                var existingCourse = await _courseRepository.GetCourseByIdAsync(id);
                if (existingCourse == null)
                {
                    return NotFound("Course not found.");
                }

                existingCourse.Name = course.Name;
                existingCourse.Description = course.Description;
                existingCourse.Duration = course.Duration;

                await _courseRepository.UpdateCourseAsync(existingCourse);

                return Ok("Course updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

    }
}


