import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClient.dart';
import 'package:http/http.dart' as http;

class AuthenticatedClientImplementation implements AuthenticatedClient {
  final String token;
  final http.Client _client;

  AuthenticatedClientImplementation({required this.token}) :
    _client = http.Client();

  @override
  Future<http.StreamedResponse> send(http.BaseRequest request) {
    request.headers['Authorization'] = 'Bearer $token';
    return _client.send(request);
  }

  @override
  void close() => _client.close();
}