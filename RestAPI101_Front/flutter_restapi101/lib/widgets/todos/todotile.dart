import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';
import 'package:flutter_restapi101/services/colorGenerator/ColorGenerator.dart';
import 'package:flutter_restapi101/widgets/labels/labelChip.dart';
import 'package:flutter_restapi101/widgets/todos/addLabelButton.dart';
import 'package:flutter_restapi101/widgets/todos/todoDialog.dart';

class TodoTile extends StatefulWidget {
  final int index;
  final Todo todo;

  TodoTile({required this.index, required this.todo, Key? key}) : super(key: key);

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

    Widget item = Card(
      child: InkWell(
        onTap: _handleTap,
        child: Padding(
          padding: const EdgeInsets.symmetric(vertical: 6),
          child: Row(
            mainAxisSize: MainAxisSize.max,
            children: [
              SizedBox(
                width: 54,
                child: Align(
                  alignment: Alignment.centerRight,
                  child: Padding(
                    padding: const EdgeInsets.only(right: 4),
                    child: _buildLeading(),
                  ),
                ),
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    _buildTitle(),

                    if(todo.description.isNotEmpty)
                      Text(todo.description, style: TextStyle(color: Colors.white70))
                  ],
                ),
              ),
              SizedBox(
                width: 54,
                child: Center(
                  child: _buildTrailing()
                ),
              )
            ],
          ),
        ),
      ),
    );

    return Stack(
      children: [
        item,
        Positioned.directional(
          textDirection: Directionality.of(context),
          start: 3,
          top: 0,
          bottom: 0,
          child: Align(
            alignment: AlignmentDirectional.centerStart,
            child: ReorderableDragStartListener(
              index: widget.index,
              child: Icon(
                Icons.drag_indicator,
                color: Theme.of(context).iconTheme.color?.withOpacity(0.6),
              ),
            ),
          ),
        ),
      ],
    );
  }

  Widget _buildLeading() {
    return Checkbox(
      onChanged: _handleChangedDone,
      value: done,
    );
  }

  Widget _buildTitle() {
    var todo = widget.todo;
    var labels = <Label>[];

    var labelsState = context.read<LabelsCubit>().state;
    if(labelsState is LabelsLoaded)
      labels = labelsState.labels;

    List<Widget> labelsWidgets = [];

    if(todo.labelIds.isNotEmpty && labels.isNotEmpty)
      labelsWidgets =
          todo.labelIds
            .where((id) => labels.any((label) => label.id == id))
            .map<Widget>((labelId) {
              var label = labels.firstWhere((label) => label.id == labelId);

              return Transform.scale(
                scale: 0.9,
                child: LabelChip(
                  label: label,
                  deleteIcon: Icon(Icons.close, color: ColorUtils.textColorOn(label.color)),
                  onDeleted: () =>
                      context.read<TodosCubit>().removeLabel(todo, label),
                )
              );
            }).toList(growable: false);

    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Wrap(
          alignment: WrapAlignment.start,
          crossAxisAlignment: WrapCrossAlignment.start,
          children: [
            ...labelsWidgets,

            Transform.scale(
              scale: 0.85,
              child: AddLabelButton(todo: todo)
            )
          ]
        ),
        Text(todo.title),
      ],
    );
  }

  Widget _buildTrailing() {
    return IconButton(
      icon: Icon(
        Icons.delete,
        color: Theme.of(context).errorColor.withOpacity(0.8),
      ),
      splashRadius: 22,
      onPressed: _handleDelete,
    );
  }

  void _handleTap() async {
    var result = await showDialog<TodoWriteDTO>(
      context: context, 
      builder: (context) => TodoDialog(todo: widget.todo),
    );

    if(result == null) return;

    context.read<TodosCubit>().updateTodo(widget.todo, result);
  }

  void _handleChangedDone(bool? value) {
    context.read<TodosCubit>().patchDone(widget.todo, !done);
    setState(() {
      done = !done;
    });
  }

  void _handleDelete() {
    context.read<TodosCubit>().deleteTodo(widget.todo);
  }
}