using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs {
    public class AuthTokenReadDTO {
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Expires { get; set; }
    }
}