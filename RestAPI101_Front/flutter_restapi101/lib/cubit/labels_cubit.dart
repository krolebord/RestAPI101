import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';
import 'package:flutter_restapi101/services/labels/labelsRepository.dart';
import 'package:get_it/get_it.dart';

part 'labels_state.dart';

class LabelsCubit extends Cubit<LabelsState> {
  final LabelsRepository _repository;

  LabelsCubit() :
    _repository = GetIt.instance.get<LabelsRepository>(), 
    super(LabelsLoading()) {
      fetchLabels();
    }

  Future<void> fetchLabels() async {
    emit(LabelsLoading());

    try {
      var labels = await _repository.getLabels();
      emit(LabelsLoaded(labels: labels));
    }
    on LabelsLoadingError catch(e) {
      emit(LabelsLoadingErrorState(message: e.errorMessage));
    }
  }

  void createLabel(LabelWriteDTO label) =>
    _handleUpdateAction(_repository.createLabel(label));

  void updateLabel(int id, LabelWriteDTO label) =>
    _handleUpdateAction(_repository.updateLabel(id, label));

  void deleteLabel(int id) =>
    _handleUpdateAction(_repository.deleteLabel(id));

  void _handleUpdateAction(Future<void> action) async {
    emit(LabelsLoading());

    try {
      await action;
    }
    on LabelsUpdatingError catch(e) {
      emit(LabelsUpdatingErrorState(message: e.errorMessage));
    }

    await fetchLabels();
  }
}
