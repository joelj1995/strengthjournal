﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.ServiceExceptions;
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
        public async Task<ActionResult<ExerciseDto>> GetExercises([FromQuery] int pageNumber = 1, [FromQuery] int perPage = 5, bool allRecords=false)
        {
            var userId = HttpContext.GetUserId();
            var exercise = await exerciseService.GetExercises(userId, pageNumber, perPage, allRecords);
            return Ok(exercise);
        }
        
        [HttpGet("{exerciseId:Guid}/history")]
        public async Task<ActionResult<IEnumerable<ExerciseHistoryDto>>> GetExerciseHistory([FromRoute] Guid exerciseId)
        {
            var userId = HttpContext.GetUserId();
            try
            {
                var history = await exerciseService.GetExerciseHistory(userId, exerciseId, 10);
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
            await exerciseService.CreateExercise(exercise.Name, userId);
            return Ok();
        }

        [HttpPut("{exerciseId:Guid}")]
        public async Task<ActionResult> UpdateExercise([FromRoute]Guid exerciseId, ExerciseUpdateDto exercise)
        {
            var userId = HttpContext.GetUserId();
            try
            {
                await exerciseService.UpdateExercise(userId, exerciseId, exercise.Name);
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
