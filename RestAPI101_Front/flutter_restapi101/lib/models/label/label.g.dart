// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'label.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Label _$LabelFromJson(Map<String, dynamic> json) {
  return Label(
    id: json['id'] as int,
    name: json['name'] as String,
    description: json['description'] as String? ?? '',
    color: Label.colorFromInt(json['color'] as int?),
  );
}

Map<String, dynamic> _$LabelToJson(Label instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'description': instance.description,
      'color': Label.colorToInt(instance.color),
    };
