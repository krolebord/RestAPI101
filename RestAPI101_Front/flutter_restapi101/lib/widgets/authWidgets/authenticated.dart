import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';
import 'package:flutter_restapi101/widgets/home/appTab.dart';
import 'package:flutter_restapi101/widgets/home/homePage.dart';
import 'package:flutter_restapi101/widgets/todos/todosFAB.dart';
import 'package:flutter_restapi101/widgets/todos/todosPage.dart';

class Authenticated extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [ 
        BlocProvider<UserCubit>(create: (context) => UserCubit()),
        BlocProvider<TodosCubit>(create: (context) => TodosCubit())
      ], 
      child: HomePage(
        tabs: [
          AppTab(
            tabIcon: Icons.list_alt, 
            tabText: "Todos", 
            page: TodosPage(),
            floatingActionButton: TodosFAB()
          ),
          AppTab(
            tabIcon: Icons.sticky_note_2, 
            tabText: "Notes", 
            page: Center(child: Text('Work in progress')),
            floatingActionButton: FloatingActionButton(
              onPressed: () {},
              child: Icon(Icons.note_add),
            )
          ),
          AppTab(
            tabIcon: Icons.settings_ethernet, 
            tabText: "Commands", 
            page: Center(child: Text('Work in progress')),
          )
        ]
      )
    );
  }
}