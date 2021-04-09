import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/services/todos/todosRepository.dart';

class TodosStatusBar extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocListener<TodosCubit, TodosState>(
      listener: (context, state) {
        if(state is TodosUpdatingError)
          ScaffoldMessenger.of(context)
            .showSnackBar(SnackBar(content: Text('Unkown error occurred while updating')));
      },
      child: Container(
        color: Theme.of(context).colorScheme.surface.withOpacity(0.7),
        child: SizedBox(
          height: 48,
          width: double.infinity,
          child: Row(
            mainAxisSize: MainAxisSize.max,
            children: [
              Expanded(
                flex: 1,
                child: Align(
                  alignment: Alignment.centerLeft,
                  child: _buildStatus()
                ),
              ),
              Expanded(
                flex: 1,
                child: Align(
                  alignment: Alignment.center,
                  child: _buildTitle()
                ),
              ),
              Expanded(
                flex: 1,
                child: Align(
                  alignment: Alignment.centerRight,
                  child: Padding(
                    padding: const EdgeInsets.only(right: 20),
                    child: IconButton(
                      splashRadius: 18,
                      icon: Icon(Icons.refresh), 
                      onPressed: () => _onRefresh(context)
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
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

  Widget _buildStatus() {
    return Padding(
      padding: const EdgeInsets.only(left: 20),
      child: BlocBuilder<TodosCubit, TodosState>(
        builder: (context, state) {
          if(state is TodosLoading)
            return Tooltip(
              message: "Syncing...",
              child: Icon(Icons.sync),
            );

          if(state is TodosLoadingErrorState)
            return Text('Couldn\'t sync todos');

          return Tooltip(
            message: "Synced",
            child: Icon(Icons.cloud_done),
          );
        }
      ),
    );
  }

  void _onRefresh(BuildContext context) {
    context.read<TodosCubit>().updateTodos();
  }
}
