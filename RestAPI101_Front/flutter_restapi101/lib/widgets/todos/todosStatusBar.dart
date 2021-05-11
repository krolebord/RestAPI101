import 'package:expandable/expandable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/widgets/common/statusBar.dart';
import 'package:flutter_restapi101/widgets/common/syncStatus.dart';
import 'package:flutter_restapi101/widgets/labels/labelFilterBar.dart';

class TodosStatusBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ExpandablePanel(
      header: _TodosMainBar(),
      collapsed: Container(),
      expanded: LabelFilterBar(
        onFiltersChanged: (filters) => _handleFiltersChanged(context, filters),
      ),
      theme: ExpandableThemeData(
        hasIcon: false,
        useInkWell: false,
        tapHeaderToExpand: false,
        tapBodyToCollapse: false,
        tapBodyToExpand: false
      ),
    );
  }

  void _handleFiltersChanged(BuildContext context, List<Label> filters) async {
    var cubit = context.read<TodosCubit>();

    cubit.setFilters(filters);
    cubit.fetchTodos();
  }
}

class _TodosMainBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return StatusBar<TodosCubit, TodosState>(
      listener: (context, state) {
        if(state is TodosUpdatingErrorState)
          ScaffoldMessenger.of(context)
            .showSnackBar(SnackBar(content: Text(state.message)));
      },
      leading: _buildStatus(),
      title: _buildTitle(),
      trailing: _buildTrailing(context),
    );
  }

  Widget _buildStatus() {
    return Padding(
      padding: const EdgeInsets.only(left: 20),
      child: BlocBuilder<TodosCubit, TodosState>(
        builder: (context, state) {
          if(state is TodosLoading)
            return SyncStatus.syncing;

          if(state is TodosLoadingErrorState)
            return SyncStatus.error;

          return SyncStatus.synced;
        }
      ),
    );
  }

  Widget _buildTitle() {
    return BlocBuilder<TodosCubit, TodosState>(
      buildWhen: (previous, current) => current is TodosLoaded,
      builder: (context, state) {
        if(state is TodosLoaded) {
          int doneCount = 0;
          state.todos.forEach((todo) {
            if(todo.done) doneCount++;
          });
          return Text('Done: $doneCount/${state.todos.length}');
        }

        return Text('Todos');
      },
    );
  }

  Widget _buildTrailing(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(right: 20),
      child: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          TextButton(
            child: Text('Filter'),
            onPressed: ExpandableController.of(context, required: true)?.toggle
          ),
          IconButton(
            splashRadius: 18,
            icon: Icon(Icons.refresh),
            onPressed: () => _handleRefresh(context)
          ),
        ],
      ),
    );
  }

  void _handleRefresh(BuildContext context) {
    context.read<TodosCubit>().fetchTodos();
  }
}
