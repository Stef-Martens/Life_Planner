using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifePlanner.Server.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public Type Type { get; set; }

        //[Required]
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        [Required]
        public int Year { get; set; }

        public decimal? Target { get; set; }
        public string? Unit { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsComplete { get; set; }

        public List<Subtask>? Subtasks { get; set; }

        public Goal()
        {
            if (Subtasks == null)
            {
                Subtasks = new List<Subtask>();
            }

        }
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
