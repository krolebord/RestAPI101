using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface ITodosRepository {
        public bool SaveChanges();

        public void CreateTodo(Todo todo);

        public void DeleteTodo(Todo todo);
    }
}