import 'dart:convert';
import 'dart:io';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todoIncludeMode.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/services/authenticatedServiceMixin.dart';
import 'package:flutter_restapi101/services/todos/todosRepository.dart';
import 'package:flutter_restapi101/models/todo/todoFilterDTO.dart';
import 'package:get_it/get_it.dart';

class TodosRepositoryImplementation with AuthenticatedServiceMixin implements TodosRepository {
  final GetIt getIt = GetIt.instance;

  @override
  Future<List<Todo>> getTodos({TodoIncludeMode includeMode = TodoIncludeMode.All, List<Label>? filterLabels}) async {
    var filter = TodoFilterDTO(
        includeMode: includeMode,
        labelIds: filterLabels
    );

    var request = ApiRequests.getAllTodos(params: filter.toJson());

    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: {
        List<dynamic> jsonTodos = json.decode(response.body);
        return jsonTodos.map((jsonTodo) => Todo.fromJson(jsonTodo)).toList();
      }
      case HttpStatus.noContent: return [];
      default: throw TodosLoadingError(errorMessage: response.body);
    }
  }

  @override
  Future<Todo> getTodo(int id) async {
    var request = ApiRequests.getSpecifiedTodo(id);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: {
        Todo todo = Todo.fromJson(json.decode(response.body));
        return todo;
      }
      default: throw TodosLoadingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> createTodo(TodoWriteDTO todo) async {
    var request = ApiRequests.postTodo();
    request.body = json.encode(todo.toJson());

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.created: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> updateTodo(Todo todo, TodoWriteDTO newTodo) async {
    var request = ApiRequests.putTodo(todo.id);
    request.body = json.encode(newTodo.toJson());

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> patchDone(Todo todo, bool done) async {
    var request = ApiRequests.patchTodo(todo.id);
    request.body = json.encode([
      {
        'op': 'replace',
        'path': '/done',
        'value': done.toString()
      }
    ]);

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> reorderTodo(Todo todo, int newOrder) async {
    var request = ApiRequests.reorderTodo(todo.id, newOrder);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      case HttpStatus.notFound: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> addLabel(Todo todo, Label label) async {
    var request = ApiRequests.addLabelToTodo(todo.id, label.id);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      case HttpStatus.notFound: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> removeLabel(Todo todo, Label label) async {
    var request = ApiRequests.removeLabelFromTodo(todo.id, label.id);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      case HttpStatus.notFound: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }
  
  @override
  Future<void> deleteTodo(Todo todo) async {
    var request = ApiRequests.deleteTodo(todo.id);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }
}