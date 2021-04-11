import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/widgets/todos/todotile.dart';

class TodoList extends StatelessWidget {
  final List<Todo> todos;

  TodoList({required this.todos});

  @override
  Widget build(BuildContext context) {
    return ReorderableListView.builder(
      itemCount: todos.length + 1,
      onReorder: (oldIndex, newIndex) => _handleReorder(context, oldIndex, newIndex),
      itemBuilder: (context, index) {
        if(index >= todos.length)
          return SizedBox(height: 78, key: ValueKey(todos.length));
        var todo = todos[index];
        return TodoTile(todo: todo, key: ValueKey(todo.id));
      }
    );
  }

  void _handleReorder(BuildContext context, int oldIndex, int newIndex) {
    var todo = todos.removeAt(oldIndex);
    context.read<TodosCubit>().reorderTodo(todo.id, newIndex);
    
    if(oldIndex < newIndex) newIndex--;
    todos.insert(newIndex, todo);
  }
}