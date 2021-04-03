import 'package:flutter/material.dart';
import 'package:json_annotation/json_annotation.dart';

part 'authToken.g.dart';

@immutable
@JsonSerializable()
class AuthToken {
  @JsonKey(name: 'token')
  final String value;
  @JsonKey(required: true)
  final DateTime expires;

  AuthToken({required this.value, required this.expires});

  bool get valid => DateTime.now().toUtc().isBefore(expires);

  factory AuthToken.fromJson(Map<String, dynamic> json) => _$AuthTokenFromJson(json);
  Map<String, dynamic> toJson() => _$AuthTokenToJson(this);
}