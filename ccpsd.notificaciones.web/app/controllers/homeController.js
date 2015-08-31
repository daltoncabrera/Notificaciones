'use strict';
app.controller('homeController', ['$scope', 'authService', function ($scope, authService) {
    $scope.authentication = authService.authentication;
    //console.log($scope.authentication);
}]);