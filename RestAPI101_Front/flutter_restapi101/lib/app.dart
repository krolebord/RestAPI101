import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_restapi101/appRoutes.dart';
import 'package:flutter_restapi101/cubit/auth_cubit.dart';
import 'package:flutter_restapi101/services/auth/authService.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authenticated.dart';
import 'package:flutter_restapi101/widgets/authWidgets/authenticator.dart';
import 'package:flutter_restapi101/widgets/home/homePage.dart';
import 'package:flutter_restapi101/widgets/pages/accountPage.dart';
import 'package:flutter_restapi101/widgets/pages/settingsPage.dart';
import 'package:flutter_restapi101/widgets/themeProvider/themeProvider.dart';
import 'package:get_it/get_it.dart';
import 'package:vrouter/vrouter.dart';

class RestAPIApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ThemeProvider(
      initialTheme: ThemeData.dark(),
      builder: (context, theme) => VRouter(
        title: "RestAPI 101",
        theme: theme,
        initialUrl: AppRoutes.homeRoute,
        routes: [
          VNester(
            path: null, 
            widgetBuilder: _buildAppRoot,
            nestedRoutes: [
              _buildSignedOutGuard(),
              _buildSignedInGuard(),
              VRouteRedirector(
                path: ':_(.+)',
                redirectTo: AppRoutes.homeRoute
              ),
            ]
          ),
        ]
      )
    );
  }

  Widget _buildAppRoot(Widget child) {
    return BlocProvider<AuthCubit>(
      create: (_) => AuthCubit(),
      child: Builder(
        builder: (context) => BlocListener<AuthCubit, AuthState>(
          listener: (context, state) {
            if(state is AuthSignedIn)
              context.vRouter.push(AppRoutes.homeRoute);
            else if(state is AuthSignedOut)
              context.vRouter.push(AppRoutes.authRoute);
          },
          child: child,
        ),
      ),
    );
  }

  VRouteElement _buildSignedOutGuard() {
    return VGuard(
      beforeEnter: (vRedirector) async {
        if(GetIt.instance.get<AuthService>().currentUser != null)
          vRedirector.push(AppRoutes.homeRoute);
      },
      stackedRoutes: [
        VWidget(
          path: AppRoutes.authRoute, 
          widget: Builder(
            builder: (context) {
              return Authenticator(
                onSignedIn: (user) => context.vRouter.push(AppRoutes.homeRoute),
              );
            }
          )
        )
      ]
    );
  }

  VRouteElement _buildSignedInGuard() {
    return VGuard(
      beforeEnter: (vRedirector) async {
        if(GetIt.instance.get<AuthService>().currentUser == null)
          vRedirector.push(AppRoutes.authRoute);
      },
      stackedRoutes: [
        VNester(
          path: null, 
          widgetBuilder: (child) => Authenticated(child: child), 
          nestedRoutes: _buildAuthenticatedRoutes()
        )
      ]
    );
  }

  List<VRouteElement> _buildAuthenticatedRoutes() {
    return [
      VWidget(
        path: AppRoutes.homeRoute,
        widget: HomePage()
      ),
      VWidget(
        path: AppRoutes.accountRoute, 
        widget: AccountPage()
      ),
      VWidget(
        path: AppRoutes.settingsRoute, 
        widget: SettingsPage()
      )
    ];
  }
}