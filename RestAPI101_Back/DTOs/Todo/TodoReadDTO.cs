namespace RestAPI101_Back.DTOs {
    public class TodoReadDTO {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}