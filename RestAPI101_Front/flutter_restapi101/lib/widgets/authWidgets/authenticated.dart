import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/labels_cubit.dart';
import 'package:flutter_restapi101/cubit/todos_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';

class Authenticated extends StatelessWidget {
  final Widget child;

  Authenticated({required this.child});

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [ 
        BlocProvider<UserCubit>(create: (context) => UserCubit()),
        BlocProvider<LabelsCubit>(create: (context) => LabelsCubit()),
        BlocProvider<TodosCubit>(create: (context) => TodosCubit())
      ], 
      child: child
    );
  }
}
