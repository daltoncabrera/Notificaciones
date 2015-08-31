'use strict';
app.factory('aplicacionesService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var aplicacionesServiceFactory = {};

    var _getAplicaciones = function () {

        return $http.get(serviceBase + 'api/Aplicaciones').then(function (results) {
            return results;
        });
    };
    

    aplicacionesServiceFactory
   .GetTipos = function () {
       return $http.get(serviceBase + 'api/Aplicaciones/GetTiposAplicaciones').then(function (results) {
           return results;
       });
   };

    var _getDataSource = function () {

        return new kendo.data.DataSource({
            transport: {
                read: function (e) {
                    $http.get(serviceBase + 'api/Aplicaciones/Get')
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
                    $http.post(serviceBase + 'api/Aplicaciones/Update/', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Aplicacion Actualizada!');
                    }).error(function (error) {
                        e.error(error);
                        toastr.success('Error Actualizando Aplicacion');
                    })
                },
                destroy: function (e) {
                    $http.delete(serviceBase + 'api/Aplicaciones/Delete/?id='+ e.data.id).success(function (results) {
                        e.success(results);
                    }).error(function (error) {
                        e.error(error);
                        toastr.error('Error Eliminando Aplicacion!');
                    })
                },
                create: function (e) {
                    $http.post(serviceBase + 'api/Aplicaciones/Create', e.data).success(function (results) {
                        e.success(results);
                        toastr.success('Aplicacion Creada!');
                    }).error(function (error) {
                        e.error(error)
                        toastr.error('Error Creando Aplicacion!');
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
                        id: {type:"integer", editable: false, nullable: true },
                        secret: { nullable: true, validation: { required: false } },
                        name: { validation: { required: true } },
                        description: { validation: { required: true } },
                        applicationType: { type: "number", validation: { required: true} },
                        active: { type: "boolean", validation: { required: true } },
                        refreshKey: { type: "boolean", validation: { required: false } },
                        imgName: { validation: { required: false } },
                        allowedOrigin: { validation: { required: true } }
                    }
                }
            }
        });
    };

    var _getGridAplicaciones = function () {
        var grid = {
            dataSource: _getDataSource(),
            pageable: true,
            toolbar: ["create"],
            columns: [
                { field: "name", title: "Nombre", width: "100px" },
                { field: "description", title: "Descripcion", width: "150px" },
                {
                    field: "secret", title: "Key", width: "340px", enabled: false,
                },
                { field: "allowedOrigin", title: "Origenes", width: "120px" },
                {
                    field: "applicationType",
                    title: "Tipo",
                    width: "60px",
                    editor: TipoDropDownEditor
                },
                { field: "imgName", title: "Imagen", width: "100px"}, 
                { field: "active", title: "Activa", width: "60px" },
                { field: "refreshKey", title: "Actualizar Key", width: "60px" , hidden:true},
                { command: [{ name: "edit", text: "Editar" }, { name: "destroy", text: "eliminar" }], title: "&nbsp;", width: "180px" }],
            editable: { mode: "popup", confirmation: "Seguro desea eliminar la aplicacion?", },
            edit: function (e) {
                $('*[data-container-for="secret"]').remove();
                $("label[for='secret']").parent().remove();
            }
        }

        return grid;
    };

    function TipoDropDownEditor(container, options) {
        //$('<input required data-text-field="value" data-value-field="key" data-bind="value:' + options.field + '"/>')
        //<select    data-bind="value:usuarios"></select>
        $('<input required  kendo-combo-box k-options="AplicacionTipoOptions" data-bind="value:' + options.field + '"/>')
            .appendTo(container);
    }
    

    aplicacionesServiceFactory.getAplicaciones = _getAplicaciones;
    aplicacionesServiceFactory.GetGridAplicaciones = _getGridAplicaciones;

    return aplicacionesServiceFactory;

}]);