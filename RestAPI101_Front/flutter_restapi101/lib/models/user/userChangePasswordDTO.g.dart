// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'userChangePasswordDTO.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserChangePasswordDTO _$UserChangePasswordDTOFromJson(
    Map<String, dynamic> json) {
  return UserChangePasswordDTO(
    oldPassword: json['oldPassword'] as String,
    newPassword: json['newPassword'] as String,
  );
}

Map<String, dynamic> _$UserChangePasswordDTOToJson(
        UserChangePasswordDTO instance) =>
    <String, dynamic>{
      'oldPassword': instance.oldPassword,
      'newPassword': instance.newPassword,
    };
