using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class TodosRepository : ITodosRepository {
        private readonly RestAppContext context;

        public TodosRepository(RestAppContext context) {
            this.context = context;
        }

        public bool SaveChanges() => context.SaveChanges() >= 0;

        public void CreateTodo(Todo todo) {
            if(string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentNullException(nameof(todo.Title));
            if(todo.User == null)
                throw new ArgumentNullException(nameof(todo.User));

            context.Todos.Add(todo);
        }

        public void DeleteTodo(Todo todo) {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            context.Todos.Remove(todo);
        }
    }
}