import 'package:flutter/cupertino.dart';
import 'package:json_annotation/json_annotation.dart';

part 'userChangePasswordDTO.g.dart';

@immutable
@JsonSerializable()
class UserChangePasswordDTO {
  final String oldPassword;
  final String newPassword;

  UserChangePasswordDTO({required this.oldPassword, required this.newPassword});

  factory UserChangePasswordDTO.fromJson(Map<String, dynamic> json) => _$UserChangePasswordDTOFromJson(json);
  Map<String, dynamic> toJson() => _$UserChangePasswordDTOToJson(this);
}