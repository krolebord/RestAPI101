import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todoIncludeMode.dart';
import 'package:flutter_restapi101/services/colorGenerator/ColorGenerator.dart';

class TodosFilterBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      color: Theme.of(context).colorScheme.surface.withOpacity(0.7),
      child: BlocBuilder<LabelsCubit, LabelsState>(
        builder: (context, state) {
          if(state is LabelsLoaded) {
            return _LabelFilters(
              labels: state.labels
            );
          }

          return SizedBox(
            width: 36,
            height: 36,
            child: CircularProgressIndicator(),
          );
        },
      ),
    );
  }
}

class _LabelFilters extends StatefulWidget {
  final List<Label> labels;

  _LabelFilters({required this.labels});

  @override
  _LabelFiltersState createState() => _LabelFiltersState();
}

class _LabelFiltersState extends State<_LabelFilters> {
  TodoIncludeMode _includeMode = TodoIncludeMode.All;
  List<Label> _filters = [];

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 6),
      child: Wrap(
        spacing: 6,
        runSpacing: 4,
        children:[
          for(var includeMode in TodoIncludeMode.values)
            _buildIncludeChip(includeMode),

          const SizedBox(
            height: 28,
            child: const VerticalDivider(indent: 6, endIndent: 6)
          ),

          for(var label in widget.labels)
            _buildLabelFilterChip(label),
        ],
      )
    );
  }

  Widget _buildIncludeChip(TodoIncludeMode includeMode) {
    var labelText = includeMode.toString();
    labelText = labelText.substring(labelText.indexOf('.') + 1);

    return ChoiceChip(
      selected: _includeMode == includeMode,
      label: Text(labelText),
      onSelected: (value) => _onIncludeModeSelected(includeMode, value),
    );
  }

  void _onIncludeModeSelected(TodoIncludeMode includeMode, bool value) {
    setState(() {
      _includeMode = includeMode;
    });

    var cubit = context.read<TodosCubit>();

    cubit.setIncludeMode(_includeMode);
    cubit.fetchTodos();
  }

  Widget _buildLabelFilterChip(Label label) {
    var fontColor = ColorUtils.textColorOn(label.color);
    return FilterChip(
      selected: _filters.contains(label),
      backgroundColor: label.color,
      selectedColor: label.color,
      checkmarkColor: fontColor,
      label: Tooltip(
          waitDuration: const Duration(milliseconds: 500),
          message: label.description.isNotEmpty ? label.description : label.name,
          child: Text(
            label.name,
            style: TextStyle(color: fontColor),
          )
      ),
      onSelected: (value) => _onLabelSelected(label, value),
    );
  }

  void _onLabelSelected(Label label, bool value) {
    setState(() {
      if(value)
        _filters.add(label);
      else
        _filters.remove(label);
    });

    var cubit = context.read<TodosCubit>();

    cubit.setFilters(_filters);
    cubit.fetchTodos();
  }
}



