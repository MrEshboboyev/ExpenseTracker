using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
