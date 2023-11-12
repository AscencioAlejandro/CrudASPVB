
@Code

'ViewData("Title") = "Index"

End Code



<div>
    <h2>Inscripciones</h2>
    <hr />

    <dl class="dl-horizontal" id="blockAdd">
        <div class="form-inline">

            <div id="notifycmbDocente" class="form-group ">
                <label for="cmbDocente">Seleccione un Docente (*)</label>
                <select class="form-control select2 select2-hidden-accessible" style="width: 100%;" id="cmbDocente" tabindex="-1" aria-hidden="true">
                    <option selected="selected" value="0" disabled="disabled">Seleccione una Opcion </option>
                </select>
            </div>

            <div id="notifycmbCurso" class="form-group">
                <label for="cmbCurso">Seleccione un Curso (*)</label>
                <select class="form-control select2 select2-hidden-accessible" style="width: 100%; " id="cmbCurso" tabindex="-1" aria-hidden="true">
                    <option selected="selected" value="0" disabled="disabled">Seleccione una Opcion </option>
                </select>
            </div>

            <div id="notifycmbCurso" class="form-group">
                <div class="buttonspaceTop"></div>
                <a class="btn btn-success text-white" id="btnAction"><span id="lbltitleaction"></span></a>
                <a class="btn btn-success text-white" id="btnClear">Nuevo Registro</a>
            </div>

        </div>

    </dl>

    <dl class="dl-horizontal" id="blockList">
        <div class="bs-example" data-example-id="bordered-table" id="tblPrincipal">
            <table class="table-futurista ">
                <thead>
                    <tr>

                        <th>Curso Asignado</th>
                        <th>Nombre de Docente</th>
                        <th>Fecha de Inscripcion</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody id="tblshowinscripciones">
                </tbody>
            </table>
        </div>


    </dl>

    <div class="panel panel-primary" id="banner">
        <div class="panel-heading">
            <h3 class="panel-title">Notificacion de Sistema</h3>
        </div>
        <div class="panel-body">
            No hay datos en la Tabla
        </div>
    </div>

</div>
<p>

    @*@Html.ActionLink("Back to List", "Index")*@
</p>

<script>
    $(document).ready(function () {

        $('.select2').select2();

        let dataGlobal;
        let dataSanitizated = 0;
        let titlemodal = "Guardar Inscripcion";
        $("#lbltitleaction").text(titlemodal);
        $("#btnClear").hide();
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        })

        fill_tbl();
        fill_cmb_docentes();
        fill_cmb_cursos();

        function reloadComponents() {

            clean();
            fill_tbl();
        }

        function fill_cmb_cursos() {

            $.ajax({
                type: 'GET',
                url: '/Curso/getCursos',
                dataType: 'json',
                async: false,
                success: function (data) {

                    if (data.length !== 0) {
                        $("#cmbCurso").prop("disabled", false).trigger("change.select2");
                        var selectElement = $("#cmbCurso");

                        $.each(data, function (index, item) {
                            selectElement.append($('<option>', {
                                value: item.id_curso,
                                text: item.nombre
                            }));
                        });

                    }
                    else {
                   
                        $("#cmbCurso").prop("disabled", true).trigger("change.select2");
                      
                    }
                                       

                },
                error: function (xhr, status, error) {
                    console.error('Error al obtener los datos:', error);
                    console.log(xhr.responseText);
                }
            });
        }

        function fill_cmb_docentes() {


            $.ajax({
                type: 'GET',
                url: '/Docente/getDocentes',
                dataType: 'json',
                async: false,
                success: function (data) {                  

                    if (data.length !== 0) {
                        $("#cmbDocente").prop("disabled", false).trigger("change.select2");
                        var selectElement = $("#cmbDocente");
                        $.each(data, function (index, item) {
                            selectElement.append($('<option>', {
                                value: item.id_docente,
                                text: item.nombreCompleto
                            }));
                        });
                    }
                    else {

                        $("#cmbDocente").prop("disabled", true).trigger("change.select2");
                    }

                },
                error: function (xhr, status, error) {
                    console.error('Error al obtener los datos:', error);
                    console.log(xhr.responseText);
                }
            });

        }

        function fill_tbl() {
            $.ajax({
                type: 'GET',
                url: '/Inscripcion/getInscripciones',
                dataType: 'json',
                async: false,
                success: function (data) {
                    dataGlobal = data;
                    let fila = "";

                    if (data.length !== 0) {                       
                     
                        $("#tblPrincipal").show();
                        $("#banner").hide();

                        console.log(data);

                        data.forEach(function (obj) {
                            fila += "<tr>" +
                                "<td>" + obj.descripcion_curso + "</td>" +
                                "<td>" + obj.descripcion_docente + "</td>" +
                                "<td>" + parseDateFromAspNetJson(obj.fecha) + "</td>" +
                                "<td>" +
                                "<a class='btn btn-primary text-white btnupdateaction buttonspace' data-idrecord='" + obj.id_inscripcion + "'>Editar</a>" +
                                "<a class='btn btn-danger text-white btndeleteaction' data-idrecord='" + obj.id_inscripcion + "'>Eliminar</a>" +
                                "</td>" +
                                "</tr>";
                        });

                        $("#tblshowinscripciones").html(fila);

                    } else {
                        $("#banner").show();
                        $("#tblPrincipal").hide();
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error al obtener los datos:', error);
                    console.log(xhr.responseText);
                }
            });
        }
             
        function showRecord(id) {
            $("#btnClear").show();
            let dataSelected = dataGlobal.find(data => data.id_inscripcion === id);
            dataSanitizated = dataSelected.id_inscripcion;
            if (dataSelected != undefined) {
                    
                $("#cmbCurso").val(dataSelected.id_curso);
                $("#cmbDocente").val(dataSelected.id_docente);

                $("#cmbCurso, #cmbDocente").trigger("change");

            } else {
                return null;
            }        

        }

        function validarSelects(selects) {
            var camposValidos = true;

            $.each(selects, function (index, selectId) {
                var valorSelect = $('#' + selectId).val();
                console.log(selectId);
                if (!valorSelect || valorSelect.trim() === '0') {
                    toastr.error("Los campos (*) son obligatorios", "Notificacion de Sistema");
                    $("#" + "notify" + selectId).addClass("select-vacio");
                    camposValidos = false;
                }
                else {
                    $("#" + "notify" + selectId).removeClass("select-vacio");
                }
            });

            return camposValidos;
        }

        function parseDateFromAspNetJson(dateString) {
      
            var milliseconds = parseInt(dateString.replace("/Date(", "").replace(")/", ""), 10);
            var date = new Date(milliseconds);
            return date.toLocaleString();
        }

        $("#btnClear").click(function () {

            clean();

        });

        function clean() {

            let titlemodal = "Guardar Inscripcion";
            $("#lbltitleaction").text(titlemodal);
            $("#btnClear").hide();

            $("#cmbCurso").val(0);
            $("#cmbDocente").val(0);

            $("#cmbCurso, #cmbDocente").trigger("change");

        }
           
        $('#btnAction').click(function () {

            var camposAValidar = ['cmbDocente', 'cmbCurso'];
            var resultValidation = validarSelects(camposAValidar);

            if (resultValidation != false) {

                let values = {
                    id_inscripcion: dataSanitizated,
                    id_docente: $("#cmbDocente").val(),
                    id_curso: $("#cmbCurso").val()
                }

                $.ajax({
                    type: 'POST',
                    url: '/Inscripcion/alterInscripcion',
                    contentType: 'application/json',
                    data: JSON.stringify(values),
                    success: function (response) {  


                        swalWithBootstrapButtons.fire(
                            'Notificacion',
                            'Accion Realizada con Exito!',
                            'success'
                        );

                        reloadComponents();

                    },
                    error: function (error) {


                    }

                });
            }
         

        });

        $(document).on("click", ".btnupdateaction", function () {

            titlemodal = "Actualizar Inscripcion";
            $("#lbltitleaction").text(titlemodal);
            let id = $(this).data("idrecord");
            showRecord(id);

        });

        $(document).on("click", ".btndeleteaction", function () {

            let id = $(this).data("idrecord");

            swalWithBootstrapButtons.fire({
                title: "Estas Seguro?",
                text: "Si ejecutas esta accion no podras recuperar este registro!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Si, Borrar"
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({
                        type: 'POST',
                        url: '/Inscripcion/deleteInscripcion/' + id,
                        contentType: 'application/json',
                        data: {},
                        success: function (response) {

                            swalWithBootstrapButtons.fire(
                                'Notificacion',
                                'Accion Realizada con Exito!',
                                'success'
                            );

                            reloadComponents();

                        },
                        error: function (error) {


                        }
                    });
                }
            });

        });


    });
</script>
