import 'package:equatable/equatable.dart';
import 'package:flutter/cupertino.dart';
import 'package:json_annotation/json_annotation.dart';

part 'todo.g.dart';

@immutable
@JsonSerializable()
class Todo extends Equatable {
  final int id;

  @JsonKey(defaultValue: 0)
  final int order;

  final bool done;

  final String title;

  @JsonKey(
    name: 'labels',
    defaultValue: []
  )
  final List<int> labelIds;

  @JsonKey(defaultValue:  '')
  final String description;

  Todo({
    required this.id,
    required this.order,
    required this.done,
    required this.title,
    required this.description,
    required this.labelIds
  });

  factory Todo.fromJson(Map<String, dynamic> json) => _$TodoFromJson(json);
  Map<String, dynamic> toJson() => _$TodoToJson(this);

  @override
  List<Object?> get props => [title, description, done];
}