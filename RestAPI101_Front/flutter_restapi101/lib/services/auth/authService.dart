import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/models/auth/authRegisterCredentials.dart';
import 'package:flutter_restapi101/models/auth/authUser.dart';

abstract class AuthService {
  AuthUser? get currentUser;
  Stream<AuthUser?> get userChanges;

  void initialize();

  Future<void> login(AuthCredentials credentials);

  Future<void> register(AuthRegisterCredentials credentials);

  Future<void> logout();

  Future<void> updateToken();
}

class AuthServiceError {
  int responseCode;
  String errorMessage;

  AuthServiceError({required this.responseCode, required this.errorMessage});

  AuthServiceError.unauthorized() :
    responseCode = 401,
    errorMessage = 'Signed out';
}