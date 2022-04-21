using ELearning.Interfaces.Services;
using ELearning.Models;
using ELearning.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        
        private readonly IStudentService _studentService;
        //private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService)//, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
           // _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Student>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync([FromQuery(Name ="page")] int? page , [FromQuery(Name = "numberOfRows")] int? numberOfRows, CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _studentService.GetStudentsAsync(page, numberOfRows, cancellationToken));
            }
            catch (Exception ex) 
            {
                return Ok(Enumerable.Empty<Student>());
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(long? id , CancellationToken cancellationToken)
        {
            try
            {
                if (id == null) return BadRequest("Student id should be provided");
                return Ok(await _studentService.GetStudentByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateAsync(Student student,CancellationToken cancellationToken)
        {
            try
            {
                if (student == null) return BadRequest();
                if(!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _studentService.CreateStudentAsync(student, cancellationToken));
            }
            catch (ServiceUnavailableException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateAsync(long? id,Student student,CancellationToken cancellationToken)
        {
            try
            {
                if (student == null) return BadRequest("Student data should be provided");
                if (id == null) return BadRequest("Student id should be provided");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _studentService.UpdateStudentAsync(id,student, cancellationToken));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ServiceUnavailableException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(long? id,CancellationToken cancellationToken)
        {
            try
            {
                if (id == null) return BadRequest("Student id should be provided");
                return Ok(await _studentService.DeleteStudentByIdAsync(id, cancellationToken));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ServiceUnavailableException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}