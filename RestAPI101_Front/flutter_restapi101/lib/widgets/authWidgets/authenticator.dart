import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/models/auth/authUser.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authScaffold.dart';
import 'signedOut.dart';

class Authenticator extends StatelessWidget {
  final Widget Function(BuildContext context, AuthUser user) builder;

  Authenticator({required Key key, required this.builder}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BlocProvider<AuthCubit>(
      create: (context) => AuthCubit(),
      child: BlocConsumer<AuthCubit, AuthState>(
        listener: (context, state) {
          if(state is AuthError) {
            ScaffoldMessenger.of(context)
              .showSnackBar(SnackBar(content: Text(state.errorMessage)));
          }
        },
        builder: (context, state) {
          if(state is AuthLoading) {
            return AuthScaffold(child: CircularProgressIndicator());
          }
          else if(state is AuthSignedOut) {
            return AuthScaffold(child: SignedOut());
          }
          else if(state is AuthSignedIn) {
            return builder.call(context, state.user);
          }

          return AuthScaffold(child: Center(child: Text('Unexpected error occured')));
        },
      ),
    );
  }
}