import 'dart:io';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_restapi101/app.dart';
import 'package:flutter_restapi101/developmentHttpOverrides.dart';
import 'package:flutter_restapi101/registerServices.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await registerServices();

  if(!kReleaseMode) {
    HttpOverrides.global = new DevelopmentHttpOverrides();
  }

  runApp(RestAPIApp());
}
