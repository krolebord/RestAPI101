// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'todoFilterDTO.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Map<String, dynamic> _$TodoFilterDTOToJson(TodoFilterDTO instance) {
  final val = <String, dynamic>{
    'includeMode': _$TodoIncludeModeEnumMap[instance.includeMode],
  };

  void writeNotNull(String key, dynamic value) {
    if (value != null) {
      val[key] = value;
    }
  }

  writeNotNull(
      'labelIds', TodoFilterDTO._labelIdsFromLabels(instance.labelIds));
  return val;
}

const _$TodoIncludeModeEnumMap = {
  TodoIncludeMode.All: 'All',
  TodoIncludeMode.Done: 'Done',
  TodoIncludeMode.Undone: 'Undone',
};
