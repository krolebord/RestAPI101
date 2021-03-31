﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.Models {
    public class AuthToken {
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Expires { get; set; }
    }
}