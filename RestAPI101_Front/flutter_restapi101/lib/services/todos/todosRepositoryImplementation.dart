import 'dart:convert';
import 'dart:io';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClient.dart';
import 'package:flutter_restapi101/services/todos/todosRepository.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;

class TodosRepositoryImplementation implements TodosRepository {
  final GetIt getIt = GetIt.instance;

  static const String contentType = 'application/json';

  @override
  Future<List<Todo>> getTodos() async {
    var client = await getIt.getAsync<AuthenticatedClient>();

    var request = http.Request('GET', APIURLs.getAllTodos());

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

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
  Future<void> createTodo(TodoWriteDTO todo) async {
    var client = await getIt.getAsync<AuthenticatedClient>();

    var request = http.Request('POST', APIURLs.postTodo());
    request.headers[HttpHeaders.contentTypeHeader] = contentType;
    request.body = json.encode(todo.toJson());

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    switch(response.statusCode) {
      case HttpStatus.created: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> updateTodo(int id, TodoWriteDTO todo) async {
    var client = await getIt.getAsync<AuthenticatedClient>();

    var request = http.Request('PUT', APIURLs.putTodo(id));
    request.headers[HttpHeaders.contentTypeHeader] = contentType;
    request.body = json.encode(todo.toJson());

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> changeDone(int id, bool done) async {
    var client = await getIt.getAsync<AuthenticatedClient>();

    var request = http.Request('PATCH', APIURLs.patchTodo(id));
    request.headers[HttpHeaders.contentTypeHeader] = contentType;
    request.body = json.encode([
      {
        'op': 'replace',
        'path': '/done',
        'value': done.toString()
      }
    ]);

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }
  
  @override
  Future<void> deleteTodo(int id) async {
    var client = await getIt.getAsync<AuthenticatedClient>();

    var request = http.Request('DELETE', APIURLs.deleteTodo(id));
    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    switch(response.statusCode) {
      case HttpStatus.ok: return;
      default: throw TodosUpdatingError(errorMessage: response.body);
    }
  }

}