// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authCredentials.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AuthCredentials _$AuthCredentialsFromJson(Map<String, dynamic> json) {
  $checkKeys(json, requiredKeys: const ['login', 'password']);
  return AuthCredentials(
    login: json['login'] as String,
    password: json['password'] as String,
  );
}

Map<String, dynamic> _$AuthCredentialsToJson(AuthCredentials instance) =>
    <String, dynamic>{
      'login': instance.login,
      'password': instance.password,
    };
