import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/themeChanger.dart';
import 'package:flutter_restapi101/widgets/themeProvider.dart';

class RestAPIApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ThemeProvider(
      initialTheme: ThemeData.dark(),
      builder: (context, theme) => MaterialApp(
        title: "RestAPI 101",
        theme: theme,
        home: Scaffold(
          appBar: AppBar(
            leading: Icon(Icons.api),
            title: Text("Rest API 101"),
            actions: [
              ThemeChanger()
            ],
          ),
          body: Center(
          ),
        ),
      ),
    );
  }
}