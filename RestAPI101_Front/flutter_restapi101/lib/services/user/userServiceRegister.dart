import 'package:flutter_restapi101/services/user/userService.dart';
import 'package:flutter_restapi101/services/user/userServiceImplementation.dart';
import 'package:get_it/get_it.dart';

extension UserServiceRegister on GetIt {
  void registerUserService() {
    this.registerSingleton<UserService>(UserServiceImplementation());
  }
}