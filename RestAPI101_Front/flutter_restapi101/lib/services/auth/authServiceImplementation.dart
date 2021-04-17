import 'dart:convert';
import 'dart:async';
import 'dart:io';
import 'package:flutter_restapi101/apiUrls.dart';
import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/models/auth/authRegisterCredentials.dart';
import 'package:flutter_restapi101/models/auth/authToken.dart';
import 'package:flutter_restapi101/models/auth/authUser.dart';
import 'package:flutter_restapi101/services/auth/authService.dart';
import 'package:flutter_restapi101/services/authPrefs/authPrefs.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;

class AuthServiceImplementation implements AuthService {
  final AuthPrefs _prefs;

  final StreamController<AuthUser?> _userChangedController;

  AuthUser? _currentUser;

  AuthServiceImplementation() :
    _userChangedController = StreamController<AuthUser?>.broadcast(),
    _prefs = GetIt.instance.get<AuthPrefs>() {
    _userChangedController.onListen = () => _userChangedController.add(_currentUser);
  }

  @override
  AuthUser? get currentUser => _currentUser;

  @override
  Stream<AuthUser?> get userChanges => _userChangedController.stream;

  @override
  void initialize() async {
    if(_prefs.authCredentials != null && _prefs.authToken != null) {
      var user = AuthUser(
        credentials: _prefs.authCredentials!,
        token: _prefs.authToken!
      );
      _changeUser(user);
    }
    else {
      _changeUser(null);
    }
  }

  @override
  Future<void> login(AuthCredentials credentials) async {
    var user = AuthUser(
      credentials: credentials,
      token: await _getToken(credentials)
    );

    _prefs.authCredentials = user.credentials;
    _prefs.authToken = user.token;

    _changeUser(user);
  }

  @override
  Future<void> register(AuthRegisterCredentials credentials) async {
    var request = ApiRequests.register();
    request.headers[HttpHeaders.contentTypeHeader] = 'application/json';
    request.body = json.encode(credentials.toJson());

    var response = await _sendRequest(request);

    if(response.statusCode == HttpStatus.ok) {
      await login(credentials);
    }
    else {
      throw AuthServiceError(
        responseCode: response.statusCode, 
        errorMessage: (jsonDecode(response.body) as Map<String,dynamic>).values.first ?? "Unexpected error occurred"
      );
    }
  }

  @override
  Future<void> logout() async {
    _prefs.authCredentials = null;
    _prefs.authToken = null;
    _changeUser(null);
  }

  @override
  Future<void> updateToken() async {
    if(_currentUser == null)
      throw AuthServiceError.unauthorized();

    var token = await _getToken(_currentUser!.credentials);
    
    _prefs.authToken = token;
    _currentUser!.token = token;
  }

  Future<AuthToken> _getToken(AuthCredentials credentials) async {
    var request = ApiRequests.login();
    request.headers[HttpHeaders.contentTypeHeader] = 'application/json';
    request.body = json.encode(credentials.toJson());

    var response = await _sendRequest(request);

    if(response.statusCode == HttpStatus.ok) {
      Map<String, dynamic> responseBody = jsonDecode(response.body);
      return AuthToken.fromJson(responseBody);
    }
    else {
      throw AuthServiceError(
        responseCode: response.statusCode, 
        errorMessage: (jsonDecode(response.body) as Map<String,dynamic>).values.first ?? "Unexpected error occurred"
      );
    }
  }

  void _changeUser(AuthUser? user) {
    _currentUser = user;

    if(_userChangedController.hasListener)
      _userChangedController.add(_currentUser);
  }

  Future<http.Response> _sendRequest(http.Request request) async {
    var client = http.Client();

    var streamedResponse = await client.send(request);
    var response = await http.Response.fromStream(streamedResponse);

    client.close();

    return response;
  }
}