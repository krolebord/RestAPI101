// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'userData.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserData _$UserDataFromJson(Map<String, dynamic> json) {
  return UserData(
    login: json['login'] as String,
    username: json['username'] as String,
  );
}

Map<String, dynamic> _$UserDataToJson(UserData instance) => <String, dynamic>{
      'login': instance.login,
      'username': instance.username,
    };
