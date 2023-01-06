using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Journal.API.ApiModels;
using StrengthJournal.Journal.API.Extensions;
using StrengthJournal.Journal.API.ServiceExceptions;
using StrengthJournal.Journal.API.Services;

namespace StrengthJournal.Journal.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ExerciseService exerciseService;

        public ExercisesController(ExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        [HttpGet("")]
        public async Task<ActionResult<ExerciseDto>> GetExercises(
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int perPage = 5,
            bool allRecords=false)
        {
            var userId = HttpContext.GetUserId();
            var exercise = await exerciseService.GetExercises(userId, pageNumber, perPage, search, allRecords);
            return Ok(exercise);
        }

        [HttpGet("{exerciseId:Guid}")]
        public async Task<ActionResult<ExerciseDto>> GetExercise([FromRoute] Guid exerciseId)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                var exercise = await exerciseService.GetExercise(userId, exerciseId);
                return Ok(exercise);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
        
        [HttpGet("{exerciseId:Guid}/history")]
        public async Task<ActionResult<IEnumerable<ExerciseHistoryDto>>> GetExerciseHistory(
            [FromRoute] Guid exerciseId, 
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int perPage = 5,
            [FromQuery] Guid? excludeWorkoutId = null)
        {
            var userId = HttpContext.GetUserId();
            try
            {
                var history = await exerciseService.GetExerciseHistory(userId, exerciseId, pageNumber, perPage, excludeWorkoutId);
                return Ok(history);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateExercise([FromBody]ExerciseCreationDto exercise)
        {
            var userId = HttpContext.GetUserId();
            await exerciseService.CreateExercise(exercise.Name, userId, exercise.ParentExerciseId);
            return Ok();
        }

        [HttpPut("{exerciseId:Guid}")]
        public async Task<ActionResult> UpdateExercise([FromRoute]Guid exerciseId, ExerciseCreationDto exercise)
        {
            var userId = HttpContext.GetUserId();
            try
            {
                await exerciseService.UpdateExercise(userId, exerciseId, exercise.Name, exercise.ParentExerciseId);
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{exerciseId:Guid}")]
        public async Task<ActionResult> DeleteExercise([FromRoute]Guid exerciseId)
        {
            var userId = HttpContext.GetUserId();
            await exerciseService.DeleteExercise(userId, exerciseId);
            return Ok();
        }
    }
}
