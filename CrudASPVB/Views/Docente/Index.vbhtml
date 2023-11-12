@Code
'ViewData("Title") = "Index"
End Code



<div>
    <h2>Docentes</h2>
    <hr />
    <dl class="dl-horizontal">
        <a class="btn btn-success text-white" id="btnSaveModal">Nuevo Registro</a>
        <div class="bs-example" data-example-id="bordered-table" id="tblPrincipal">
            <table class="table-futurista ">
                <thead>
                    <tr>

                        <th>Nombre</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody id="tblshowdocentes">
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

<div class="modal fade" tabindex="-1" role="dialog" id="myModal" data-target="#myModal">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><span id="titlemodal"> </span> Registro</h4>
            </div>
            <div class="modal-body">
                <form id="myForm">
                    <div id="notifytxtNombre" class="form-group">
                        <label for="txtNombre">(*) Nombres :</label>
                        <input type="text" class="form-control" name="nombre" id="txtNombre" placeholder="">
                    </div>
                    <div id="notifytxtApellido" class="form-group">
                        <label for="txtApellido">(*) Apellidos :</label>
                        <input type="text" class="form-control" name="apellido" id="txtApellido" placeholder="">
                    </div>
                </form>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="btncancel">Close</button>
                <button type="" class="btn btn-primary" id="btnAction">Save changes</button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

<script>
    $(document).ready(function () {

        let dataGlobal;
        let dataSanitizated = 0;
        let titlemodal;

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: true
        })

        function validarCampos(campos) {

            //if (!Array.isArray(campos)) {
            //    console.error('El argumento campos no es un array válido.');
            //    return false;
            //}

            //if (!campos.every(function (campo) { return typeof campo === 'string'; })) {
            //    console.error('Al menos un elemento en el array no es una cadena de texto.');
            //    return false;
            //}

            //if (!campos.every(function (campo) { return $('#' + campo).length > 0; })) {
            //    console.error('Al menos un elemento en el array no es un ID de campo válido.');
            //    return false;
            //}

            var camposValidos = true;

            $.each(campos, function (index, campoId) {
                var valorCampo = $('#' + campoId).val();

                if (valorCampo.trim() === '') {                   
                    toastr.error("Los campos (*) son obligatorios", "Notificacion de Sistema");
                    $('#' + 'notify' + campoId).addClass('has-error');
                    camposValidos = false;
                }
            });

            return camposValidos;
        }

        fill_tbl();

        function reloadComponents() {
            dataGlobal = "";
            dataSanitizated = "";
            $("#txtNombre").val("");
            $("#txtApellido").val("");
            fill_tbl();
        }

   
        function fill_tbl() {
            $.ajax({
                type: 'GET',
                url: '/Docente/getDocentes',
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
                                "<td>" + obj.nombre + " " + obj.apellido + "</td>" +
                                "<td>" +
                                "<a class='btn btn-primary text-white btnupdateaction buttonspace' data-idrecord='" + obj.id_docente + "'>Editar</a>" +
                                "<a class='btn btn-danger text-white btndeleteaction' data-idrecord='" + obj.id_docente + "'>Eliminar</a>" +
                                "</td>" +
                                "</tr>";
                        });

                        $("#tblshowdocentes").html(fila);
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

            let cursoSeleccionado = dataGlobal.find(docente => docente.id_docente === id);
            if (cursoSeleccionado != undefined) {

                dataSanitizated = cursoSeleccionado.id_docente;
                titlemodal = "Editar";
                $('#myModal').modal('show');
                $('#titlemodal').text(titlemodal);
                $("#txtNombre").val(cursoSeleccionado.nombre);
                $("#txtApellido").val(cursoSeleccionado.apellido);

            }
            else {
                return null;
            }

        }
                

        $('#btnAction').click(function () {

            var camposAValidar = ['txtNombre', 'txtApellido'];
            var resultValidation = validarCampos(camposAValidar);

            if (resultValidation != false) {

                let values = {
                    id_docente: dataSanitizated,
                    nombre : $("#txtNombre").val(),
                    apellido: $("#txtApellido").val()                   
                }

                
            $.ajax({
                type: 'POST',
                url: '/Docente/alterDocente',
                contentType: 'application/json',
                data: JSON.stringify(values),
                success: function (response) {
                    $('#myModal').modal('hide');

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

        $('#btnSaveModal').click(function () {
            titlemodal = "Guardar"
            $('#titlemodal').text(titlemodal);
            $('#myModal').modal('show');

           
        });

        $('#myModal').on('hidden.bs.modal', function (e) {
            $("#txtNombre").val("");
            $("#txtApellido").val("");
            $("#notifytxtNombre").removeClass("has-error");
            $("#notifytxtApellido").removeClass("has-error");
        });

        $('#myModal').on('hide.bs.modal', function (e) {
            $("#txtNombre").val("");
            $("#txtApellido").val("");
            $("#notifytxtNombre").removeClass("has-error");
            $("#notifytxtApellido").removeClass("has-error");
        })

        $(document).on("click", ".btnupdateaction", function () {

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
                        url: '/Docente/deleteDocente/' + id,
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
                        error: function (jqXHR, textStatus, errorThrown) {

                            toastr.error(jqXHR.responseText, "Mensaje de Sistema");
                        }
                    });
                }
            });

        });


    });
</script>
