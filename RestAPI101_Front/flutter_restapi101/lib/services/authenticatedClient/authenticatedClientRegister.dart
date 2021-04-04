import 'package:flutter_restapi101/services/auth/authService.dart';
import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClient.dart';
import 'package:flutter_restapi101/services/authenticatedClient/authenticatedClientImplementation.dart';
import 'package:get_it/get_it.dart';

extension AuthenticatedClientRegister on GetIt {
  void registerAuthenticatedClient() {
    this.registerFactoryAsync<AuthenticatedClient>(
      () async {
        var authService = this.get<AuthService>();

        if(authService.currentUser == null)
          throw AuthServiceError.unauthorized();
        
        var user = authService.currentUser!;

        if(!user.token.valid)
          await authService.updateToken();

        return AuthenticatedClientImplementation(token: user.token.value);
      }
    );
  }
}