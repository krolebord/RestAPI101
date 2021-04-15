import 'package:flutter/material.dart';
import 'package:flutter_restapi101/appRoutes.dart';
import 'package:vrouter/vrouter.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';
import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/widgets/userActions/userAction.dart';

class UserActions extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Center(
      child: BlocConsumer<UserCubit, UserState>(
        listener: (context, state) {
          if(state is UserError)
            ScaffoldMessenger.of(context)
              .showSnackBar(SnackBar(content: Text(state.errorMessage)));
        },
        builder: (context, state) {
          if(state is UserInitial) {
            return SizedBox(width: 40, height: 40, child: CircularProgressIndicator());
          }

          if (state is UserLoaded) {
            return _UserActions(state.user);
          }

          return TextButton(
            onPressed: () => context.read<UserCubit>().update(), 
            child: Text('Load user'),
          );
        }, 
        
      ),
    );
  }
}

class _UserActions extends StatelessWidget {
  final ApiUser user;

  _UserActions(this.user);

  @override
  Widget build(BuildContext context) {
    List<UserAction> actions = [
      UserAction(
        icon: Icons.account_circle, 
        label: 'Account', 
        onPressed: (context) => context.vRouter.push(AppRoutes.accountRoute)
      ),
      UserAction(
        icon: Icons.settings, 
        label: "Settings", 
        onPressed: (context) => context.vRouter.push(AppRoutes.settingsRoute)
      ),
      UserAction(
        icon: Icons.logout, 
        label: "Log out", 
        onPressed: (context) => context.read<AuthCubit>().logout()
      ),
    ];
    return DropdownButtonHideUnderline(
      child: DropdownButton<UserAction>(
        items: actions.map((action) => DropdownMenuItem<UserAction>(
          child: Row(children: [
            Padding(
              padding: const EdgeInsets.only(right: 4),
              child: Icon(action.icon, color: Theme.of(context).colorScheme.onSurface),
            ),
            Text(action.label)
          ]),
          onTap: () => action.onPressed.call(context),
          value: action,
        )).toList(),
        selectedItemBuilder: (context) => [
          Row(children: [
            Padding(
              padding: const EdgeInsets.only(right: 4),
              child: Icon(Icons.account_circle),
            ),
            Text(user.username)
          ])
        ],
        onChanged: (_) {},
        value: actions.first
      ),
    );
  }
}