using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs {
    public class TodoCreateDTO {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}