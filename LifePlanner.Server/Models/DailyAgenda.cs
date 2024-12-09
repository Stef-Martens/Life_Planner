using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class DailyAgenda
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<DailyTask> DailyTasks { get; set; }
    }
}
