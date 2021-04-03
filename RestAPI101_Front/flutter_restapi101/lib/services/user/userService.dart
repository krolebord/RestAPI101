import 'package:flutter_restapi101/models/apiUser.dart';

abstract class UserService {
  ApiUser get currentUser;

  Future<void> initialize();

  Future<void> updateUser();
  
  Future<void> changeUsername(String username);
  
  Future<void> deleteUser();
}

class UserServiceError {
  String message;

  UserServiceError({required this.message});
}