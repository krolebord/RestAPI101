using System.Collections.Generic;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface ITodosRepository {
        public bool SaveChanges();
        
        public IEnumerable<Todo> GetAllTodos();
        public Todo GetTodoById(int id);

        public void CreateTodo(Todo todo);

        public void DeleteTodo(Todo todo);
    }
}