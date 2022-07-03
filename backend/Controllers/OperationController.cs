using backend.DTOs.Requests;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IDbService _dbService;
        public OperationController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPut("operations/{idOperation}")]
        public async Task<IActionResult> PutExercise([FromBody] PutOperationRequest operation, [FromRoute] int idOperation)
        {
            System.Console.WriteLine("dupa");
            var response = await _dbService.PutOperationAsync(operation, idOperation);
            if (response == true)
                return Ok();
            else return BadRequest("Operation doesn't exist");
        }

        [HttpDelete("operations/{idOperation}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int idOperation)
        {
            var response = await _dbService.DeleteOperationAsync(idOperation);
            if (response == true)
                return Ok();
            else return BadRequest("Operation doesn't exist");
        }
    }
}
