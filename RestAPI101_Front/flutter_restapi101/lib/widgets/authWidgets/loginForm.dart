import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/models/auth/authCredentials.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authFormField.dart';

class LoginForm extends StatefulWidget {
  final void Function() onRegister;

  LoginForm({required this.onRegister, Key? key}) : super(key: key);

  @override
  _LoginFormState createState() => _LoginFormState();
}

class _LoginFormState extends State<LoginForm> {
  late final GlobalKey<FormState> _formKey;

  late final TextEditingController _loginController;
  late final TextEditingController _passwordController;

  @override
  void initState() {
    super.initState();

    _formKey = GlobalKey<FormState>();

    _loginController = TextEditingController();
    _passwordController = TextEditingController();
  }

  @override
  void dispose() {
    _loginController.dispose();
    _passwordController.dispose();

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
                      'Log In',
                      style: TextStyle(fontSize: 22, color: theme.buttonColor)
                    ),
                  ),
                  SizedBox(height: 12),
                  AuthFormField(
                    controller: _loginController,
                    labelText: 'Login',
                    validator: _validateLogin,
                    autofillHints: [AutofillHints.username],
                  ),
                  SizedBox(height: 8),
                  AuthFormField(
                    controller: _passwordController,
                    labelText: 'Password',
                    autofillHints: [AutofillHints.password],
                    obscureText: true,
                    validator: _validatePassword,
                    onFieldSubmitted: (_) => _submit(context),
                  ),
                  SizedBox(height: 12),
                  Row(
                    mainAxisSize: MainAxisSize.max,
                    mainAxisAlignment: MainAxisAlignment.end,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      TextButton(
                        onPressed: widget.onRegister,
                        child: Text('Register')
                      ),
                      SizedBox(width: 8),
                      ElevatedButton(
                        onPressed: () => _submit(context),
                        child: Text('Log In')
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
  }

  String? _validatePassword(String? password) {
    if(password == null || password.isEmpty)
      return 'Must be specified';
  }

  void _submit(BuildContext context) {
    if(!_formKey.currentState!.validate()) return;

    var credentials = AuthCredentials(
      login: _loginController.text,
      password: _passwordController.text
    );

    context.read<AuthCubit>().login(credentials);
  }
}
