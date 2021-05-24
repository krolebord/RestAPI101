import 'package:flutter_restapi101/httpMethods.dart';
import 'package:http/http.dart';

class ApiUrls {
  static const bool useHttps = false;
  static const String apiPath = "api";

  ApiUrls._();

  static String get authority {
    // if(Platform.isAndroid)
    //   return "10.0.2.2:5001";

    return "localhost:5001";
  }

  static Uri constructUri(String authority, String path, [Map<String, dynamic>? params]) =>
    useHttps ? Uri.http(authority, path, params) : Uri.https(authority, path, params);

  // Auth
  static Uri login() => constructUri(authority, '$apiPath/auth/login');

  static Uri register() => constructUri(authority, '$apiPath/auth/register');

  // User
  static Uri getUser() => constructUri(authority, '$apiPath/user');

  static Uri deleteUser() => constructUri(authority, '$apiPath/user');

  static Uri changeName() => constructUri(authority, '$apiPath/user/username');

  static Uri changePassword() => constructUri(authority, '$apiPath/user/password');
  
  // Labels
  static Uri getAllLabels() => constructUri(authority, '$apiPath/labels');

  static Uri getSpecifiedLabel(int id) => constructUri(authority, '$apiPath/labels/$id');

  static Uri createLabel() => constructUri(authority, '$apiPath/labels');

  static Uri updateLabel(int id) => constructUri(authority, '$apiPath/labels/$id');

  static Uri deleteLabel(int id) => constructUri(authority, '$apiPath/labels/$id');

  // Todos
  static Uri getAllTodos({Map<String, dynamic>? params}) => constructUri(authority, '$apiPath/todos', params);

  static Uri getSpecifiedTodo(int id) => constructUri(authority, '$apiPath/todos');

  static Uri postTodo() => constructUri(authority, '$apiPath/todos');

  static Uri putTodo(int id) => constructUri(authority, '$apiPath/todos/$id');

  static Uri patchTodo(int id) => constructUri(authority, '$apiPath/todos/$id');

  static Uri reorderTodo(int id, int newOrder) => constructUri(authority, '$apiPath/todos/reorder/$id:$newOrder');

  static Uri addLabelToTodo(int todoId, int labelId) => constructUri(authority, '$apiPath/todos/label/$todoId:$labelId');

  static Uri removeLabelFromTodo(int todoId, int labelId) => constructUri(authority, '$apiPath/todos/label/$todoId:$labelId');

  static Uri deleteTodo(int id) => constructUri(authority, '$apiPath/todos/$id');
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
  static Request getAllTodos({Map<String, dynamic>? params}) =>
    Request(HttpMethods.get, ApiUrls.getAllTodos(params: params));

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
