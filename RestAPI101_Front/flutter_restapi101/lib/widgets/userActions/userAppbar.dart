import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/userActions/userActions.dart';

class UserAppBar extends StatelessWidget implements PreferredSizeWidget {
  @override
  Size get preferredSize => Size.fromHeight(60);

  @override
  Widget build(BuildContext context) {
    return AppBar(
      title: Text("Rest API 101"),
      actions: [
        UserActions(),
        SizedBox(width: 24)
      ],
    );
  }
}