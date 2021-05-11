import 'package:flutter/material.dart';

@immutable
class AppTab {
  final IconData tabIcon;
  final String tabText;
  final Widget page;
  final Widget? floatingActionButton;

  const AppTab({
    required this.tabIcon, 
    required this.tabText, 
    required this.page,
    this.floatingActionButton
  });
}