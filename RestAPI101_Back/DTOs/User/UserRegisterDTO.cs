using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RestAPI101_Back.DTOs {
    public class UserRegisterDTO {
        [Required(ErrorMessage = "Login must be specified")]
        [MinLength(6, ErrorMessage = "Login must be at least 6 characters long")]
        [MaxLength(64, ErrorMessage = "Login must be maximum 64 characters long")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Password must be specified")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(32, ErrorMessage = "Password must be maximum 32 characters long")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Username must be specified")]
        public string Username { get; set; }
    }
}