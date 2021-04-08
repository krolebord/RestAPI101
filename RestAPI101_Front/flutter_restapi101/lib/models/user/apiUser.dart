import 'package:flutter/foundation.dart';
import 'package:json_annotation/json_annotation.dart';

part 'apiUser.g.dart';

@immutable
@JsonSerializable()
class ApiUser {
  final String login;
  
  @JsonKey(defaultValue: 'Unknown')
  final String username;

  ApiUser({required this.login, required this.username});

  factory ApiUser.fromJson(Map<String, dynamic> json) => _$ApiUserFromJson(json);
  Map<String, dynamic> toJson() => _$ApiUserToJson(this);
}