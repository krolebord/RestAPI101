import 'package:flutter_restapi101/services/auth/authService.dart';
import 'package:flutter_restapi101/services/auth/authServiceImplementation.dart';
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