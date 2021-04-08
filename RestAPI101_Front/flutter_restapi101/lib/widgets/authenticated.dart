import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';
import 'package:flutter_restapi101/widgets/home/homePage.dart';

class Authenticated extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [ 
        BlocProvider<UserCubit>(create: (context) => UserCubit()),
        BlocProvider<TodosCubit>(create: (context) => TodosCubit())
      ], 
      child: HomePage()
    );
  }
}