using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetExercises()
        {
            var userId = HttpContext.GetUserId();
            var exercise = await exerciseService.GetExercises();
            return Ok(exercise);
        }
    }
}
