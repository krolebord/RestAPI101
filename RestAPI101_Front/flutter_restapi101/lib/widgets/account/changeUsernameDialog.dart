import 'package:flutter/material.dart';

class ChangeUsernameDialog extends StatefulWidget {
  final String username;

  ChangeUsernameDialog({required this.username});

  @override
  _ChangeUsernameDialogState createState() => _ChangeUsernameDialogState();
}

class _ChangeUsernameDialogState extends State<ChangeUsernameDialog> {
  final _formFieldKey = GlobalKey<FormFieldState>();

  late final TextEditingController _usernameController;

  @override
  void initState() {
    super.initState();
    _usernameController = TextEditingController(text: widget.username);
  }

  @override
  void dispose() {
    _usernameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: SizedBox(
        width: 500,
        height: 150,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
          child: Column(
            mainAxisSize: MainAxisSize.max,
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Change Username',
                style: Theme.of(context).textTheme.headline6,
              ),
              TextFormField(
                key: _formFieldKey,
                controller: _usernameController,
                validator: _validateUsername,
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
    );
  }

  String? _validateUsername(String? username) {
    if(username == null || username.isEmpty)
      return 'Must be specified';
  }

  void _handleChange() {
    if(!_formFieldKey.currentState!.validate())
      return;
    
    Navigator.of(context).pop<String>(_usernameController.text);
  }
}