import 'package:flutter_restapi101/httpMethods.dart';
import 'package:http/http.dart';

class ApiUrls {
  static const String baseurl = "https://localhost:5001";
  static const String prefix = "api";

  ApiUrls._();

  // Auth
  static Uri login() => Uri.parse('$baseurl/$prefix/auth/login');

  static Uri register() => Uri.parse('$baseurl/$prefix/auth/register');

  // User
  static Uri getUser() => Uri.parse('$baseurl/$prefix/user');

  static Uri deleteUser() => Uri.parse('$baseurl/$prefix/user');

  static Uri changeName() => Uri.parse('$baseurl/$prefix/user/username');

  static Uri changePassword() => Uri.parse('$baseurl/$prefix/user/password');
  
  // Labels
  static Uri getAllLabels() => Uri.parse('$baseurl/$prefix/labels');

  static Uri getSpecifiedLabel(int id) => Uri.parse('$baseurl/$prefix/labels/$id');

  static Uri createLabel() => Uri.parse('$baseurl/$prefix/labels');

  static Uri updateLabel(int id) => Uri.parse('$baseurl/$prefix/labels/$id');

  static Uri deleteLabel(int id) => Uri.parse('$baseurl/$prefix/labels/$id');

  // Todos
  static Uri getAllTodos() => Uri.parse('$baseurl/$prefix/todos');

  static Uri getSpecifiedTodo(int id) => Uri.parse('$baseurl/$prefix/todos');

  static Uri postTodo() => Uri.parse('$baseurl/$prefix/todos');

  static Uri putTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');

  static Uri patchTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');

  static Uri reorderTodo(int id, int newOrder) => Uri.parse('$baseurl/$prefix/todos/reorder/$id:$newOrder');

  static Uri addLabelToTodo(int todoId, int labelId) => Uri.parse('$baseurl/$prefix/todos/label/$todoId:$labelId');

  static Uri removeLabelFromTodo(int todoId, int labelId) => Uri.parse('$baseurl/$prefix/todos/label/$todoId:$labelId');

  static Uri deleteTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
}

class ApiRequests {
  ApiRequests._();

  // Auth
  static Request login() => 
    Request(HttpMethods.post, ApiUrls.login());
  static Request register() => 
    Request(HttpMethods.post, ApiUrls.register());

  // User
  static Request getUser() => 
    Request(HttpMethods.get, ApiUrls.getUser());

  static Request deleteUser() => 
    Request(HttpMethods.delete, ApiUrls.deleteUser());

  static Request changeUsername() => 
    Request(HttpMethods.put, ApiUrls.changeName());

  static Request changePassword() => 
    Request(HttpMethods.post, ApiUrls.changePassword());

  // Labels
  static Request getSpecifiedLabel(int id) =>
    Request(HttpMethods.get, ApiUrls.getSpecifiedLabel(id));

  static Request getAllLabels() =>
    Request(HttpMethods.get, ApiUrls.getAllLabels());

  static Request createLabel() =>
    Request(HttpMethods.post, ApiUrls.createLabel());
    
  static Request updateLabel(int id) =>
    Request(HttpMethods.put, ApiUrls.updateLabel(id));

  static Request deleteLabel(int id) =>
    Request(HttpMethods.delete, ApiUrls.deleteLabel(id));

  // Todos
  static Request getAllTodos() => 
    Request(HttpMethods.get, ApiUrls.getAllTodos());

  static Request getSpecifiedTodo(int id) => 
    Request(HttpMethods.get, ApiUrls.getSpecifiedTodo(id));

  static Request postTodo() => 
    Request(HttpMethods.post, ApiUrls.postTodo());

  static Request putTodo(int id) => 
    Request(HttpMethods.put, ApiUrls.putTodo(id));

  static Request patchTodo(int id) => 
    Request(HttpMethods.patch, ApiUrls.patchTodo(id));

  static Request reorderTodo(int id, int newOrder) => 
    Request(HttpMethods.put, ApiUrls.reorderTodo(id, newOrder));

  static Request addLabelToTodo(int todoId, int labelId) =>
    Request(HttpMethods.put, ApiUrls.addLabelToTodo(todoId, labelId));

  static Request removeLabelFromTodo(int todoId, int labelId) =>
      Request(HttpMethods.delete, ApiUrls.removeLabelFromTodo(todoId, labelId));

  static Request deleteTodo(int id) => 
    Request(HttpMethods.delete, ApiUrls.deleteTodo(id));
}
