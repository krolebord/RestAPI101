import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/widgets/todos/todoList.dart';
import 'package:flutter_restapi101/widgets/todos/todosStatusBar.dart';

class TodosPage extends StatelessWidget {
  TodosPage({Key? key}) : super(key :key);

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.max,
      children: [
        TodosStatusBar(),
        Expanded(
          child: BlocBuilder<TodosCubit, TodosState>(
            buildWhen: (previous, current) => !(previous is TodosLoaded && current is TodosLoading) && !(current is TodosUpdatingErrorState),
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

              if(state is TodosLoaded) {
                if(state.todos.isNotEmpty)
                  return TodoList(todos: state.todos);
                else
                  return Center(child: Text('No todos yet\n\n\nTry adding one'));
              }

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
          ),
        ),
      ],
    );
  }
}