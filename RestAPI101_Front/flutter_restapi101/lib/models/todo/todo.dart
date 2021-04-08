import 'package:equatable/equatable.dart';
import 'package:flutter/cupertino.dart';
import 'package:json_annotation/json_annotation.dart';

part 'todo.g.dart';

@immutable
@JsonSerializable()
class Todo extends Equatable {
  final int id;
  final bool done;
  final String title;

  @JsonKey(defaultValue:  '')
  final String description;

  Todo({
    required this.id,
    required this.done,
    required this.title,
    required this.description
  });

  factory Todo.fromJson(Map<String, dynamic> json) => _$TodoFromJson(json);
  Map<String, dynamic> toJson() => _$TodoToJson(this);

  @override
  List<Object?> get props => [title, description, done];
}