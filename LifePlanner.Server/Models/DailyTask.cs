using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class DailyTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DailyAgendaId { get; set; }
        public DailyAgenda DailyAgenda { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? SubtaskId { get; set; }
        public Subtask? Subtask { get; set; }

        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
