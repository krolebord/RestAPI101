import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class SyncStatus {
  SyncStatus._();

  static const Widget syncing = const Tooltip(
    message: "Syncing...",
    child: Icon(Icons.sync),
  );

  static const Widget synced = const Tooltip(
    message: "Synced",
    child: Icon(Icons.cloud_done),
  );

  static const Widget error = const Tooltip(
    message: "Couldn\'t sync",
    child: Icon(Icons.cloud_off),
  );
}