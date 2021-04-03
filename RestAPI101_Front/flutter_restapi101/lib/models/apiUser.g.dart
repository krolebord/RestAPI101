// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'apiUser.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ApiUser _$ApiUserFromJson(Map<String, dynamic> json) {
  return ApiUser(
    login: json['login'] as String,
    username: json['username'] as String? ?? 'Unknown',
  );
}

Map<String, dynamic> _$ApiUserToJson(ApiUser instance) => <String, dynamic>{
      'login': instance.login,
      'username': instance.username,
    };
