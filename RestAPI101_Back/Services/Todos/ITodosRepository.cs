using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface ITodosRepository : IRepository {
        public void CreateTodo(Todo todo);

        public void DeleteTodo(Todo todo);

        public int OrderDistance { get; }
        public void NormalizeOrderForUser(User user);
    }
}