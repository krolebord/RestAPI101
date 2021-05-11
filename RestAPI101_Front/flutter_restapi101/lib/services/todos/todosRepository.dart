import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';

abstract class TodosRepository {
  Future<List<Todo>> getTodos();

  Future<Todo> getTodo(int id);

  void setFilters(List<Label> filters);

  Future<void> createTodo(TodoWriteDTO todo);

  Future<void> updateTodo(Todo todo, TodoWriteDTO newTodo);

  Future<void> patchDone(Todo todo, bool done);

  Future<void> reorderTodo(Todo todo, int newOrder);

  Future<void> addLabel(Todo todo, Label label);

  Future<void> removeLabel(Todo todo, Label label);
  
  Future<void> deleteTodo(Todo todo);
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