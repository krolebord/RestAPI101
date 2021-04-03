import 'package:http/http.dart';

abstract class AuthenticatedClient {
  Future<StreamedResponse> send(BaseRequest request);
}