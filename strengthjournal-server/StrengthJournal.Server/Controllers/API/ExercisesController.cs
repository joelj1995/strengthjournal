using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
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
        public async Task<ActionResult<ExerciseDto>> GetExercises()
        {
            var exercise = await exerciseService.GetExercises();
            return Ok(exercise);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateExercise([FromBody]ExerciseCreationDto exercise)
        {
            var userId = HttpContext.GetUserId();
            await exerciseService.CreateExercise(exercise.Name, userId);
            return Ok();
        }
    }
}
