import 'authToken.dart';
import 'authCredentials.dart';

class AuthUser {
  AuthCredentials credentials;
  AuthToken token;

  AuthUser({required this.credentials, required this.token});
}
