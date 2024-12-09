using System;
using System.ComponentModel.DataAnnotations;

namespace LifePlanner.Server.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public List<Goal>? Goals { get; set; }
    }
}
