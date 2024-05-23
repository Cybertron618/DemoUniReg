using DemoUniReg.Api.Infrastructures.Services;
using DemoUniReg.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUniReg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentDetailController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetStudentDetails()
        {
            var studentDetails = await _studentService.GetStudentDetailsAsync();
            return Ok(studentDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetail>> GetStudentDetail(int id)
        {
            var studentDetail = await _studentService.GetStudentDetailByIdAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }
            return Ok(studentDetail);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDetail>> CreateStudentDetail([FromBody] StudentDetail studentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentService.CreateStudentDetailAsync(studentDetail);
            return CreatedAtAction(nameof(GetStudentDetail), new { id = studentDetail.StudentMIS }, studentDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentDetail(int id, [FromBody] StudentDetail studentDetail)
        {
            if (id != studentDetail.StudentMIS)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _studentService.UpdateStudentDetailAsync(studentDetail);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentDetail(int id)
        {
            var studentDetail = await _studentService.GetStudentDetailByIdAsync(id);
            if (studentDetail == null)
            {
                return NotFound();
            }

            await _studentService.DeleteStudentDetailAsync(id);
            return NoContent();
        }
    }
}
