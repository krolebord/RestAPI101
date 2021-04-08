import 'package:flutter_restapi101/services/todos/todosRepository.dart';
import 'package:flutter_restapi101/services/todos/todosRepositoryImplementation.dart';
import 'package:get_it/get_it.dart';

extension TodosRepositoryRegister on GetIt {
  void registerTodosRepository() {
    this.registerLazySingleton<TodosRepository>(() => TodosRepositoryImplementation());
  }
}