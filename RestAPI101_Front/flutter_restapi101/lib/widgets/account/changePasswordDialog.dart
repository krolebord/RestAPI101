import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/user/userChangePasswordDTO.dart';

class ChangePasswordDialog extends StatefulWidget {
  @override
  _ChangePasswordDialogState createState() => _ChangePasswordDialogState();
}

class _ChangePasswordDialogState extends State<ChangePasswordDialog> {
  final _formKey = GlobalKey<FormState>();

  late final TextEditingController _oldPasswordController;
  late final TextEditingController _newPasswordController;
  late final TextEditingController _confirmPasswordController;

  @override
  void initState() {
    super.initState();
    _oldPasswordController = TextEditingController();
    _newPasswordController = TextEditingController();
    _confirmPasswordController = TextEditingController();
  }

  @override
  void dispose() {
    _oldPasswordController.dispose();
    _newPasswordController.dispose();
    _confirmPasswordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: SizedBox(
        width: 500,
        height: 270,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisSize: MainAxisSize.max,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  'Change Username',
                  style: Theme.of(context).textTheme.headline6,
                ),
                TextField(
                  decoration: InputDecoration(labelText: 'Old password'),
                  controller: _oldPasswordController,
                  obscureText: true,
                ),
                TextFormField(
                  decoration: InputDecoration(labelText: 'New password'),
                  controller: _newPasswordController,
                  validator: _validateNewPassword,
                  obscureText: true,
                ),
                TextFormField(
                  decoration: InputDecoration(labelText: 'Confirm password'),
                  controller: _confirmPasswordController,
                  validator: _validateConfirmPassword,
                  obscureText: true,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    SizedBox(
                      width: 80,
                      child: TextButton(
                        child: Text('Cancel'),
                        onPressed: () => Navigator.of(context).pop(),
                      ),
                    ),
                    SizedBox(width: 8),
                    SizedBox(
                      width: 80,
                      child: ElevatedButton(
                        child: Text('Change'),
                        onPressed: _handleChange,
                      ),
                    ),
                  ],
                )
              ],
            ),
          ),
        ),
      ),
    );
  }

  String? _validateNewPassword(String? password) {
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
    var password = _newPasswordController.text;
    if(password != confirmPassword)
      return 'Passwords must match';
  }

  void _handleChange() {
    if(!_formKey.currentState!.validate())
      return;
    
    Navigator.of(context).pop<UserChangePasswordDTO>(
      UserChangePasswordDTO(
        oldPassword: _oldPasswordController.text, 
        newPassword: _newPasswordController.text
      )
    );
  }
}