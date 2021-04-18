import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/cubit/user_cubit.dart';
import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/models/user/userChangePasswordDTO.dart';
import 'package:flutter_restapi101/widgets/account/changePasswordDialog.dart';
import 'package:flutter_restapi101/widgets/account/changeUsernameDialog.dart';
import 'package:flutter_restapi101/widgets/account/confirmationDialog.dart';
import 'package:flutter_restapi101/widgets/userActions/userAppbar.dart';

class AccountPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: UserAppBar(),
      body: BlocBuilder<UserCubit, UserState>(
        builder: (context, state) {
          if(state is UserLoaded)
            return _AccountSettings(user: state.user);
          return Center(child: CircularProgressIndicator());
        },
      )
    );
  }
}

class _AccountSettings extends StatelessWidget {
  final ApiUser user;

  _AccountSettings({required this.user});

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    var deleteColor = theme.errorColor;

    return Align(
      alignment: Alignment.center,
      child: ConstrainedBox(
        constraints: BoxConstraints(maxWidth: 700),
        child: SingleChildScrollView(
          child: Card(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Container(
                  alignment: Alignment.topLeft,
                  padding: EdgeInsets.only(left: 12, top: 8),
                  child: Text(
                    'Account settings',
                    style: theme.textTheme.headline5
                  ),
                ),
                SettingsTile(
                  title: 'Login',
                  value: user.login,
                ),
                _SettingsDivider(),
                SettingsTile(
                  title: 'Username', 
                  value: user.username,
                  onTap: () => _handleChangeUsername(context),
                ),
                _SettingsDivider(),
                SettingsTile(
                  title: 'Password', 
                  value: '********',
                  onTap: () => _handleChangePassword(context),
                ),
                _SettingsDivider(),
                ListTile(
                  title: Text(
                    'Delete account', 
                    style: TextStyle(color: deleteColor)
                  ),
                  trailing: Icon(
                    Icons.delete_forever,
                    color: deleteColor,
                  ),
                  onTap: () => _handleDeleteUser(context),
                )
              ],
            )
          ),
        ),
      ),
    );
  }

  void _handleChangeUsername(BuildContext context) async {
    var username = await showDialog<String>(
      context: context, 
      builder: (context) => ChangeUsernameDialog(username: user.username),
    );
    
    if(username != null)
      context.read<UserCubit>().changeUsername(username);
  }

  void _handleChangePassword(BuildContext context) async {
    var passwordChange = await showDialog<UserChangePasswordDTO>(
      context: context, 
      builder: (context) => ChangePasswordDialog(),
    );
    
    if(passwordChange == null) return;

    var authCubit = context.read<AuthCubit>();
    var messanger = ScaffoldMessenger.of(context);
    context.read<UserCubit>().changePassword(passwordChange).then((success) {
      if(!success) return;

      authCubit.logout();
      messanger.showSnackBar(SnackBar(content: Text('Password has been successfully changed')));
    });
  }

  void _handleDeleteUser(BuildContext context) async {
    bool? delete = await showDialog<bool>(context: context, builder: (context) => 
      ConfirmationDialog(
        title: 'Delete Account',
        description: 'Are you sure you want to delete your account?',
        confirmLabel: Text(
          'Delete',
          style: TextStyle(color: Theme.of(context).errorColor),
        ),
      )
    );

    if(delete == null || !delete) 
      return;

    var authCubit = context.read<AuthCubit>();
    var messanger = ScaffoldMessenger.of(context);
    context.read<UserCubit>().deleteUser().then((value) {
      authCubit.logout();
      messanger.showSnackBar(SnackBar(content: Text('Account has been successfully deleted')));
    });
  }
}

class SettingsTile extends StatelessWidget {
  final String title;
  final String value;
  final void Function()? onTap;

  SettingsTile({required this.title, required this.value, this.onTap}); 

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    
    var titleStyle = theme.textTheme.subtitle2;
    titleStyle = titleStyle?.apply(color: titleStyle.color?.withOpacity(0.6));

    return ListTile(
      contentPadding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
      title: Column(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            title,
            style: titleStyle,
          ),
          Text(
            value,
            style: Theme.of(context).textTheme.headline5,
          )
        ],
      ),
      trailing: onTap == null ? null : Icon(Icons.edit),
      onTap: onTap,
    );
  }
}

class _SettingsDivider extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 12.0),
      child: Divider(height: 0),
    );
  }
}
