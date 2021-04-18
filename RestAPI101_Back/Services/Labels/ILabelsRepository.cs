using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface ILabelsRepository : IRepository {
        public void CreateLabel(Label label);
        public void DeleteLabel(Label label);
    }
}