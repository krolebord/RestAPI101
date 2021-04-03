import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:json_annotation/json_annotation.dart';

part 'authRegisterCredentials.g.dart';

@immutable
@JsonSerializable()
class AuthRegisterCredentials extends AuthCredentials {
  @JsonKey(required: true)
  final String username;

  AuthRegisterCredentials({required this.username, required String login, required String password}) :
    super(login: login, password: password);

  factory AuthRegisterCredentials.fromJson(Map<String, dynamic> json) => _$AuthRegisterCredentialsFromJson(json);

  Map<String, dynamic> toJson() => _$AuthRegisterCredentialsToJson(this);
}