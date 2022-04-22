using ELearning.Dto;
using ELearning.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {

        private readonly IStorageService _storageService;
        //private readonly ILogger<StorageController> _logger;

        public StorageController(IStorageService storageService)//, ILogger<StorageController> logger)
        {
            _storageService = storageService;
            //_logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFilesAsync([FromQuery(Name = "container")] string containerName, CancellationToken cancellationToken)
        {
            try
            {
                if (containerName == null || containerName == string.Empty) 
                    return BadRequest("Container name  should be provided");

                return Ok(await _storageService.GetAllFilesAsync(containerName, cancellationToken));
            }
            catch (Exception ex)
            {
                return Ok(Enumerable.Empty<string>());
            }

        }


        [HttpGet("{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(string fileName, [FromQuery(Name = "container")] string containerName, CancellationToken cancellationToken)
        {
            try
            {
                if (fileName == null || fileName == string.Empty) return BadRequest("File name should be provided");
                if (containerName == null || containerName == string.Empty) return BadRequest("Container name  should be provided");

                return Ok(await _storageService.GetFileAsync(fileName, containerName, cancellationToken));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UploadAsync([FromQuery(Name = "container")] string containerName, CancellationToken cancellationToken)
        {
            try
            {

                if (!HttpContext.Request.Form.Files.Any()) return BadRequest("You have to upload a file");
                if (containerName == null || containerName == string.Empty) return BadRequest("Container name  should be provided");

                var file = HttpContext.Request.Form.Files[0];
                var result = await _storageService.UploadFileAsync(file, containerName, cancellationToken);
                if(result) return Ok(result);
                else return StatusCode(StatusCodes.Status500InternalServerError, "Can't upload file");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        
        [HttpDelete("{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(string fileName, [FromQuery(Name = "container")] string containerName,  CancellationToken cancellationToken)
        {
            try
            {
                if (fileName == null || fileName == string.Empty) return BadRequest("File name should be provided");
                if (containerName == null || containerName == string.Empty) return BadRequest("Container name  should be provided");

                var result = await _storageService.DeleteFileAsync(fileName, containerName, cancellationToken);
                if (result) return Ok(result);
                else return StatusCode(StatusCodes.Status500InternalServerError, "Can't delete file");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
