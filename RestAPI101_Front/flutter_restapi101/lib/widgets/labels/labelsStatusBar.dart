import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/widgets/common/statusBar.dart';
import 'package:flutter_restapi101/widgets/common/syncStatus.dart';

class LabelsStatusBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return StatusBar<LabelsCubit, LabelsState>(
      listener: (context, state) {
        if(state is LabelsUpdatingErrorState)
          ScaffoldMessenger.of(context)
            .showSnackBar(SnackBar(content: Text(state.message)));
            //.showSnackBar(SnackBar(content: Text('Unkown error occurred while updating')));
      },
      leading: _buildStatus(),
      title: _buildTitle(),
      trailing: _buildRefreshButton(context),
    );
  }

  Widget _buildStatus() {
    return Padding(
      padding: const EdgeInsets.only(left: 20),
      child: BlocBuilder<LabelsCubit, LabelsState>(
        builder: (context, state) {
          if(state is LabelsLoading)
            return SyncStatus.syncing;

          if(state is LabelsLoadingErrorState)
            return SyncStatus.error;

          return SyncStatus.synced;
        }
      ),
    );
  }

  Widget _buildTitle() {
    return BlocBuilder<LabelsCubit, LabelsState>(
      buildWhen: (previous, current) => current is LabelsLoaded,
      builder: (context, state) {
        if(state is LabelsLoaded) {
          var count = state.labels.length;
          return Text(count.toString() + (count == 1 ? ' label' : ' labels'));
        }

        return Text('Labels');
      },
    );
  }

  Widget _buildRefreshButton(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(right: 20),
      child: IconButton(
        splashRadius: 18,
        icon: Icon(Icons.refresh), 
        onPressed: () => _onRefresh(context)
      ),
    );
  }

  void _onRefresh(BuildContext context) {
    context.read<LabelsCubit>().fetchLabels();
  }
}