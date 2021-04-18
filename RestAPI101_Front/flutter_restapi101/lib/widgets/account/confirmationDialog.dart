import 'package:flutter/material.dart';

class ConfirmationDialog extends StatelessWidget {
  final String title;
  final String? description;
  final Widget? confirmLabel;
  final Widget? cancelLabel;

  ConfirmationDialog({
    required this.title,
    this.confirmLabel,
    this.cancelLabel,
    this.description
  });

  @override
  Widget build(BuildContext context) {
    var theme = Theme.of(context);
    return Dialog(
      child: SizedBox(
          width: 300,
          height: 120,
          child: Padding(
            padding: const EdgeInsets.all(8),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  title,
                  style: theme.textTheme.headline6,
                ),
                if(description != null) Text(
                  description!,
                  style: TextStyle(color: theme.colorScheme.onSurface.withOpacity(0.7)),
                ),
                Row(
                  children: [
                    Expanded(
                      child: TextButton(
                        child: cancelLabel ?? Text('Cancel'),
                        onPressed: () => Navigator.of(context).pop(false),
                      )
                    ),
                    Expanded(
                      child: TextButton(
                        child: confirmLabel ?? Text('Ok'),
                        onPressed: () => Navigator.of(context).pop(true),
                      )
                    ),
                  ],
                )
              ],
            ),
          ),
      ),
    );
  }
}