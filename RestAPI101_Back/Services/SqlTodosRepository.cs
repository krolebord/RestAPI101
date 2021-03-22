using System;
using System.Collections.Generic;
using System.Linq;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class SqlTodosRepository : ITodosRepository {
        private readonly RestAppContext context;

        public SqlTodosRepository(RestAppContext context) {
            this.context = context;
        }

        public bool SaveChanges() => context.SaveChanges() >= 0;

        public IEnumerable<Todo> GetAllTodos() {
            return context.Todos.ToList();
        }

        public Todo GetTodoById(int id) {
            return context.Todos.Find(id);
        }

        public void CreateTodo(Todo todo) {
            if(string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentNullException(nameof(todo.Title));

            context.Todos.Add(todo);
        }

        public void DeleteTodo(Todo todo) {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            context.Todos.Remove(todo);
        }
    }
}