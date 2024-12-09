using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class Subtask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GoalId { get; set; }
        public Goal Goal { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}
