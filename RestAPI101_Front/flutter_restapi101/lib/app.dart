import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/auth/authUser.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authenticator.dart';
import 'package:flutter_restapi101/widgets/authenticated.dart';
import 'package:flutter_restapi101/widgets/themeProvider.dart';
import 'package:provider/provider.dart';

class RestAPIApp extends StatelessWidget {
  final Key _authenticatorKey = UniqueKey();

  @override
  Widget build(BuildContext context) {
    return ThemeProvider(
      initialTheme: ThemeData.dark(),
      builder: (context, theme) => MaterialApp(
        title: "RestAPI 101",
        theme: theme,
        home: Authenticator(
          key: _authenticatorKey,
          builder: (context, user) {
            return Provider<AuthUser>.value(
              value: user,
              child: Authenticated(),
            );
          },
        )
      ),
    );
  }
}