import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/widgets/labels/labelChip.dart';

class AddLabelButton extends StatelessWidget {
  final Todo todo;

  AddLabelButton({required this.todo});

  @override
  Widget build(BuildContext context) {
    return Tooltip(
      message: 'Add label',
      waitDuration: Duration(milliseconds: 500),
      child: ActionChip(
        pressElevation: 0,
        side: BorderSide(
            color: Colors.white38,
            width: 1
        ),
        label: Icon(Icons.add),
        onPressed: () => _handlePressed(context),
      ),
    );
  }
  
  void _handlePressed(BuildContext context) async {
    final RenderBox button = context.findRenderObject()! as RenderBox;
    final RenderBox overlay = Navigator.of(context).overlay!.context.findRenderObject()! as RenderBox;
    final RelativeRect position = RelativeRect.fromRect(
      Rect.fromPoints(
        button.localToGlobal(Offset.zero, ancestor: overlay),
        button.localToGlobal(button.size.bottomRight(Offset.zero), ancestor: overlay),
      ),
      Offset.zero & overlay.size,
    );

    var labelsState = context.read<LabelsCubit>().state;

    if(!(labelsState is LabelsLoaded)) return;

    final List<PopupMenuEntry<Label>> items = labelsState.labels
      .where((label) => !todo.labelIds.contains(label.id))
      .map<PopupMenuEntry<Label>>(
          (label) => PopupMenuItem(child: LabelChip(label: label), value: label)
    ).toList();

    if (items.isEmpty) return;

    var label = await showMenu<Label>(
      elevation: 10,
      context: context,
      position: position,
      items: items,
    );

    if(label == null) return;

    context.read<TodosCubit>().addLabel(todo, label);
  }
}
