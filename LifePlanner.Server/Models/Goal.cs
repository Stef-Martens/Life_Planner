using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public Type Type { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public decimal? Target { get; set; }
        public string? Unit { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsComplete { get; set; }

        public List<Subtask> Subtasks { get; set; }
    }

    public enum Type
    {
        OneTime,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
