import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/services/colorGenerator/ColorGenerator.dart';

class LabelFilterBar extends StatelessWidget {
  final void Function(List<Label> newFilters)? onFiltersChanged;

  LabelFilterBar({this.onFiltersChanged});

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      color: Theme.of(context).colorScheme.surface.withOpacity(0.7),
      child: BlocBuilder<LabelsCubit, LabelsState>(
        builder: (context, state) {
          if(state is LabelsLoaded) {
            if(state.labels.length == 0)
              return Padding(
                padding: const EdgeInsets.all(8),
                child: Text('No labels'),
              );

            return _LabelFilters(
              labels: state.labels,
              onFiltersChanged: onFiltersChanged
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
  final void Function(List<Label> newFilters)? onFiltersChanged;
  final List<Label> labels;

  _LabelFilters({required this.labels, this.onFiltersChanged});

  @override
  _LabelFiltersState createState() => _LabelFiltersState();
}

class _LabelFiltersState extends State<_LabelFilters> {
  List<Label> _filters = [];

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 6),
      child: Wrap(
        children: widget.labels.map<Widget>((label) {
          var fontColor = ColorUtils.textColorOn(label.color);
          return Padding(
            padding: const EdgeInsets.symmetric(horizontal: 2),
            child: FilterChip(
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
              onSelected: (value) {
                setState(() {
                  if(value)
                    _filters.add(label);
                  else
                    _filters.remove(label);
                });
                widget.onFiltersChanged?.call(_filters);
              },
            ),
          );
        }).toList(growable: false),
      ),
    );
  }
}



