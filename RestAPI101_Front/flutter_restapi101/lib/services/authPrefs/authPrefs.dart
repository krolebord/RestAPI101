import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/models/auth/authToken.dart';

abstract class AuthPrefs {
  Future<void> initialize();

  AuthCredentials? get authCredentials;
  set authCredentials(AuthCredentials? credentials);

  AuthToken? get authToken;
  set authToken(AuthToken? token);
}
