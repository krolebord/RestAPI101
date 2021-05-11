import 'package:flutter/material.dart';
import 'package:json_annotation/json_annotation.dart';

part 'label.g.dart';

@immutable
@JsonSerializable()
class Label {
  final int id;

  final String name;

  @JsonKey(defaultValue: '')
  final String description;

  @JsonKey(
    fromJson: colorFromInt, 
    toJson: colorToInt)
  final Color color;

  Label({
    required this.id,
    required this.name,
    required this.description,
    required this.color
  });

  factory Label.fromJson(Map<String, dynamic> json) => _$LabelFromJson(json);
  Map<String, dynamic> toJson() => _$LabelToJson(this);

  static Color colorFromInt(int? value) => 
    value == null ? const Color(0xFF2196) : Color(value);

  static int? colorToInt(Color? color) =>
    color?.value;
}