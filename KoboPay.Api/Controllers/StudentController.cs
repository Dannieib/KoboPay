using KoboPay.Logic.Dtos;
using KoboPay.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KoboPay.Api.Controllers
{
    [Route("api/v1/student")]
    [ApiController]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [ProducesResponseType(typeof(BaseResponseModel<Guid>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateUpdateStudentDto createUpdateStudentDto)
        {
            var add = await _studentService.CreateStudentAsync(createUpdateStudentDto);
            return Ok(add);
        }

        [ProducesResponseType(typeof(BaseResponseModel<bool>), (int)HttpStatusCode.OK)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] CreateUpdateStudentDto createUpdateStudentDto)
        {
            var update = await _studentService.UpdateStudentAsync(id,createUpdateStudentDto);
            return Ok(update);
        }

        [ProducesResponseType(typeof(BaseResponseModel<bool>), (int)HttpStatusCode.OK)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FetchStudent([FromRoute] Guid id)
        {
            var update = await _studentService.GetStudentByIdAsync(id);
            return Ok(update);
        }

        [ProducesResponseType(typeof(BaseResponseModel<bool>), (int)HttpStatusCode.OK)]
        [HttpGet("get-students")]
        public async Task<IActionResult> FetchAllStudent()
        {
            var update = await _studentService.GetAllStudentAsync();
            return Ok(update);
        }

        [ProducesResponseType(typeof(BaseResponseModel<Guid>), (int)HttpStatusCode.OK)]
        [HttpPost("add-student-courses/{studentid:guid}")]
        public async Task<IActionResult> AddStudentCourses([FromBody] List<StudentCourseDto> studentCourses)
        {
            await _studentService.AddStudentCourses(studentCourses);
            return Ok();
        }

        [ProducesResponseType(typeof(BaseResponseModel<Guid>), (int)HttpStatusCode.OK)]
        [HttpPut("update-student-courses/{id:guid}")]
        public async Task<IActionResult> UpdateStudentCourses([FromRoute] Guid id, [FromBody] StudentCourseDto studentCourses)
        {
            await _studentService.UpdateStudentCourse(id,studentCourses);
            return Ok();
        }
    }
}
