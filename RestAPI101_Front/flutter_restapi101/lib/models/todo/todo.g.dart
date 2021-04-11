// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'todo.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Todo _$TodoFromJson(Map<String, dynamic> json) {
  return Todo(
    id: json['id'] as int,
    order: json['order'] as int? ?? 0,
    done: json['done'] as bool,
    title: json['title'] as String,
    description: json['description'] as String? ?? '',
  );
}

Map<String, dynamic> _$TodoToJson(Todo instance) => <String, dynamic>{
      'id': instance.id,
      'order': instance.order,
      'done': instance.done,
      'title': instance.title,
      'description': instance.description,
    };
