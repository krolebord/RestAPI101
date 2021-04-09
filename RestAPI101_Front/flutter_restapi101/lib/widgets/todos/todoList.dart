import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/widgets/todos/todotile.dart';

class TodoList extends StatelessWidget {
  final List<Todo> todos;

  TodoList({required this.todos});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        var todo = todos[index];
        return TodoTile(todo: todo, key: ValueKey(todo.id));
      }
    );
  }
}