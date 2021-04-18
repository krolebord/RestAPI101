import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/widgets/todos/todoDialog.dart';

class TodosFAB extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return FloatingActionButton(
      child: const Icon(Icons.add),
      onPressed: () => _handlePressed(context)
    );
  }

  void _handlePressed(BuildContext context) async {
    var result = await showDialog<TodoWriteDTO>(
      context: context, 
      builder: (context) => TodoDialog(),
    );

    if(result == null) return;

    context.read<TodosCubit>().createTodo(result);
  }
}