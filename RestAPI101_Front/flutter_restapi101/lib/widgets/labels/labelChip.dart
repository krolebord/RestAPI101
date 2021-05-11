import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/services/colorGenerator/ColorGenerator.dart';

class LabelChip extends StatelessWidget {
  final Label label;

  final Widget? deleteIcon;
  final void Function()? onDeleted;
  
  LabelChip({
    required this.label,
    this.deleteIcon,
    this.onDeleted
  });

  @override
  Widget build(BuildContext context) {
    return Tooltip(
      waitDuration: const Duration(milliseconds: 500),
      message: label.description.isNotEmpty ? label.description : label.name,
      child: Chip(
        backgroundColor: label.color,
        label: Text(
          label.name,
          style: TextStyle(color: ColorUtils.textColorOn(label.color))
        ),
        deleteIcon: deleteIcon,
        onDeleted: onDeleted,
        deleteButtonTooltipMessage: 'Remove',
      ),
    );
  }
}