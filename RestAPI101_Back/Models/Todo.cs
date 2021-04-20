using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI101_Back.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public int Order { get; set; }

        [Required]
        public bool Done { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public HashSet<Label> Labels { get; set; } = new();
    }
}
