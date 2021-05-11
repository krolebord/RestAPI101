import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/common/adaptiveLayout.dart';
import 'package:flutter_restapi101/widgets/common/appTab.dart';
import 'package:flutter_restapi101/widgets/labels/labelsFAB.dart';
import 'package:flutter_restapi101/widgets/labels/labelsPage.dart';
import 'package:flutter_restapi101/widgets/todos/todosFAB.dart';
import 'package:flutter_restapi101/widgets/todos/todosPage.dart';

class HomePage extends StatefulWidget {
  final List<AppTab> tabs = [
    AppTab(
        tabIcon: Icons.label,
        tabText: "Labels",
        page: LabelsPage(),
        floatingActionButton: LabelsFAB()
    ),
    AppTab(
        tabIcon: Icons.list_alt,
        tabText: "Todos",
        page: TodosPage(),
        floatingActionButton: TodosFAB()
    ),
    AppTab(
        tabIcon: Icons.sticky_note_2,
        tabText: "Notes",
        page: Center(child: Text('Work in progress')),
        floatingActionButton: FloatingActionButton(
          onPressed: () {},
          child: Icon(Icons.note_add),
        )
    ),
    AppTab(
      tabIcon: Icons.settings_ethernet,
      tabText: "Commands",
      page: Center(child: Text('Work in progress')),
    )
  ];

  HomePage({Key? key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> with SingleTickerProviderStateMixin {
  late final TabController _tabController;

  @override
  initState() {
    super.initState();
    _tabController = TabController(length: widget.tabs.length, vsync: this, initialIndex: 0);
    _tabController.addListener(_handleTabChange);
  }

  @override
  void dispose() {
    _tabController.removeListener(_handleTabChange);
    _tabController.dispose();
    super.dispose();
  }

  void _handleTabChange() {
    if(_tabController.indexIsChanging)
      setState(() {}); 
  }

  @override
  Widget build(BuildContext context) {
    return AdaptiveLayout(
      widthBreakpoint: 700,
      leftChild: Container(
        child: Material(
          elevation: 2,
          color: Theme.of(context).colorScheme.surface,
          child: ListView(
            children: [ 
              for(var i = 0; i < widget.tabs.length; i++) 
                _buildLeftBarTab(i),
            ]
          )
        ),
      ),
      rightChild: TabBarView(
        controller: _tabController,
        physics: NeverScrollableScrollPhysics(),
        children: widget.tabs.map((tab) => tab.page).toList(growable: false),
      ),
      floatingActionButton: widget.tabs[_tabController.index].floatingActionButton,
    );
  }

  Widget _buildLeftBarTab(int index) {
    var tab = widget.tabs[index];
    return ListTile(
      leading: Icon(tab.tabIcon),
      title: Text(tab.tabText),
      selected: index == _tabController.index,
      onTap: () => _tabController.animateTo(index),
    );
  }
}