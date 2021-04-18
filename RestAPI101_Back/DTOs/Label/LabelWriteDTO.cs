using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs {
    public class LabelWriteDTO {
        [Required, MaxLength(16)]
        public string Name { get; set; }
        public int? Color { get; set; }
    }
}