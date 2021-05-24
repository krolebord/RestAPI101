import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/themeNotifier.dart';
import 'package:provider/provider.dart';

class ThemeProvider extends StatelessWidget {
  final ThemeData _initialTheme;
  final Widget Function(
    BuildContext context,
    ThemeData theme,
  ) builder;

  ThemeProvider({
    required this.builder
  }) : _initialTheme = ThemeData.dark(), super();
  
  ThemeProvider.light({
    required this.builder
  }) : _initialTheme = ThemeData.light();

  ThemeProvider.dark({
    required this.builder
  }) : _initialTheme = ThemeData.dark();

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider<ThemeNotifier>(
      create: (context) => ThemeNotifier(_initialTheme),
      child: Consumer<ThemeNotifier>(
        builder: (context, value, child) => builder(context, value.theme),
      ) 
    );
  }

  static ThemeNotifier of(BuildContext context, {bool listen = true}) {
    return Provider.of<ThemeNotifier>(context, listen: listen);
  }
}