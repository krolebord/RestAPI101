using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI101_Back.Models {
    [Table("Users")]
    public class User {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(6), MaxLength(32)]
        public string Login { get; set; }
        
        [Required, MinLength(8), MaxLength(32)]
        public string Password { get; set; }

        public List<Todo> Todos { get; set; } = new ();
    }
}