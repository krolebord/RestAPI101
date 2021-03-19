import 'package:flutter/material.dart';
class ThemeNotifier extends ChangeNotifier {
  ThemeData _themeData;
  
  ThemeNotifier(this._themeData);
  
  ThemeData get theme => _themeData;

  void setTheme(ThemeData theme) {
    _themeData = theme;
    notifyListeners();
  }
}
