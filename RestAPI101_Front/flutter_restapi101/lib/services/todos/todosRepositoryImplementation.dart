import 'dart:convert';
import 'dart:io';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/services/authenticatedServiceMixin.dart';
import 'package:flutter_restapi101/services/todos/todosRepository.dart';
import 'package:get_it/get_it.dart';

class TodosRepositoryImplementation with AuthenticatedServiceMixin implements TodosRepository {
  final GetIt getIt = GetIt.instance;

  @override
  Future<List<Todo>> getTodos() async {
    var response = await sendRequest(ApiRequests.getAllTodos());

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
  Future<void> updateTodo(int id, TodoWriteDTO todo) async {
    var request = ApiRequests.putTodo(id);
    request.body = json.encode(todo.toJson());

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> patchDone(int id, bool done) async {
    var request = ApiRequests.patchTodo(id);
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
  Future<void> reorderTodo(int id, int newOrder) async {
    var request = ApiRequests.reorderTodo(id, newOrder);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      case HttpStatus.notFound: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }
  
  @override
  Future<void> deleteTodo(int id) async {
    var request = ApiRequests.deleteTodo(id);
    var response = await sendRequest(request);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }
}