import 'package:flutter/material.dart';

class AuthFormField extends StatelessWidget {
  final TextEditingController? controller;
  final String? labelText;
  final bool obscureText;
  final String? Function(String? text)? validator;
  final void Function(String text)? onFieldSubmitted;
  final Iterable<String>? autofillHints;

  AuthFormField({
    this.controller,
    this.labelText,
    this.obscureText = false,
    this.validator,
    this.onFieldSubmitted,
    this.autofillHints
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: controller,
      decoration: InputDecoration(
        border: OutlineInputBorder(),
        labelText: labelText
      ),
      obscureText: obscureText,
      validator: validator,
      onFieldSubmitted: onFieldSubmitted,
      autofillHints: autofillHints,
    );
  }
}