part of 'auth_cubit.dart';

@immutable
abstract class AuthState extends Equatable {
  @override
  List<Object?> get props => [];
}

class AuthLoading extends AuthState {}

class AuthError extends AuthState {
  final String errorMessage;

  AuthError(this.errorMessage);

  @override
  List<Object?> get props => [errorMessage];
}

class AuthSignedOut extends AuthState {}

class AuthSignedIn extends AuthState {
  final AuthUser user;

  AuthSignedIn(this.user);

  @override
  List<Object?> get props => [user];
}
