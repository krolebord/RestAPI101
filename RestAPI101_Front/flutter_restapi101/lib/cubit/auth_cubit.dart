import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/models/auth/authRegisterCredentials.dart';
import 'package:flutter_restapi101/models/auth/authUser.dart';
import 'package:flutter_restapi101/services/auth/authService.dart';
import 'package:get_it/get_it.dart';
import 'package:meta/meta.dart';

part 'auth_state.dart';

class AuthCubit extends Cubit<AuthState> {
  late final AuthService _authService;

  AuthCubit() : super(AuthSignedOut()) {
    _authService = GetIt.instance.get<AuthService>();

    _authService.userChanges.listen(onAuthUserChanged);
  }

  void onAuthUserChanged(AuthUser? user) {
    if(user == null) {
      emit(AuthSignedOut());
    } 
    else {
      emit(AuthSignedIn(user));
    }
  }

  void login(AuthCredentials credentials) => 
    _handleAuthAction(() => _authService.login(credentials));

  void register(AuthRegisterCredentials credentials) =>
    _handleAuthAction(() => _authService.register(credentials));

  void logout() =>
    _handleAuthAction(() => _authService.logout());

  void _handleAuthAction(Function func) async {
    emit(AuthLoading());

    try {
      await func.call();
    } on AuthServiceError catch(e) {
      emit(AuthError(e.errorMessage));
      emit(AuthSignedOut());
    } catch(e) {
      emit(AuthError(e.toString()));
      emit(AuthSignedOut());
    }
  }
}
