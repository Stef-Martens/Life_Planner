using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Auth0Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Goal>? Goals { get; set; }
        public List<MonthlyGoal> MonthlyGoals { get; set; }
        public List<DailyAgenda> DailyAgendas { get; set; }
        public List<DailyTask> DailyTasks { get; set; }

    }
}
