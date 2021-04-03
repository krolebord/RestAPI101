import 'package:flutter_restapi101/services/Auth/authService.dart';
import 'package:flutter_restapi101/services/Auth/authServiceImplementation.dart';
import 'package:flutter_restapi101/services/authPrefs/authPrefs.dart';
import 'package:get_it/get_it.dart';

extension AuthServiceRegister on GetIt {
  void registerAuthService() {
    this.registerSingletonWithDependencies<AuthService>(
      () => AuthServiceImplementation()..initialize(),
      dependsOn: [AuthPrefs]
    );
  }
}