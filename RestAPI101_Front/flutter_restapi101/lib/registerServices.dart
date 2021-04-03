import 'package:get_it/get_it.dart';
import 'package:flutter_restapi101/services/authPrefs/authPrefsRegister.dart';
import 'package:flutter_restapi101/services/auth/authServiceRegister.dart';
import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClientRegister.dart';

void registerServices() {
  var services = GetIt.instance;

  services.registerAuthPrefs();

  services.registerAuthService();

  services.registerAuthenticatedClient();
}