import 'package:flutter/material.dart';
import 'package:json_annotation/json_annotation.dart';

part 'authCredentials.g.dart';

@immutable
@JsonSerializable()
class AuthCredentials {
  @JsonKey(required: true)
  final String login;

  @JsonKey(required: true)
  final String password;

  AuthCredentials({required this.login, required this.password});

  factory AuthCredentials.fromJson(Map<String, dynamic> json) => _$AuthCredentialsFromJson(json);
  Map<String, dynamic> toJson() => _$AuthCredentialsToJson(this);
}