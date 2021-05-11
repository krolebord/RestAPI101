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
      buildDefaultDragHandles: false,
      padding: const EdgeInsets.only(bottom: 78),
      itemCount: todos.length ,
      onReorder: (oldIndex, newIndex) => _handleReorder(context, oldIndex, newIndex),
      itemBuilder: (context, index) {
        var todo = todos[index];
        return TodoTile(index: index, todo: todo, key: ValueKey(todo.id));
      }
    );
  }

  void _handleReorder(BuildContext context, int oldIndex, int newIndex) {
    var todo = todos.removeAt(oldIndex);
    context.read<TodosCubit>().reorderTodo(todo, newIndex);
    
    if(oldIndex < newIndex) newIndex--;
    todos.insert(newIndex, todo);
  }
}