import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/models/auth/authToken.dart';
import 'package:flutter_restapi101/services/authPrefs/authPrefs.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthPrefsImplementation implements AuthPrefs {
  static const String loginKey = "Auth_Login";
  static const String passwordKey = "Auth_Password";
  
  static const String tokenKey = "Auth_Token";
  static const String tokenExpiresKey = "Auth_Token_Expires";

  late SharedPreferences _prefs;


  AuthCredentials? _credentials;

  AuthCredentials? get authCredentials => _credentials;

  set authCredentials(AuthCredentials? credentials) {
    _credentials = credentials;
    if(credentials != null) {
      _prefs.setString(loginKey, credentials.login);
      _prefs.setString(passwordKey, credentials.password);
    }
    else {
      _prefs.remove(loginKey);
      _prefs.remove(passwordKey);
    }
  }


  AuthToken? _token;

  AuthToken? get authToken => _token;

  set authToken(AuthToken? token) {
    _token = token;
    if(token != null) {
      _prefs.setString(tokenKey, token.value);
      _prefs.setInt(tokenExpiresKey, token.expires.millisecondsSinceEpoch);
    }
    else {
      _prefs.remove(tokenKey);
      _prefs.remove(tokenExpiresKey);
    }
  }


  Future<void> initialize() async { 
    _prefs = await SharedPreferences.getInstance();

    if(_prefs.containsKey(loginKey) && _prefs.containsKey(passwordKey)) {
      _credentials = AuthCredentials(
        login: _prefs.getString(loginKey)!, 
        password: _prefs.getString(passwordKey)!
      );
    }

    if(_prefs.containsKey(tokenKey) && _prefs.containsKey(tokenExpiresKey)) {
      _token = AuthToken(
        value: _prefs.getString(tokenKey)!, 
        expires: DateTime.fromMillisecondsSinceEpoch(_prefs.getInt(tokenExpiresKey)!)
      );
    }
  }
}