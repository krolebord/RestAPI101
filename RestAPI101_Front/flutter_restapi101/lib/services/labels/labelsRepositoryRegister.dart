import 'package:flutter_restapi101/services/labels/labelsRepository.dart';
import 'package:flutter_restapi101/services/labels/labelsRepositoryImplementation.dart';
import 'package:get_it/get_it.dart';

extension LabelsRepositoryRegister on GetIt {
  void registerLabelsRepository() {
    this.registerLazySingleton<LabelsRepository>(() => LabelsRepositoryImplementation());
  }
}