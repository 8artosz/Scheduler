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
    public class ExerciseController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ExerciseController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("exercises")]
        public async Task<IActionResult> GetExercises()
        {
            return Ok(await _dbService.GetExercisesAsync());
        }

        [HttpPost("exercise")]
        public async Task<IActionResult> PostExercise([FromBody] PostExerciseRequest exercise)
        {
            var response = await _dbService.PostExerciseAsync(exercise);
            if (response == true)
                return Ok();
            else return BadRequest("Exercise already exists");
        }

        [HttpPut("exercise/{idExercise}")]
        public async Task<IActionResult> PutExercise([FromBody] PostExerciseRequest exercise, [FromRoute] int idExercise)
        {
            var response = await _dbService.PutExerciseAsync(exercise, idExercise);
            if (response == true)
                return Ok();
            else return BadRequest("Exercise doesn't exist");
    }

        [HttpDelete("exercise/{idExercise}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int idExercise)
        {
            var response = await _dbService.DeleteExerciseAsync(idExercise);
            if (response == true)
                return Ok();
            else return BadRequest("Exercise doesn't exist");
        }
        [HttpGet("exercise/{idExercise}/operations")]
        public async Task<IActionResult> GetExerciseOperationsAsync([FromRoute] int idExercise)
        {
            return Ok(await _dbService.GetExerciseOperationsAsync(idExercise));
        }

        [HttpPost("exercise/{idExercise}/operations")]
        public async Task<IActionResult> PostExerciseOperationsAsync([FromBody] PostExerciseOperationsRequest operation, [FromRoute] int idExercise)
        {
            var response = await _dbService.PostExerciseOperationsAsync(operation,idExercise);
            if (response == true)
                return Ok();
            else return BadRequest("Exercise doesn't exists");
        }
    }
}
