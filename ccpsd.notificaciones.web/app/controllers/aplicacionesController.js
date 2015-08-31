'use strict';
app.controller('aplicacionesController', ['$scope', 'authService', 'aplicacionesService', function ($scope, authService, aplicacionesService) {
    $scope.authentication = authService.authentication;
    $scope.aplicacionesGridOpts = aplicacionesService.GetGridAplicaciones();
    aplicacionesService.GetTipos().then(function (result) {
        var data = result.data;

        $scope.AplicacionTipoOptions = {
            dataSource: data,
            dataTextField: "value",
            dataValueField: "key",
            valuePrimitive: true
        };
    });
    
}]);