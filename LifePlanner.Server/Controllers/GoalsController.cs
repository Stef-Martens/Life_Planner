using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LifePlanner.Server.Models;
using LifePlanner.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LifePlanner.Server.Controllers
{
    [Route("api/users/{userId}/goals")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;
        private readonly IUserService _userService;

        public GoalsController(IGoalService goalService, IUserService userService)
        {
            _goalService = goalService;
            _userService = userService;
        }


        // GET: api/users/{userId}/goals
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goal>>> GetGoalsByUserId(int userId)
        {
            var user = HttpContext.User;

            var goals = await _goalService.GetGoalsByUserId(userId);
            if (!goals.Any())
            {
                return NotFound("No goals found for the specified user.");
            }

            // add the check to the return
            return Ok(goals);
        }

        // GET: api/users/{userId}/goals/check
        [HttpGet("check")]
        public async Task<ActionResult<object>> CheckIfNewGoalsAreNeeded(int userId)
        {
            var goals = await _goalService.GetGoalsByUserId(userId);

            return CheckIfNewGoalsAreNeeded(goals);
        }

        private object CheckIfNewGoalsAreNeeded(IEnumerable<Goal> goals)
        {
            var currentYear = DateTime.Now.Year;
            var nextYear = currentYear + 1;
            var currentMonth = DateTime.Now.Month;

            // Check if there are any goals for the current year
            var enumerable = goals as Goal[] ?? goals.ToArray();
            var hasCurrentYearGoals = enumerable.Any(goal => goal.Year == currentYear);
            if (!hasCurrentYearGoals)
            {
                return new { newGoalsNeeded = true, year = currentYear };
            }

            // Check if it's December and there are no goals for the next year
            var isDecember = (currentMonth == 12);
            var hasNextYearGoals = enumerable.Any(goal => goal.Year == nextYear);
            if (isDecember && !hasNextYearGoals)
            {
                return new { newGoalsNeeded = true, year = nextYear };
            }

            return new { newGoalsNeeded = false };
        }


        // GET: api/users/{userId}/goals/{goalId}
        [HttpGet("{goalId}")]
        public async Task<ActionResult<Goal>> GetGoalById(int userId, int goalId)
        {
            var goal = await _goalService.GetById(goalId);

            if (goal == null || goal.UserId != userId)
            {
                return NotFound("Goal not found or does not belong to the user.");
            }

            return Ok(goal);
        }

        // POST: api/users/{userId}/goals
        [HttpPost]
        public async Task<ActionResult<Goal>> PostGoal(int userId, [FromBody] Goal goal)
        {
            if (goal.UserId != userId)
            {
                return BadRequest("Mismatch between route parameter and goal data.");
            }

            goal.User = await _userService.GetById(userId);

            var createdGoal = await _goalService.Add(goal);

            return CreatedAtAction(
                nameof(GetGoalById),
                new { userId, goalId = createdGoal.Id },
                createdGoal
            );
        }

        // PUT: api/users/{userId}/goals/{goalId}
        [HttpPut("{goalId}")]
        public async Task<IActionResult> PutGoal(int userId, int goalId, [FromBody] Goal goal)
        {
            if (goal.Id != goalId || goal.UserId != userId)
            {
                return BadRequest("Mismatch between route parameters and goal data.");
            }

            try
            {
                await _goalService.Update(goal);
            }
            catch (Exception)
            {
                if (!GoalExists(goalId))
                {
                    return NotFound("Goal not found.");
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/users/{userId}/goals/{goalId}
        [HttpDelete("{goalId}")]
        public async Task<IActionResult> DeleteGoal(int userId, int goalId)
        {
            var goal = await _goalService.GetById(goalId);

            if (goal == null || goal.UserId != userId)
            {
                return NotFound("Goal not found or does not belong to the user.");
            }

            await _goalService.Delete(goal.Id);

            return NoContent();
        }


        private bool GoalExists(int goalId)
        {
            return _goalService.GetById(goalId) != null;
        }
    }
}
