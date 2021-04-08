import 'package:get_it/get_it.dart';
import 'package:flutter_restapi101/services/services.dart';

void registerServices() {
  var services = GetIt.instance;

  services.registerAuthPrefs();

  services.registerAuthService();
  services.registerUserService();

  services.registerAuthenticatedClient();

  services.registerTodosRepository();
}