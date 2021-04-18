import 'dart:convert';
import 'dart:io';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/models/user/userChangePasswordDTO.dart';
import 'package:flutter_restapi101/services/authenticatedServiceMixin.dart';
import 'package:flutter_restapi101/services/user/userService.dart';

class UserServiceImplementation with AuthenticatedServiceMixin implements UserService {
  bool _loaded = false;
  late ApiUser _user;

  @override
  bool get loaded => _loaded;

  @override
  ApiUser get currentUser => _user;

  @override
  Future<void> loadUser() async {
    var response = await sendRequest(ApiRequests.getUser());

    if(response.statusCode != HttpStatus.ok)
      throw UserServiceError(message: 'Couldn\'t load user');

    _user = ApiUser.fromJson(json.decode(response.body));
    _loaded = true;
  }

  @override
  Future<void> changeUsername(String username) async {
    var request = ApiRequests.changeUsername();
    request.body = json.encode({
      'username': username
    });

    var response = await sendRequest(request, jsonContent: true);

    if(response.statusCode != HttpStatus.ok)
      throw UserServiceError(message: 'Couldn\'t change username');
  }

  @override
  Future<void> changePassword(UserChangePasswordDTO changePassword) async {
    var request = ApiRequests.changePassword();
    request.body = json.encode(changePassword.toJson());

    var response = await sendRequest(request, jsonContent: true);
    
    switch(response.statusCode) {
      case HttpStatus.ok: return;
      case HttpStatus.badRequest: 
        throw UserServiceError(message: 'Couldn\'t change password: wrong old password');
      default: 
        throw UserServiceError(message: 'Couldn\'t change password');
    }
  }

  @override
  Future<void> deleteUser() async {
    var response = await sendRequest(ApiRequests.deleteUser());

    if(response.statusCode != HttpStatus.ok)
      throw UserServiceError(message: 'Couldn\'t delete account');
  }
}