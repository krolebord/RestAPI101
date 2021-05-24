import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';
import 'package:flutter_restapi101/widgets/labels/labelChip.dart';
import 'package:flutter_restapi101/widgets/labels/labelDialog.dart';

class LabelsList extends StatelessWidget {
  final List<Label> labels;

  LabelsList({required this.labels});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      key: PageStorageKey<String>("LabelsPage"),
      padding: const EdgeInsets.only(bottom: 78),
      itemCount: labels.length,
      itemBuilder: _buildLabelTile
    );
  }

  Widget _buildLabelTile(BuildContext context, int index) {
    var label = labels[index];

    return Card(
      child: ListTile(
        leading: LabelChip(label: label),
        title: Center(
          child: Text(label.description)
        ),
        trailing: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            Tooltip(
              message: 'Edit',
              child: IconButton(
                icon: Icon(Icons.edit),
                splashRadius: 22,
                onPressed: () => _handleEdit(context, label),
              ),
            ),
            Tooltip(
              message: 'Delete',
              child: IconButton(
                icon: Icon(Icons.delete, color: Theme.of(context).errorColor),
                splashRadius: 22,
                onPressed: () => context.read<LabelsCubit>().deleteLabel(label.id),
              ),
            ),
          ],
        ),
      ),
    );
  }

  void _handleEdit(BuildContext context, Label label) async {
    var result = await showDialog<LabelWriteDTO>(
      context: context,
      builder: (context) => LabelDialog(label: label),
    );

    if(result == null) return;

    context.read<LabelsCubit>().updateLabel(label.id, result);
  }
}