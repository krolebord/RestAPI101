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
    return RefreshIndicator(
      onRefresh: () => context.read<TodosCubit>().updateTodos(),
      child: ListView.builder(
        itemCount: todos.length,
        itemBuilder: (context, index) => TodoTile(todo: todos[index]),
      ),
    );
  }
}