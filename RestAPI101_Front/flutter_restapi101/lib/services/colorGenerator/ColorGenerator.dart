import 'dart:math';

import 'package:flutter/material.dart';

class ColorUtils {
  ColorUtils._();

  static Color randomColor({int? seed}) {
    var rng = Random(seed);
    return Color.fromRGBO(rng.nextInt(256), rng.nextInt(256), rng.nextInt(256), 1);
  }

  static Color textColorOn(Color color) =>
     color.computeLuminance() > 0.5 ? Colors.black : Colors.white;
}
