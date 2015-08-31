'use strict';
app.controller('notificacionesController', ['$scope', 'authService', 'notificacionesService', function ($scope, authService, notificacionesService) {
    $scope.authentication = authService.authentication;
    $scope.NotificacionesGridOpts = notificacionesService.GetGridNotificaciones();
    notificacionesService.GetTiposNotificacionesVigenciasAplicaciones().then(function (result) {
        var data = result.data;
     
        $scope.AplicacionOptions = {           
            dataSource: data.aplicaciones,
            dataTextField: "value",
            dataValueField: "key",
            valuePrimitive: true
        };

        $scope.TipoNotificacionOptions = {
            dataSource: data.tipoNotificacion,
            dataTextField: "value",
            dataValueField: "key",
            valuePrimitive: true
        };

        $scope.TipoVigenciaOptions = {
            dataSource: data.tipoVigencia,
            dataTextField: "value",
            dataValueField: "key",
            valuePrimitive: true
        };

        

        $scope.getApplication = function (app) {
            console.log(app);
        };

    });

    notificacionesService.GetDomainUsers().then(function (result) {
        $scope.DomainUsersOptions = {
            dataSource: result.data,
            valuePrimitive: true
        };
    });
}]);