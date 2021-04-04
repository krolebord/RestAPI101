import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';

@immutable
class UserAction extends Equatable {
  final IconData icon;
  final String label;
  final void Function(BuildContext context) onPressed;

  UserAction({required this.icon, required this.label, required this.onPressed});

  @override
  List<Object?> get props => [icon];
}