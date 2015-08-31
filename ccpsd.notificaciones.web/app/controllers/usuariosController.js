'use strict';
app.controller('usuariosController', ['$scope', 'authService', 'usuariosService', function ($scope, authService, usuariosService) {
    $scope.authentication = authService.authentication;
    $scope.usuariosGridOpts = usuariosService.GetGridUsuarios();
}]);