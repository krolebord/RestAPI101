import 'package:flutter_restapi101/models/user/apiUser.dart';

abstract class UserService {
  ApiUser get currentUser;

  bool get loaded;

  Future<void> updateUser();
  
  Future<void> changeUsername(String username);
  
  Future<void> deleteUser();
}

class UserServiceError {
  String message;

  UserServiceError({required this.message});
}