import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/models/user/userChangePasswordDTO.dart';

abstract class UserService {
  ApiUser get currentUser;

  bool get loaded;

  Future<void> loadUser();
  
  Future<void> changeUsername(String username);

  Future<void> changePassword(UserChangePasswordDTO changePassword);
  
  Future<void> deleteUser();
}

class UserServiceError {
  String message;

  UserServiceError({required this.message});
}