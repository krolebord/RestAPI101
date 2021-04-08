// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'todoWriteDTO.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

TodoWriteDTO _$TodoWriteDTOFromJson(Map<String, dynamic> json) {
  return TodoWriteDTO(
    title: json['title'] as String,
    description: json['description'] as String? ?? '',
    done: json['done'] as bool,
  );
}

Map<String, dynamic> _$TodoWriteDTOToJson(TodoWriteDTO instance) =>
    <String, dynamic>{
      'done': instance.done,
      'title': instance.title,
      'description': instance.description,
    };
