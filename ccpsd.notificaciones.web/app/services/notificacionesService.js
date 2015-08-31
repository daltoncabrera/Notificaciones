'use strict';
app.factory('notificacionesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var NotificacionesServiceFactory = {};

    NotificacionesServiceFactory
    .GetTiposNotificacionesVigenciasAplicaciones = function () {
        return $http.get(serviceBase + 'api/Notificaciones/GetTiposNotificacionesVigenciasAplicaciones').then(function (results) {
            return results;
        });
    };

    NotificacionesServiceFactory
    .GetDomainUsers = function () {
        return $http.get(serviceBase + 'api/Notificaciones/GetDomainUsers').then(function (results) {
            return results;
        });
    };


    var _getDataSource = function () {

        return new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    $http.get(serviceBase + 'api/Notificaciones/Get')
                        .success(function (results) {

                            e.success(results);
                        })
                        .error(function (error) {
                            e.error(error);
                        })

                }
                ,
                update: function (e) {

                    $http.post(serviceBase + 'api/Notificaciones/Update/', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Notificacion Actualizado!');
                    }).error(function (error) {
                        e.error(error);
                        toastr.error('Error Actualizando Notificacion');
                    })
                },
                destroy: function (e) {
                    $http.delete(serviceBase + 'api/Notificaciones/Delete/?id=' + e.data.notificacionId).success(function (results) {
                        e.success(results);
                    }).error(function (error) {
                        e.error(error);
                        toastr.error('Error Eliminando Notificacion!');
                    })
                },
                create: function (e) {

                    $http.post(serviceBase + 'api/Notificaciones/Create', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Notificacion Creado!');
                    }).error(function (error) {
                        e.error(error)
                        toastr.error('Error Creando Notificacion!');
                    });
                },

                parameterMap: function (options, operation) {
                }
            },
            batch: false,
            pageSize: 20,
            schema: {
                model: {
                    id: "notificacionId",
                    fields: {
                        notificacionId: { editable: false, nullable: false },
                        aplicacionId: { editable: true, nullable: true },
                        aplicacion: { type: "text", editable: false, nullable: true },
                        titulo: { validation: { required: true } },
                        nota: { type: "text", validation: { required: true } },
                        usuarios: { type: "text", validation: { required: true } },
                        idTipo: { editable: true, nullable: false },
                        tipo: { type: "text", editable: true, nullable: true },
                        link: { editable: true, validation: { required: false } },
                        idTipoVigencia: { validation: { required: true } },
                        vigencia: { type: "integer", validation: { required: true } },
                        fechaCreacion: { type: "date", validation: { required: false } },
                        activo: { type: "boolean", validation: { required: false } }
                    }
                }
            }
        });
    };

    NotificacionesServiceFactory
    .GetGridNotificaciones = function () {
        var grid = {
            dataSource: _getDataSource(),
            pageable: true,
            toolbar: ["create"],
            columns: [
                {
                    command: [{ name: "edit", text: "Editar" }, { name: "destroy", text: "eliminar" }],
                    title: "&nbsp;", width: "170px"
                },
                { field: "notificacionId", title: "Id", width: "80px", hidden: true },
                { field: "aplicacion", title: "App", width: "80px" },
                { field: "titulo", title: "Titulo", width: "150px" },
                { field: "tipo", title: "Tipo", width: "100px" },
                { field: "vigencia", title: "Vigencia", width: "80px" },
                { field: "link", title: "Link", width: "150px" },
                { field: "usuarios", title: "Usuarios", width: "150px", hidden: false, template: function (rr) {
                    if(rr && rr.usuarios)
                        return rr.usuarios.join(', ');
                }
                },
                { field: "nota", title: "Mensaje", width: "150px", hidden: false },
                { field: "fechaCreacion", title: "Fecha", width: "80px", format: "{0:yyyy/MM/dd}" },
                { field: "activo", title: "Activo", width: "60px" },
                ],
            editable: {
                mode: "popup",
                confirmation: "Seguro desea eliminar el Notificacion?",
                template: kendo.template($("#template").html()),
                window: {
                    title: "Creacion/Edicion notificaciones",
                    //width:"600px"
                }
            },
            edit: function (e) {
                $('*[data-container-for="joinDate"]').remove();
                $("label[for='joinDate']").parent().remove();

            }
        }

        return grid;
    };


    NotificacionesServiceFactory

    return NotificacionesServiceFactory;

}]);