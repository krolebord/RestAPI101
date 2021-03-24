using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs {
    public class UserReadDTO {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime TokenExpires { get; set; }
    }
}