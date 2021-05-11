// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'labelWriteDTO.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

LabelWriteDTO _$LabelWriteDTOFromJson(Map<String, dynamic> json) {
  return LabelWriteDTO(
    name: json['name'] as String,
    description: json['description'] as String?,
    color: Label.colorFromInt(json['color'] as int?),
  );
}

Map<String, dynamic> _$LabelWriteDTOToJson(LabelWriteDTO instance) =>
    <String, dynamic>{
      'name': instance.name,
      'description': instance.description,
      'color': Label.colorToInt(instance.color),
    };
