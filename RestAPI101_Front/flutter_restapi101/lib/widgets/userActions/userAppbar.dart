import 'package:flutter/material.dart';
import 'package:flutter_restapi101/appRoutes.dart';
import 'package:vrouter/vrouter.dart';
import 'package:flutter_restapi101/widgets/userActions/userActions.dart';

class UserAppBar extends StatelessWidget implements PreferredSizeWidget {
  @override
  Size get preferredSize => Size.fromHeight(60);

  @override
  Widget build(BuildContext context) {
    return AppBar(
      title: InkWell(
        onTap: () => context.vRouter.push(AppRoutes.homeRoute),
        borderRadius: BorderRadius.circular(4),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 50),
          child: Text("Rest API 101"),
        )
      ),
      actions: [
        UserActions(),
        SizedBox(width: 24)
      ],
    );
  }
}