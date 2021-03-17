namespace RestAPI101_Back.DTOs {
    public class TodoReadDTO {
        public long Id { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}