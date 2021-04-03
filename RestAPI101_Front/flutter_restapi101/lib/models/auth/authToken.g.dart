// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'authToken.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AuthToken _$AuthTokenFromJson(Map<String, dynamic> json) {
  $checkKeys(json, requiredKeys: const ['expires']);
  return AuthToken(
    value: json['token'] as String,
    expires: DateTime.parse(json['expires'] as String),
  );
}

Map<String, dynamic> _$AuthTokenToJson(AuthToken instance) => <String, dynamic>{
      'token': instance.value,
      'expires': instance.expires.toIso8601String(),
    };
