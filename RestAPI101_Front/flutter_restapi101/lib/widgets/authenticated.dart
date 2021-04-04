import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';
import 'package:flutter_restapi101/widgets/userActions/userActions.dart';

class Authenticated extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [ 
        BlocProvider<UserCubit>(create: (context) => UserCubit()),
        BlocProvider<TodosCubit>(create: (context) => TodosCubit()) 
      ], 
      child: Scaffold(
        appBar: AppBar(
          leading: Icon(Icons.api),
          title: Text("Rest API 101"),
          actions: [
            UserActions(),
            SizedBox(width: 16)
          ],
        ),
      )
    );
  }
}