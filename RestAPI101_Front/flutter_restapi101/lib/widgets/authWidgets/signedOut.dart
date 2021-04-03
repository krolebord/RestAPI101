import 'package:animations/animations.dart';
import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/authWidgets/loginForm.dart';
import 'package:flutter_restapi101/widgets/authWidgets/registerForm.dart';

class SignedOut extends StatefulWidget {
  @override
  _SignedOutState createState() => _SignedOutState();
}

class _SignedOutState extends State<SignedOut> {
  late Widget _currentForm;

  late Widget _loginForm;
  late Widget _registerForm;

  @override
  void initState() {
    super.initState();
    
    _loginForm = LoginForm(switchToRegister: _switchToRegister);
    _registerForm = RegisterForm(switchToLogin: _switchToLogin);

    _currentForm = _loginForm;
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: AnimatedSwitcher(
        duration: const Duration(milliseconds: 300),
        transitionBuilder: (child, animation) => FadeScaleTransition(child: child, animation: animation),
        child: _currentForm
      ),
    );
  }

  void _switchToRegister() => setState(() => _currentForm = _registerForm);

  void _switchToLogin() => setState(() => _currentForm = _loginForm);
}