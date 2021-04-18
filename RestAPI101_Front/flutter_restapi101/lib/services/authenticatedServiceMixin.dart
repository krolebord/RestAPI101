import 'dart:io';

import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClient.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart';

mixin AuthenticatedServiceMixin {
  Future<Response> sendRequest(Request request, {bool jsonContent = false}) async {
    var client = await GetIt.instance.getAsync<AuthenticatedClient>();

    if(jsonContent) request.headers[HttpHeaders.contentTypeHeader] = 'application/json';

    var streamedResponse = await client.send(request);
    var response = await Response.fromStream(streamedResponse);

    client.close();

    return response;
  }
}