import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/userActions/userAppbar.dart';

class SettingsPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: UserAppBar(),
      body: Center(
        child: Text('Settings'),
      ),
    );
  }
}