import 'package:json_annotation/json_annotation.dart';

part 'userData.g.dart';

@JsonSerializable()
class UserData {
  final String login;
  final String username;
  UserData({required this.login, required this.username});

  factory UserData.fromJson(Map<String, dynamic> json) => _$UserDataFromJson(json);
  Map<String, dynamic> toJson() => _$UserDataToJson(this);
}