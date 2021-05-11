import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:json_annotation/json_annotation.dart';

part 'labelWriteDTO.g.dart';

@immutable
@JsonSerializable()
class LabelWriteDTO {
  final String name;
  final String? description;

  @JsonKey(
    toJson: Label.colorToInt,
    fromJson: Label.colorFromInt
  )
  final Color? color;

  LabelWriteDTO({required this.name, this.description, this.color});

  factory LabelWriteDTO.fromJson(Map<String, dynamic> json) => _$LabelWriteDTOFromJson(json);
  Map<String, dynamic> toJson() => _$LabelWriteDTOToJson(this);
}
