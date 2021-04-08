import 'dart:convert';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClient.dart';
import 'package:flutter_restapi101/services/user/userService.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;

class UserServiceImplementation implements UserService {
  bool _loaded = false;
  late ApiUser _user;

  @override
  bool get loaded => _loaded;

  @override
  ApiUser get currentUser => _user;

  @override
  Future<void> updateUser() async {
    var client = await GetIt.instance.getAsync<AuthenticatedClient>();

    var request = http.Request('GET', APIURLs.getUser());

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    client.close();

    _user = ApiUser.fromJson(json.decode(response.body));
    _loaded = true;
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