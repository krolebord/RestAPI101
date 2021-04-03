import 'package:flutter_restapi101/models/apiUser.dart';
import 'package:flutter_restapi101/services/Auth/authService.dart';
import 'package:flutter_restapi101/services/User/userService.dart';
import 'package:get_it/get_it.dart';

class UserServiceImplementation implements UserService {
  final AuthService _authService;
  
  late ApiUser _user;

  UserServiceImplementation() :
    _authService = GetIt.instance.get<AuthService>();

  @override
  ApiUser get currentUser => _user;
  
  @override
  Future<void> initialize() {
    // TODO: implement initialize
    throw UnimplementedError();
  }

  @override
  Future<void> updateUser() {
    // TODO: implement updateUser
    throw UnimplementedError();
  }

  @override
  Future<void> changeUsername(String username) {
    // TODO: implement changeUsername
    throw UnimplementedError();
  }

  @override
  Future<void> deleteUser() {
    // TODO: implement deleteUser
    throw UnimplementedError();
  }
}