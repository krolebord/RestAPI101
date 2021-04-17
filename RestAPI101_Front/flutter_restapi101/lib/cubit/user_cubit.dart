import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter_restapi101/models/user/apiUser.dart';
import 'package:flutter_restapi101/models/user/userChangePasswordDTO.dart';
import 'package:flutter_restapi101/services/user/userService.dart';
import 'package:get_it/get_it.dart';
import 'package:meta/meta.dart';

part 'user_state.dart';

class UserCubit extends Cubit<UserState> {
  final UserService _userService;

  UserCubit() : 
    _userService = GetIt.instance.get<UserService>(),
    super(UserLoading()) {
    load();
  }

  void load() async {
    emit(UserLoading());

    try {
      await _userService.loadUser();
      emit(UserLoaded(user: _userService.currentUser));
    } on UserServiceError catch(e) {
      emit(UserError(errorMessage: e.message));
    }
  }

  Future<void> changeUsername(String username)  async {
    emit(UserLoading());
    
    try {
      await _userService.changeUsername(username);
    } on UserServiceError catch(e) {
      emit(UserError(errorMessage: e.message));
    }

    load();
  }

  Future<bool> changePassword(UserChangePasswordDTO changePassword) async {
    emit(UserLoading());
    
    try {
      await _userService.changePassword(changePassword);
      return true;
    } on UserServiceError catch(e) {
      emit(UserError(errorMessage: e.message));
      return false;
    }
  }

  Future<void> deleteUser() async {
    emit(UserLoading());
    _userService.deleteUser();
  }
}
