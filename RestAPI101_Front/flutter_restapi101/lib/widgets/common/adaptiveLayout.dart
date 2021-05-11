import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/userActions/userAppbar.dart';

class AdaptiveLayout extends StatelessWidget {
  final int widthBreakpoint;
  final Widget leftChild;
  final Widget rightChild;
  final Widget? floatingActionButton;

  final Key _fabKey = UniqueKey();

  AdaptiveLayout({
    required this.widthBreakpoint, 
    required this.leftChild, 
    required this.rightChild,
    this.floatingActionButton
  });

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints)  {
        bool buildNarrow = constraints.maxWidth < widthBreakpoint;

        Widget? drawer;
        if(buildNarrow) 
          drawer = SizedBox(
            width: 250,
            child: Drawer(child: leftChild)
          );

        Widget body = buildNarrow 
          ? rightChild
          : Column(
            children: <Widget>[
              Expanded(
                child: Row(
                  mainAxisSize: MainAxisSize.max,
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: <Widget>[
                    Container(
                      width: 250,
                      child: leftChild
                    ),
                    Expanded(
                      child: rightChild
                    ),
                  ],
                ),
              ),
            ],
          );

        Widget? fab;
        if(floatingActionButton != null)
          fab = Container(
            key: _fabKey,
            child: floatingActionButton
          );

        return Scaffold(
          appBar: UserAppBar(),
          drawer: drawer,
          body: body,
          floatingActionButton: fab
        );
      }
    );
  }
}