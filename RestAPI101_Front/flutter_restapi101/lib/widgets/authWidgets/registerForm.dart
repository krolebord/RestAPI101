import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/models/auth/authRegisterCredentials.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authFormField.dart';

class RegisterForm extends StatefulWidget {
  final void Function() onLogin;

  RegisterForm({required this.onLogin, Key? key}) : super(key: key);

  @override
  _RegisterFormState createState() => _RegisterFormState();
}

class _RegisterFormState extends State<RegisterForm> {
  late final GlobalKey<FormState> _formKey;

  late final TextEditingController _usernameController;
  late final TextEditingController _loginController;
  late final TextEditingController _passwordController;
  late final TextEditingController _confirmPasswordController;

  @override
  void initState() {
    super.initState();

    _formKey = GlobalKey<FormState>();

    _usernameController = TextEditingController();
    _loginController = TextEditingController();
    _passwordController = TextEditingController();
    _confirmPasswordController = TextEditingController();
  }

  @override
  void dispose() {
    _usernameController.dispose();
    _loginController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    return Form(
      key: _formKey,
      child: AutofillGroup(
        child: Card(
          elevation: 8,
          child: Padding(
            padding: const EdgeInsets.all(8),
            child: Container(
              width: 350,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Align(
                    alignment: Alignment.centerLeft,
                    child: Text(
                      'Register',
                      style: TextStyle(fontSize: 22, color: theme.buttonColor)
                    ),
                  ),
                  SizedBox(height: 12),
                  AuthFormField(
                    controller: _loginController,
                    labelText: 'Login',
                    validator: _validateLogin
                  ),
                  SizedBox(height: 8),
                  AuthFormField(
                    controller: _usernameController,
                    labelText: 'Username',
                    validator: _validateUsername,
                  ),
                  SizedBox(height: 8),
                  AuthFormField(
                    controller: _passwordController,
                    labelText: 'Password',
                    obscureText: true,
                    validator: _validatePassword,
                  ),
                  SizedBox(height: 8),
                  AuthFormField(
                    controller: _confirmPasswordController,
                    labelText: 'Confirm password',
                    obscureText: true,
                    validator: _validateConfirmPassword,
                    onFieldSubmitted: (_) => _submit(context),
                  ),
                  SizedBox(height: 12),
                  Row(
                    mainAxisSize: MainAxisSize.max,
                    mainAxisAlignment: MainAxisAlignment.end,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      TextButton(
                        onPressed: widget.onLogin,
                        child: Text('Log In')
                      ),
                      SizedBox(width: 8),
                      ElevatedButton(
                        onPressed: () => _submit(context),
                        child: Text('Register')
                      )
                    ],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  String? _validateLogin(String? login) {
    if(login == null || login.isEmpty)
      return 'Must be specified';
    if(login.length < 6)
      return 'Must be at least 6 characters long';
    if(login.length > 64)
      return 'Must be at maximum 64 characters long';
  }

  String? _validateUsername(String? username) {
    if(username == null || username.isEmpty)
      return 'Must be specified';
  }

  String? _validatePassword(String? password) {
    if(password == null || password.isEmpty)
      return 'Must be specified';
    if(password.length < 8)
      return 'Must be at least 8 characters long';
    if(password.length > 32)
      return 'Must be at maximum 32 characters long';
  }

  String? _validateConfirmPassword(String? confirmPassword) {
    if(confirmPassword == null || confirmPassword.isEmpty)
      return 'Must be specified';
    var password = _passwordController.text;
    if(password != confirmPassword)
      return 'Passwords must match';
  }

  void _submit(BuildContext context) {
    if(!_formKey.currentState!.validate()) return;

    var credentials = AuthRegisterCredentials(
      login: _loginController.text,
      username: _usernameController.text,
      password: _passwordController.text
    );

    context.read<AuthCubit>().register(credentials);
  }
}