import 'package:flutter/material.dart';
import 'package:flutter_restapi101/widgets/home/adaptiveLayout.dart';
import 'package:flutter_restapi101/widgets/home/appTab.dart';

class HomePage extends StatefulWidget {
  final List<AppTab> tabs;

  HomePage({required this.tabs});

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
            children: List.generate(
              widget.tabs.length, 
              (index) {
                var tab = widget.tabs[index];
                return ListTile(
                  leading: Icon(tab.tabIcon),
                  title: Text(tab.tabText),
                  selected: index == _tabController.index,
                  onTap: () => _tabController.animateTo(index),
                );
              },
              growable: false
            ),
          ),
        ),
      ),
      rightChild: TabBarView(
        controller: _tabController,
        children: widget.tabs.map((tab) => tab.page).toList(growable: false),
      ),
      floatingActionButton: widget.tabs[_tabController.index].floatingActionButton,
    );
  }
}