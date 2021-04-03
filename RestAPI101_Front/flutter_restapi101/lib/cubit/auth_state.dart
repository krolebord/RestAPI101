part of 'auth_cubit.dart';

@immutable
abstract class AuthState {}

class AuthLoading extends AuthState {}

class AuthError extends AuthState {
  final String errorMessage;

  AuthError(this.errorMessage);
}

class AuthSignedOut extends AuthState {}

class AuthSignedIn extends AuthState {
  final AuthUser user;

  AuthSignedIn(this.user);
}
