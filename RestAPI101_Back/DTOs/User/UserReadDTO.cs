using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs {
    // TODO Add changeable username
    
    public class UserReadDTO {
        [Required]
        public string Login { get; set; }
    }
}