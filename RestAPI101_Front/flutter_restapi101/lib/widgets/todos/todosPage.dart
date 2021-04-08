import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/widgets/todos/todoList.dart';

class TodosPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocBuilder<TodosCubit, TodosState>(
      buildWhen: (previous, current) => !(previous is TodosLoaded && current is TodosLoading),
      builder: (context, state) {
        if(state is TodosLoading) {
          return Center(
            child: SizedBox(
              width: 80,
              height: 80,
              child: CircularProgressIndicator(),
            ),
          );
        }

        if(state is TodosLoaded)
          return TodoList(todos: state.todos);

        if(state is TodosLoadingErrorState) {
          return Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Text('Couldn\'t load todos'),
                Text(state.message),
                IconButton(
                  icon: Icon(Icons.refresh), 
                  onPressed: () => context.read<TodosCubit>().updateTodos(),
                )
              ],
            ),
          );
        }

        return Center(child: Text('Unexpected error occurred'));
      },
    );
  }
}