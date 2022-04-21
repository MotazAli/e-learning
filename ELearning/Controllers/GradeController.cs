using ELearning.Interfaces.Services;
using ELearning.Models;
using ELearning.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {

        private readonly IGradeService _gradeService;
        private readonly ILogger<GradesController> _logger;

        public GradesController(IGradeService gradeService, ILogger<GradesController> logger)
        {
            _gradeService = gradeService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Grade>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAsync([FromQuery(Name = "page")] int? page, [FromQuery(Name = "numberOfRows")] int? numberOfRows, CancellationToken cancellationToken)
        {

            try
            {
                return Ok(await _gradeService.GetGradesAsync(page, numberOfRows, cancellationToken));
            }
            catch (Exception ex)
            {
                return Ok(Enumerable.Empty<Grade>());
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Grade))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(long? id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == null) return BadRequest("Grade id should be provided");
                return Ok(await _gradeService.GetGradeByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Grade))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateAsync(Grade grade, CancellationToken cancellationToken)
        {
            try
            {
                if (grade == null) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _gradeService.CreateGradeAsync(grade, cancellationToken));
            }
            catch (ServiceUnavailableException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Grade))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateAsync(long? id, Grade grade, CancellationToken cancellationToken)
        {
            try
            {
                if (grade == null) return BadRequest("StuGradedent data should be provided");
                if (id == null) return BadRequest("Grade id should be provided");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                return Ok(await _gradeService.UpdateGradeAsync(id, grade, cancellationToken));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Grade))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(long? id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == null) return BadRequest("Grade id should be provided");
                return Ok(await _gradeService.DeleteGradeByIdAsync(id, cancellationToken));
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