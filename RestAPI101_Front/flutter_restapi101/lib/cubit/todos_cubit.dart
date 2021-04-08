import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
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
      updateTodos();
    }

  Future<void> updateTodos() async {
    emit(TodosLoading());

    try {
      var todos = await _repository.getTodos();
      emit(TodosLoaded(todos: todos));
    }
    on TodosLoadingError catch(e) {
      emit(TodosLoadingErrorState(message: e.errorMessage));
    }
  }

  void createTodo(TodoWriteDTO todo) => 
    _handleUpdateAction(_repository.createTodo(todo)); 

  void changeDone(int id, bool done) =>
    _handleUpdateAction(_repository.changeDone(id, done));

  void updateTodo(int id, TodoWriteDTO todo) =>
    _handleUpdateAction(_repository.updateTodo(id, todo));
  
  void deleteTodo(int id) =>
    _handleUpdateAction(_repository.deleteTodo(id));

  void _handleUpdateAction(Future<void> action) async {
    emit(TodosLoading());

    try {
      await action;
    }
    on TodosUpdatingError catch(e) {
      emit(TodosUpdatingErrorState(message: e.errorMessage));
    }

    await updateTodos();
  }
}
