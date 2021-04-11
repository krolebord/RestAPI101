class APIURLs {
  static const String baseurl = "https://localhost:5001";
  static const String prefix = "api";

  // Auth
  static Uri login() => Uri.parse('$baseurl/$prefix/auth/login');
  static Uri registerUser() => Uri.parse('$baseurl/$prefix/auth/register');

  // User
  static Uri getUser() => Uri.parse('$baseurl/$prefix/user');
  static Uri deleteUser() => Uri.parse('$baseurl/$prefix/user');
  static Uri changeName() => Uri.parse('$baseurl/$prefix/user');
  
  // Todos
  static Uri getAllTodos() => Uri.parse('$baseurl/$prefix/todos');
  static Uri getSpecifiedTodo(int id) => Uri.parse('$baseurl/$prefix/todos');
  static Uri postTodo() => Uri.parse('$baseurl/$prefix/todos');
  static Uri putTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
  static Uri patchTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
  static Uri reorderTodo(int id, int newOrder) => Uri.parse('$baseurl/$prefix/todos/reorder/$id:$newOrder');
  static Uri deleteTodo(int id) => Uri.parse('$baseurl/$prefix/todos/$id');
}
