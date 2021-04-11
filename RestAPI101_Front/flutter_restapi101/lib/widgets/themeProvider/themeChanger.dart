import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/themeProvider/themeProvider.dart';

class ThemeChanger extends StatefulWidget {
  @override
  _ThemeChangerState createState() => _ThemeChangerState();
}

class _ThemeChangerState extends State<ThemeChanger> {
  late bool darkMode;

  @override
  void initState() {
    super.initState();
    darkMode = ThemeProvider.of(context, listen: false).theme.brightness == Brightness.dark;
  }
  
  @override
  Widget build(BuildContext context) {
    ThemeData theme = Theme.of(context);

    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      mainAxisSize: MainAxisSize.min,
      children: [
        Switch(
          value: darkMode,
          onChanged: _changeTheme,
          inactiveThumbColor: theme.colorScheme.onPrimary,
          inactiveTrackColor: theme.colorScheme.onPrimary.withOpacity(0.5),
        ),
        Icon(
          darkMode ? Icons.brightness_3 : Icons.brightness_4, 
          color: darkMode ? Colors.yellow[200] : Colors.amber[500])
      ],
    );
  }

  void _changeTheme(bool value) {
    darkMode = !darkMode;
    ThemeProvider.of(context, listen: false)
      .setTheme(darkMode ? ThemeData.dark() : ThemeData.light());
  }
}