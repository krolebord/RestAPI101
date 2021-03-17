using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.Models {
    public class Todo {
        [Key]
        public long Id { get; set; }
        [Required]
        public bool Done { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}