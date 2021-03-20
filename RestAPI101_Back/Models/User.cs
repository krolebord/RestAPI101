using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.Models {
    public class User {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        [Required, MinLength(6), MaxLength(32)]
        public string Login { get; set; }
        [Required, MinLength(8), MaxLength(32)]
        public string Password { get; set; }
    }
}