import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/themeChanger.dart';

class RestApiAppBar extends StatelessWidget with PreferredSizeWidget {
  @override
  Size get preferredSize => Size.fromHeight(40);

  @override
  Widget build(BuildContext context) {
    return AppBar(
      leading: Icon(Icons.api),
      title: Text("Rest API 101"),
      actions: [
        ThemeChanger()
      ],
    );
  }
}