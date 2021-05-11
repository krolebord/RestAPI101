import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/services/todos/todosRepository.dart';
import 'package:get_it/get_it.dart';
import 'package:meta/meta.dart';

part 'todos_state.dart';

class TodosCubit extends Cubit<TodosState> {
  final TodosRepository _repository;

  TodosCubit() :
    _repository = GetIt.instance.get<TodosRepository>(), 
    super(TodosLoading()) {
      fetchTodos();
    }

  Future<void> fetchTodos() async {
    emit(TodosLoading());

    try {
      var todos = await _repository.getTodos();
      emit(TodosLoaded(todos: todos));
    }
    on TodosLoadingError catch(e) {
      emit(TodosLoadingErrorState(message: e.errorMessage));
    }
  }

  void setFilters(List<Label> filters) =>
      _repository.setFilters(filters);

  void createTodo(TodoWriteDTO todo) => 
    _handleUpdateAction(_repository.createTodo(todo)); 

  void patchDone(Todo todo, bool done) =>
    _handleUpdateAction(_repository.patchDone(todo, done));

  void updateTodo(Todo todo, TodoWriteDTO newTodo) =>
    _handleUpdateAction(_repository.updateTodo(todo, newTodo));

  void reorderTodo(Todo todo, int newOrder) =>
    _handleUpdateAction(_repository.reorderTodo(todo, newOrder));

  void addLabel(Todo todo, Label label) =>
      _handleUpdateAction(_repository.addLabel(todo, label));

  void removeLabel(Todo todo, Label label) =>
      _handleUpdateAction(_repository.removeLabel(todo, label));

  void deleteTodo(Todo todo) =>
    _handleUpdateAction(_repository.deleteTodo(todo));

  void _handleUpdateAction(Future<void> action) async {
    emit(TodosLoading());

    try {
      await action;
    }
    on TodosUpdatingError catch(e) {
      emit(TodosUpdatingErrorState(message: e.errorMessage));
    }

    await fetchTodos();
  }
}
