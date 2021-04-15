import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/themeProvider/themeChanger.dart';

class AuthScaffold extends StatelessWidget {
  final Widget child;

  AuthScaffold({required this.child});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        leading: Icon(Icons.api),
        title: Text("Rest API 101"),
        actions: [
          ThemeChanger()
        ],
      ),
      body: Center(child: child)
    );
  }
}