using System.Collections.Generic;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface ITodosRepository {
        bool SaveChanges();
        
        public IEnumerable<Todo> GetAllTodos();
        public Todo GetTodoById(long id);

        void CreateTodo(Todo todo);

        void DeleteTodo(Todo todo);
    }
}