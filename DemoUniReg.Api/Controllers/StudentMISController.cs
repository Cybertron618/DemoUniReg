using DemoUniReg.Api.Infrastructures.Services;
using DemoUniReg.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUniReg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentMISController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentMIS>>> GetStudentMISs()
        {
            var studentMISs = await _studentService.GetStudentMISsAsync();
            return Ok(studentMISs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentMIS>> GetStudentMIS(int id)
        {
            var studentMIS = await _studentService.GetStudentMISByIdAsync(id);
            if (studentMIS == null)
            {
                return NotFound();
            }
            return Ok(studentMIS);
        }

        [HttpPost]
        public async Task<ActionResult<StudentMIS>> CreateStudentMIS(StudentMIS studentMIS)
        {
            await _studentService.CreateStudentMISAsync(studentMIS);
            return CreatedAtAction(nameof(GetStudentMIS), new { id = studentMIS.SchoolID }, studentMIS);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentMIS(int id, StudentMIS studentMIS)
        {
            if (id != studentMIS.SchoolID)
            {
                return BadRequest();
            }

            await _studentService.UpdateStudentMISAsync(studentMIS);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentMIS(int id)
        {
            await _studentService.DeleteStudentMISAsync(id);
            return NoContent();
        }
    }
}
