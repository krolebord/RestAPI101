import 'package:flutter_restapi101/httpMethods.dart';
import 'package:http/http.dart';

abstract class _ApiUrls {
  static const String baseurl = "https://localhost:5001";
  static const String prefix = "api";

  // Auth
  static Uri login() => Uri.parse('$baseurl/$prefix/auth/login');
  static Uri register() => Uri.parse('$baseurl/$prefix/auth/register');

  // User
  static Uri getUser() => Uri.parse('$baseurl/$prefix/user');
  static Uri deleteUser() => Uri.parse('$baseurl/$prefix/user');
  static Uri changeName() => Uri.parse('$baseurl/$prefix/user/username');
  static Uri changePassword() => Uri.parse('$baseurl/$prefix/user/password');
  
  // Todos
  static Uri getAllTodos() => Uri.parse('$baseurl/$prefix/todos');
  static Uri getSpecifiedTodo(int id) => Uri.parse('$baseurl/$prefix/todos');
  static Uri postTodo() => Uri.parse('$baseurl/$prefix/todos');
  static Uri putTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
  static Uri patchTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
  static Uri reorderTodo(int id, int newOrder) => Uri.parse('$baseurl/$prefix/todos/reorder/$id:$newOrder');
  static Uri deleteTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
}

abstract class ApiRequests {
  // Auth
  static Request login() => 
    Request(HttpMethods.post, _ApiUrls.login());
  static Request register() => 
    Request(HttpMethods.post, _ApiUrls.register());

  // User
  static Request getUser() => 
    Request(HttpMethods.get, _ApiUrls.getUser());

  static Request deleteUser() => 
    Request(HttpMethods.delete, _ApiUrls.deleteUser());

  static Request changeUsername() => 
    Request(HttpMethods.put, _ApiUrls.changeName());

  static Request changePassword() => 
    Request(HttpMethods.post, _ApiUrls.changePassword());

  // Todos
  static Request getAllTodos() => 
    Request(HttpMethods.get, _ApiUrls.getAllTodos());

  static Request getSpecifiedTodo(int id) => 
    Request(HttpMethods.get, _ApiUrls.getSpecifiedTodo(id));

  static Request postTodo() => 
    Request(HttpMethods.post, _ApiUrls.postTodo());

  static Request putTodo(int id) => 
    Request(HttpMethods.put, _ApiUrls.putTodo(id));

  static Request patchTodo(int id) => 
    Request(HttpMethods.patch, _ApiUrls.patchTodo(id));

  static Request reorderTodo(int id, int newOrder) => 
    Request(HttpMethods.put, _ApiUrls.reorderTodo(id, newOrder));

  static Request deleteTodo(int id) => 
    Request(HttpMethods.delete, _ApiUrls.deleteTodo(id));
}
