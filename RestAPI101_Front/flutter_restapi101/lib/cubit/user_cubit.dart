import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_restapi101/models/apiUser.dart';
import 'package:flutter_restapi101/services/user/userService.dart';
import 'package:get_it/get_it.dart';
import 'package:meta/meta.dart';

part 'user_state.dart';

class UserCubit extends Cubit<UserState> {
  final UserService _userService;

  UserCubit() : 
    _userService = GetIt.instance.get<UserService>(),
    super(UserInitial()) {
    update();
  }

  void update() async {
    if(_userService.loaded)
      emit(UserLoaded(user: _userService.currentUser));

    try {
      await _userService.updateUser();
      emit(UserLoaded(user: _userService.currentUser));
    } on UserServiceError catch(e) {
      emit(UserError(errorMessage: e.message));
    }
  }
}
