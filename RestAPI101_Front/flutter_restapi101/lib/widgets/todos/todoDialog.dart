import 'package:flutter/material.dart';
import 'package:flutter_restapi101/models/todo/todo.dart';
import 'package:flutter_restapi101/models/todo/todoWriteDTO.dart';

class TodoDialog extends StatefulWidget {
  final Todo? todo;

  TodoDialog({this.todo});

  @override
  _TodoDialogState createState() => _TodoDialogState();
}

class _TodoDialogState extends State<TodoDialog> {
  late final TextEditingController _titleController;
  late final TextEditingController _descriptionController;

  final _formKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    _titleController = TextEditingController(text: widget.todo?.title);
    _descriptionController = TextEditingController(text: widget.todo?.description);
  }

  @override
  void dispose() {
    _titleController.dispose();
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
                  controller: _titleController,
                  decoration: InputDecoration(labelText: 'Title'),
                  validator: _validateTitle,
                ),
                TextField(
                  controller: _descriptionController,
                  decoration: InputDecoration(labelText: 'Description'),
                  minLines: 8,
                  maxLines: 16,
                ),
                SizedBox(height: 8),
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

  String? _validateTitle(String? title) {
    if(title == null || title.isEmpty)
      return 'Cannot be null';
  }

  void _handleCancel(BuildContext context) {
    Navigator.of(context).pop();
  }

  void _handleSave(BuildContext context) {
    if(_formKey.currentState!.validate()) {
      Navigator.of(context).pop<TodoWriteDTO>(
        TodoWriteDTO(
          title: _titleController.text, 
          description: _descriptionController.text,
          done: widget.todo?.done ?? false
        )
      );
    }
  }
}