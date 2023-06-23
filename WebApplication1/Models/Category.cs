using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatAt { get; set; }
        public string Detail { get; set; }

    }
}
