using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.Models {
    public class Label {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(16)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public int? Color { get; set; }
        
        [Required]
        public User User { get; set; }

        public List<Todo> Todos { get; set; } = new();
    }
}