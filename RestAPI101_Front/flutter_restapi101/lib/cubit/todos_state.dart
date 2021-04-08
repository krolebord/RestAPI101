part of 'todos_cubit.dart';

@immutable
abstract class TodosState extends Equatable {
  @override
  List<Object?> get props => [];
}

class TodosLoading extends TodosState {}

abstract class TodosErrorState extends TodosState {
  final String message;

  TodosErrorState({required this.message});

  @override
  List<Object?> get props => [message];
}

class TodosUpdatingErrorState extends TodosErrorState {
  TodosUpdatingErrorState({required String message}) : super(message: message);
}

class TodosLoadingErrorState extends TodosErrorState {
  TodosLoadingErrorState({required String message}) : super(message: message);
}

class TodosLoaded extends TodosState {
  final List<Todo> todos;

  TodosLoaded({required this.todos});

  @override
  List<Object?> get props => [todos];
}
