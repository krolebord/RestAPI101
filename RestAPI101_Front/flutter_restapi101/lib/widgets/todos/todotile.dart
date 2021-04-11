import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/widgets/todos/todoDialog.dart';

class TodoTile extends StatefulWidget {
  final Todo todo;

  TodoTile({required this.todo, Key? key}) : super(key: key);

  @override
  _TodoTileState createState() => _TodoTileState();
}

class _TodoTileState extends State<TodoTile> {
  late bool done;

  @override
  void initState() {
    super.initState();
    done = widget.todo.done;
  }

  @override
  Widget build(BuildContext context) {
    var todo = widget.todo;
    return Card(
      elevation: 3,
      child: ListTile(
        leading: Checkbox(
          onChanged: _handleChangedDone,
          value: done,
        ),
        title: Text(todo.title),
        subtitle: todo.description.isNotEmpty ? Text(todo.description) : null,
        trailing: IconButton(
          icon: Icon(
            Icons.delete,
            color: Theme.of(context).errorColor.withOpacity(0.8),
          ),
          splashRadius: 22,
          onPressed: _handleDelete,
        ),
        onTap: () => _handleTap(context),
      )
    );
  }

  void _handleTap(BuildContext context) async {
    var result = await showDialog<TodoWriteDTO>(
      context: context, 
      builder: (context) => TodoDialog(todo: widget.todo),
    );

    if(result == null) return;

    context.read<TodosCubit>().updateTodo(widget.todo.id, result);
  }

  void _handleChangedDone(bool? value) {
    context.read<TodosCubit>().patchDone(widget.todo.id, !done);
    setState(() {
      done = !done;
    });
  }

  void _handleDelete() {
    context.read<TodosCubit>().deleteTodo(widget.todo.id);
  }
}