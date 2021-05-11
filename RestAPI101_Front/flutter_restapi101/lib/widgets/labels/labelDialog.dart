import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/label/label.dart';
import 'package:flutter_restapi101/models/label/labelWriteDTO.dart';
import 'package:flutter_restapi101/services/colorGenerator/ColorGenerator.dart';

class LabelDialog extends StatefulWidget {
  final Label? label;

  LabelDialog({this.label});

  @override
  _LabelDialogState createState() => _LabelDialogState();
}

class _LabelDialogState extends State<LabelDialog> {
  late final TextEditingController _nameController;
  late final TextEditingController _descriptionController;

  final _formKey = GlobalKey<FormState>();

  late Color _color;

  @override
  void initState() {
    super.initState();
    _nameController = TextEditingController(text: widget.label?.name);
    _descriptionController = TextEditingController(text: widget.label?.description);

    _color = widget.label?.color ?? ColorUtils.randomColor();
  }

  @override
  void dispose() {
    _nameController.dispose();
    _descriptionController.dispose();

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: SizedBox(
        width: 500,
        child: Form(
          key: _formKey,
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                TextFormField(
                  controller: _nameController,
                  decoration: InputDecoration(labelText: 'Name'),
                  validator: _validateName
                ),
                TextField(
                  controller: _descriptionController,
                  decoration: InputDecoration(labelText: 'Description'),
                ),
                Padding(
                  padding: const EdgeInsets.symmetric(vertical: 8.0),
                  child: Row(
                    children: [
                      Expanded(
                        flex: 2,
                        child: Align(
                          alignment: Alignment.centerLeft,
                          child: SelectableText.rich(
                            TextSpan(
                              text: 'Color: ',
                              style: TextStyle(color: Theme.of(context).colorScheme.onSurface.withOpacity(0.7)),
                              children: [
                                TextSpan(
                                  text: '#${(_color.value << 8 >> 8).toRadixString(16)}',
                                  style: TextStyle(fontWeight: FontWeight.bold)
                                )
                              ]
                            ),
                          )
                        ),
                      ),
                      Expanded(
                        child: Align(
                          alignment: Alignment.centerRight,
                          child: SizedBox(
                            width: 120,
                            child: ElevatedButton(
                              child: Text(
                                'Random',
                                style: TextStyle(color: ColorUtils.textColorOn(_color)),
                              ),
                              style: ElevatedButton.styleFrom(primary: _color),
                              onPressed: generateColor,
                            ),
                          ),
                        )
                      ),
                    ],
                  ),
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    ElevatedButton(
                      child: Text('Cancel'),
                      style: ButtonStyle(
                        backgroundColor: MaterialStateProperty.resolveWith<Color>(
                          (states) => Theme.of(context).errorColor)
                      ),
                      onPressed: () => _handleCancel(context),
                    ),
                    SizedBox(width: 4),
                    ElevatedButton(
                      child: Text('Save'),
                      onPressed: () => _handleSave(context),
                    )
                  ],
                )
              ],
            ),
          ),
        ),
      )
    );
  }

  String? _validateName(String? name) {
    if(name == null || name.isEmpty)
      return 'Cannot be empty';
  }

  void generateColor() {
    setState(() {
      _color = ColorUtils.randomColor();
    });
  }

  void _handleCancel(BuildContext context) {
    Navigator.of(context).pop();
  }

  void _handleSave(BuildContext context) {
    if(_formKey.currentState!.validate()) {
      Navigator.of(context).pop<LabelWriteDTO>(
        LabelWriteDTO(
          name: _nameController.text,
          description: _descriptionController.text,
          color: _color
        )
      );
    }
  }
}
