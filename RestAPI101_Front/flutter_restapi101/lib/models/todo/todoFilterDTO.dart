import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/todo/todoIncludeMode.dart';
import 'package:json_annotation/json_annotation.dart';

part 'todoFilterDTO.g.dart';

@immutable
@JsonSerializable()
class TodoFilterDTO {
  final TodoIncludeMode includeMode;

  @JsonKey(
    includeIfNull: false,
    toJson: _labelIdsFromLabels)
  final List<Label>? labelIds;

  TodoFilterDTO({this.includeMode = TodoIncludeMode.All, this.labelIds});

  Map<String, dynamic> toJson() => _$TodoFilterDTOToJson(this);

  static List<String> _labelIdsFromLabels(List<Label>? labels) =>
    labels == null ? [] : labels.map<String>((label) => label.id.toString()).toList();
}