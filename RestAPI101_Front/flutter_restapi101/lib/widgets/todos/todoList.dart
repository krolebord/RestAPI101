import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/widgets/todos/todotile.dart';

class TodoList extends StatefulWidget {
  final List<Todo> todos;

  TodoList({required this.todos});

  @override
  _TodoListState createState() => _TodoListState();
}

class _TodoListState extends State<TodoList> {
  late final ScrollController _scrollController;

  @override
  void initState() {
    super.initState();
    _scrollController = new ScrollController();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return ReorderableListView.builder(
      scrollController: _scrollController,
      key: PageStorageKey<String>("TodosPage"),
      buildDefaultDragHandles: false,
      padding: const EdgeInsets.only(bottom: 78),
      itemCount: widget.todos.length ,
      onReorder: (oldIndex, newIndex) => _handleReorder(context, oldIndex, newIndex),
      itemBuilder: (context, index) {
        var todo = widget.todos[index];
        return TodoTile(index: index, todo: todo, key: ValueKey(todo.id));
      }
    );
  }

  void _handleReorder(BuildContext context, int oldIndex, int newIndex) {
    var todo = widget.todos.removeAt(oldIndex);
    context.read<TodosCubit>().reorderTodo(todo, newIndex);

    if(oldIndex < newIndex) newIndex--;
    widget.todos.insert(newIndex, todo);
  }
}