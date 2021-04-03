import 'package:flutter_restapi101/services/authPrefs/authPrefs.dart';
import 'package:flutter_restapi101/services/authPrefs/authPrefsImplementation.dart';
import 'package:get_it/get_it.dart';

extension AuthPrefsRegister on GetIt {
  void registerAuthPrefs() {
    this.registerSingletonAsync<AuthPrefs>(() async {
      var prefs = AuthPrefsImplementation();
      await prefs.initialize();
      return prefs;
    });
  }
}