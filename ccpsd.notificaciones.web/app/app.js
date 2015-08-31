
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngAnimate', 'kendo.directives']);

app.config(function ($routeProvider) {
   
   
    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html",
        title: "Inicio"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html",
        title: "Login"

    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html",
        title: "Registro"
    });

    $routeProvider.when("/usuarios", {
        controller: "usuariosController",
        templateUrl: "/app/views/usuarios.html",
        title: "Usuarios"
    });

    $routeProvider.when("/aplicaciones", {
        controller: "aplicacionesController",
        templateUrl: "/app/views/aplicaciones.html",
        title: "Aplicaciones"
    });

    $routeProvider.when("/notificaciones", {
        controller: "notificacionesController",
        templateUrl: "/app/views/notificaciones.html",
        title: "Notificaciones"
    });



    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = location.protocol + '//' + location.host + '/'; // 'http://localhost:26264/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'webnotificaciones'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.run(['$location', '$rootScope', function ($location, $rootScope) {
    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        if (current.$$route)
          $rootScope.title = current.$$route.title;
    });
}]);


