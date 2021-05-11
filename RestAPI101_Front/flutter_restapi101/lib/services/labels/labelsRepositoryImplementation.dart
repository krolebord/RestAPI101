import 'dart:io';
import 'dart:convert';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/services/authenticatedServiceMixin.dart';
import 'package:flutter_restapi101/services/labels/labelsRepository.dart';
import 'package:get_it/get_it.dart';

class LabelsRepositoryImplementation with AuthenticatedServiceMixin implements LabelsRepository {
  final GetIt getIt = GetIt.instance; 

  @override
  Future<List<Label>> getLabels() async {
    var response = await sendRequest(ApiRequests.getAllLabels());

    switch(response.statusCode) {
      case HttpStatus.ok: {
        List<dynamic> jsonTodos = json.decode(response.body);
        return jsonTodos.map((jsonTodo) => Label.fromJson(jsonTodo)).toList();
      }
      case HttpStatus.noContent: return [];
      default: throw LabelsLoadingError(errorMessage: response.body);
    }
  }

  @override
  Future<Label> getLabel(int id) async {
    var response = await sendRequest(ApiRequests.getSpecifiedLabel(id));

    switch(response.statusCode) {
      case HttpStatus.ok: 
        return Label.fromJson(json.decode(response.body));
      default: throw LabelsLoadingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> createLabel(LabelWriteDTO label) async {
    var request = ApiRequests.createLabel();
    request.body = json.encode(label.toJson());

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.created: return;
      default: throw LabelsUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> updateLabel(int id, LabelWriteDTO label) async {
    var request = ApiRequests.updateLabel(id);
    request.body = json.encode(label.toJson());

    var response = await sendRequest(request, jsonContent: true);

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      default: throw LabelsUpdatingError(errorMessage: response.body);
    }
  }

  @override
  Future<void> deleteLabel(int id) async {
    var response = await sendRequest(ApiRequests.deleteLabel(id));

    switch(response.statusCode) {
      case HttpStatus.noContent: return;
      case HttpStatus.notFound: return;
      default: throw LabelsUpdatingError(errorMessage: response.body);
    }
  }
}