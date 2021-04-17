part of 'user_cubit.dart';

@immutable
abstract class UserState extends Equatable {
  @override
  List<Object?> get props => [];
}

class UserLoading extends UserState {}

class UserError extends UserState {
  final String errorMessage;

  UserError({required this.errorMessage});

  @override
  List<Object?> get props => [errorMessage];
}

class UserLoaded extends UserState {
  final ApiUser user;

  UserLoaded({required this.user});

  @override
  List<Object?> get props => [user];
}
