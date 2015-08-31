'use strict';
app.factory('usuariosService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var AccountServiceFactory = {};

    var _getAccount = function () {

        return $http.get(serviceBase + 'api/Account').then(function (results) {
            return results;
        });
    };

    var _getDataSource = function () {

        return new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    $http.get(serviceBase + 'api/Account/Users')
                        .success(function (results) {
                            e.success(results);
                        })
                        .error(function (error) {
                            e.error(error);
                        })

                }
                ,
                update: function (e) {
                    console.log(e);
                    $http.post(serviceBase + 'api/Account/Update/', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Usuario Actualizado!');
                    }).error(function (error) {
                        e.error(error);
                        toastr.error('Error Actualizando Usuario');
                    })
                },
                destroy: function (e) {
                    $http.delete(serviceBase + 'api/Account/Delete/?id=' + e.data.id).success(function (results) {
                        e.success(results);
                    }).error(function (error) {
                        e.error(error);
                        toastr.error('Error Eliminando Usuario!');
                    })
                },
                create: function (e) {
                    $http.post(serviceBase + 'api/Account/Create', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Usuario Creado!');
                    }).error(function (error) {
                        e.error(error)
                        toastr.error('Error Creando Usuario!');
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            batch: false,
            pageSize: 20,
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { type: "integer", editable: false, nullable: true },
                        userName: {editable:true, validation: { required: true } },
                        email: { validation: { required: true } },
                        firstName: { validation: { required: true } },
                        lastName: { validation: { required: true } },
                        password: {
                            
                            validation:
                                {
                                    required: false,
                                    custom: function (input, lo) {                                        
                                        if (input.is("[name=password]")) {                                            
                                            input.attr("data-custom-msg", "Password No coinciden");
                                            console.log(input.val())
                                            console.log($("div[name=confirmPassword]"))
                                            return input.val() === $("input[name=confirmPassword]").val();
                                        }
                                        return true;
                                    }
                                }
                        },
                        confirmPassword: { type: "password", validation: { required: false } },
                        active: { type: "boolean", validation: { required: false } },
                        emailConfirmed: { type: "boolean", validation: { required: false } },
                        joinDate: { type: "date", validation: { required: false } },
                        imgName: { validation: { required: false } }
                    }
                }
            }
        });
    };

    var _getGridAccount = function () {
        var grid = {
            dataSource: _getDataSource(),
            pageable: true,
            toolbar: ["create"],
            columns: [
                { field: "userName", title: "Usuario", width: "100px" },
                { field: "firstName", title: "Primer Nombre", width: "150px" },
                { field: "lastName", title: "Segundo Nombre", width: "150px" },
                { field: "email", title: "Correo", width: "150px" },
                { field: "password", title: "Contraseña", width: "60px", hidden: true },
                { field: "confirmPassword", title: "Confirmar Contraseña", width: "60px", hidden: true },
                { field: "joinDate", title: "Fecha", width: "80px", format: "{0:yyyy/MM/dd}" },
                { field: "imgName", title: "Imagen", width: "100px" },
                { field: "active", title: "Activo", width: "60px" },
                { field: "emailConfirmed", title: "Correo  Confirmado", width: "60px" },
                { command: [{ name: "edit", text: "Editar" }, { name: "destroy", text: "eliminar" }], title: "&nbsp;", width: "180px" }],
            editable: { mode: "popup", confirmation: "Seguro desea eliminar el usuario?", },
            edit: function (e) {
                $('*[data-container-for="joinDate"]').remove();
                $("label[for='joinDate']").parent().remove();
            }
        }

        return grid;
    };

    AccountServiceFactory.GetUsuarios = _getAccount;
    AccountServiceFactory.GetGridUsuarios = _getGridAccount;

    return AccountServiceFactory;

}]);