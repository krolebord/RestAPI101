// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authRegisterCredentials.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AuthRegisterCredentials _$AuthRegisterCredentialsFromJson(
    Map<String, dynamic> json) {
  $checkKeys(json, requiredKeys: const ['login', 'password', 'username']);
  return AuthRegisterCredentials(
    username: json['username'] as String,
    login: json['login'] as String,
    password: json['password'] as String,
  );
}

Map<String, dynamic> _$AuthRegisterCredentialsToJson(
        AuthRegisterCredentials instance) =>
    <String, dynamic>{
      'login': instance.login,
      'password': instance.password,
      'username': instance.username,
    };
