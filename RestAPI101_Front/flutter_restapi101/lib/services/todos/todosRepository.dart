import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';

abstract class TodosRepository {
  Future<List<Todo>> getTodos();
  Future<Todo> getTodo(int id);

  Future<void> createTodo(TodoWriteDTO todo);

  Future<void> updateTodo(int id, TodoWriteDTO todo);

  Future<void> changeDone(int id, bool done);
  
  Future<void> deleteTodo(int id);
}

abstract class TodosError {
  final String errorMessage;

  TodosError({required this.errorMessage});
}

class TodosLoadingError extends TodosError {
  TodosLoadingError({required String errorMessage}) : super(errorMessage: errorMessage);
}

class TodosUpdatingError extends TodosError {
  TodosUpdatingError({required String errorMessage}) : super(errorMessage: errorMessage);
}