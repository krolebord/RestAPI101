import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';

abstract class LabelsRepository {
  Future<List<Label>> getLabels();

  Future<Label> getLabel(int id);

  Future<void> createLabel(LabelWriteDTO label);

  Future<void> updateLabel(int id, LabelWriteDTO label);

  Future<void> deleteLabel(int id);
}

abstract class LabelsError {
  final String errorMessage;

  LabelsError({required this.errorMessage});
}

class LabelsLoadingError extends LabelsError {
  LabelsLoadingError({required String errorMessage}) : super(errorMessage: errorMessage);
}

class LabelsUpdatingError extends LabelsError {
  LabelsUpdatingError({required String errorMessage}) : super(errorMessage: errorMessage);
}
