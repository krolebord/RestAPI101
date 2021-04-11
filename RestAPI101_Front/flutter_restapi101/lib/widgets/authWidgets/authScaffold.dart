import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/restApiAppBar.dart';

class AuthScaffold extends StatelessWidget {
  final Widget child;

  AuthScaffold({required this.child});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: RestApiAppBar(),
      body: Center(child: child)
    );
  }
}