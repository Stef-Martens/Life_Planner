using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class MonthlyGoal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? GoalId { get; set; }
        public Goal? Goal { get; set; }

        public int? SubtaskId { get; set; }
        public Subtask? Subtask { get; set; }

        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

    }
}
