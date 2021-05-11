import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class StatusBar<TCubit extends BlocBase<TState>, TState> extends StatelessWidget {
  final void Function(BuildContext context, TState state) listener;
  final Widget? leading;
  final Widget? title;
  final Widget? trailing;

  StatusBar({required this.listener, this.leading, this.title, this.trailing});
  
  @override
  Widget build(BuildContext context) {
    return BlocListener<TCubit, TState>(
      listener: listener,
      child: Container(
        height: 48,
        width: double.infinity,
        color: Theme.of(context).colorScheme.surface.withOpacity(0.7),
        child: Row(
          mainAxisSize: MainAxisSize.max,
          children: [
            Expanded(
              flex: 1,
              child: Align(
                alignment: Alignment.centerLeft,
                child: leading,
              )
            ),
            Expanded(
              flex: 1,
              child: Align(
                alignment: Alignment.center,
                child: title,
              )
            ),
            Expanded(
              flex: 1,
              child: Align(
                alignment: Alignment.centerRight,
                child: trailing,
              )
            )
          ],
        )
      ),
    );
  }
}