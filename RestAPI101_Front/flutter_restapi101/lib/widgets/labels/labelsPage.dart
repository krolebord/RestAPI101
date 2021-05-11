import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/widgets/labels/labelsList.dart';
import 'package:flutter_restapi101/widgets/labels/labelsStatusBar.dart';

class LabelsPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.max,
      children: [
        LabelsStatusBar(),
        Expanded(
          child: BlocBuilder<LabelsCubit, LabelsState>(
            buildWhen: (previous, current) => !(previous is LabelsLoaded && current is LabelsLoading) && !(current is LabelsUpdatingErrorState),
            builder: (context, state) {
              if(state is LabelsLoading) {
                return Center(
                  child: SizedBox(
                    width: 80,
                    height: 80,
                    child: CircularProgressIndicator(),
                  ),
                );
              }

              if(state is LabelsLoaded) {
                if(state.labels.isNotEmpty)
                  return LabelsList(labels: state.labels);
                else
                  return Center(child: Text('No labels yet\n\n\nTry adding one'));
              }

              if(state is LabelsLoadingErrorState) {
                return Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Text('Couldn\'t load labels'),
                      Text(state.message),
                      IconButton(
                        icon: Icon(Icons.refresh), 
                        onPressed: () => context.read<LabelsCubit>().fetchLabels(),
                      )
                    ],
                  ),
                );
              }

              return Center(child: Text('Unexpected error occurred'));
            },
          ),
        ),
      ],
    );
  }
}