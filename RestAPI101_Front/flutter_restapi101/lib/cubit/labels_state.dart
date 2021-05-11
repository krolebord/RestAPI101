part of 'labels_cubit.dart';

@immutable
abstract class LabelsState extends Equatable {
  const LabelsState();

  @override
  List<Object> get props => [];
}

class LabelsLoading extends LabelsState {}

abstract class LabelsErrorState extends LabelsState {
  final String message;

  LabelsErrorState({required this.message});

  @override
  List<Object> get props => [message];
}

class LabelsUpdatingErrorState extends LabelsErrorState {
  LabelsUpdatingErrorState({required String message}) : super(message: message);
}

class LabelsLoadingErrorState extends LabelsErrorState {
  LabelsLoadingErrorState({required String message}) : super(message: message);
}

class LabelsLoaded extends LabelsState {
  final List<Label> labels;

  LabelsLoaded({required this.labels});

  @override
  List<Object> get props => [labels];
}
