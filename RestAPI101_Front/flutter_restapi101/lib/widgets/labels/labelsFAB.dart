import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';
import 'package:flutter_restapi101/widgets/labels/labelDialog.dart';

class LabelsFAB extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return FloatingActionButton(
      child: const Icon(Icons.add),
      onPressed: () => _handlePressed(context)
    );
  }

  void _handlePressed(BuildContext context) async {
    var result = await showDialog<LabelWriteDTO>(
      context: context, 
      builder: (context) => LabelDialog(),
    );

    if(result == null) return;

    context.read<LabelsCubit>().createLabel(result);
  }
}