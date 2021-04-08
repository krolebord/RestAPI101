import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:json_annotation/json_annotation.dart';

part 'todoWriteDTO.g.dart';

@immutable
@JsonSerializable()
class TodoWriteDTO extends Equatable {
  final bool done;
  final String title;

  @JsonKey(defaultValue:  '')
  final String description;

  TodoWriteDTO({
    required this.title, 
    required this.description, 
    required this.done
  });

  TodoWriteDTO.fromTodo(Todo todo) : 
    title = todo.title,
    description = todo.description,
    done = todo.done;

  factory TodoWriteDTO.fromJson(Map<String, dynamic> json) => _$TodoWriteDTOFromJson(json);
  Map<String, dynamic> toJson() => _$TodoWriteDTOToJson(this);

  @override
  List<Object?> get props => [done, title, description]; 
}