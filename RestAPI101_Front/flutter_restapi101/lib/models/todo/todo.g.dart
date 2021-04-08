// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'todo.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Todo _$TodoFromJson(Map<String, dynamic> json) {
  return Todo(
    id: json['id'] as int,
    done: json['done'] as bool,
    title: json['title'] as String,
    description: json['description'] as String? ?? '',
  );
}

Map<String, dynamic> _$TodoToJson(Todo instance) => <String, dynamic>{
      'id': instance.id,
      'done': instance.done,
      'title': instance.title,
      'description': instance.description,
    };
