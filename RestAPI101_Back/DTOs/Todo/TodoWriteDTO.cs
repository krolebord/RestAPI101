using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RestAPI101_Back.DTOs {
    public class TodoWriteDTO {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}