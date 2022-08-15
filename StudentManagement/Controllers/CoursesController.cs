using CoursesManagement.Models;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService courseService;

        public CoursesController(ICoursesService courseService)
        {
            this.courseService = courseService;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Courses>> Get()
        {
            return courseService.Get();
        }
        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public ActionResult<Courses> Get(string id)
        {
            var course = courseService.Get(id);
            if (course == null)
            {
                return NotFound($"Course with Id = {id} not found");
            }
            return course;
        }

        // POST api/<CoursesController>
        [HttpPost]
        public ActionResult<Courses> Post([FromBody] Courses course)
        {
            courseService.Create(course);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Courses course)
        {
            var ExistingCourse = courseService.Get(id);
            if (ExistingCourse == null)
            {
                return NotFound($"Course with Id = {id} not found");
            }
            courseService.Update(id, course);
            return NoContent();
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = courseService.Get(id);
            if (student == null)
            {
                return NotFound($"Course with Id = {id} not found");
            }
            courseService.Remove(student.Id.ToString());
            return Ok($"Course with Id = {id} deleted");
        }
    }
}
