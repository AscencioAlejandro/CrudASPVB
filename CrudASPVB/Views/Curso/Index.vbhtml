@*@ModelType List(Of CrudASPVB.Curso)*@
@Imports System.Data
@Code
'ViewData("Title") = "Index"
'Dim dataCurso As List(Of Curso) = Model
End Code



<div>
    <h2>Cursos</h2>
    <hr />

    <dl class="dl-horizontal" >
        <a class="btn btn-success text-white" id="btnSaveModal">Nuevo Registro</a>
        <div class="bs-example" data-example-id="bordered-table" id="tblPrincipal">
            <table class="table-futurista ">
                <thead>
                    <tr>
                        <th>Descripcion</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody id="tblshowcursos">
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


<div class="modal fade" tabindex="-1" role="dialog" id="myModal">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><span id="titlemodal"> </span> Registro</h4>
            </div>
            <div class="modal-body">
                <div id="notifytxtDescripcion" class="form-group">
                    <label for="txtDescripcion">(*) Descripcion :</label>
                    <input type="text" class="form-control" id="txtDescripcion" placeholder="Introduce una Descripcion" required> 
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="btncancel">Close</button>
                <button type="button" class="btn btn-primary" id="btnAction">Save changes</button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

<script>
    $(document).ready(function () {

        let dataGlobal;
        let dataSanitizated = 0;
        let titlemodal;
        $("#tblPrincipal").hide();
        $("#banner").hide();

        const swalWithBootstrapButtons = Swal.mixin({
                 customClass: {
                    confirmButton: 'btn btn-success',
                     cancelButton: 'btn btn-danger'
                 },
                 buttonsStyling: true
             })

        fill_tbl();

        function reloadComponents() {
            dataGlobal = "";
            dataSanitizated = "";
            $("#txtDescripcion").val("");
            fill_tbl();            
        }

        function fill_tbl() {
            $.ajax({
                type: 'GET',
                url: '/Curso/getCursos',
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
                                "<td>" + obj.nombre + "</td>" +
                                "<td>" +
                                "<a class='btn btn-primary text-white btnupdateaction buttonspace' data-idrecord='" + obj.id_curso + "'>Editar</a>" +
                                "<a class='btn btn-danger text-white btndeleteaction' data-idrecord='" + obj.id_curso + "'>Eliminar</a>" +
                                "</td>" +
                                "</tr>";
                        });

                        $("#tblshowcursos").html(fila);
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

            let cursoSeleccionado = dataGlobal.find(curso => curso.id_curso === id);
            if (cursoSeleccionado != undefined) {

                dataSanitizated = cursoSeleccionado.id_curso;
                titlemodal = "Editar";
                $('#myModal').modal('show');
                $('#titlemodal').text(titlemodal);
                $("#txtDescripcion").val(cursoSeleccionado.nombre);

            }
            else {
                return null;
            }

        }

        function parseFromAspNetJson(data) {

            var result = data.replace(/\\/g, "");
            return result;
        }

        $('#btnAction').click(function () {

            var objData = {
                id_curso: dataSanitizated,
                nombre: $("#txtDescripcion").val()
            }

            if ($("#txtDescripcion").val() != "") {

                $.ajax({
                    type: 'POST',
                    url: '/Curso/alterCurso',
                    contentType: 'application/json',
                    data: JSON.stringify(objData),
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
                                        
                        toastr.error("Error en Sistema", "Mensaje de Sistema");
          
                    }

                });
            }
            else {

                toastr["error"]("Los campos (*) son obligatorios", "Notificacion de Sistema")
                $("#notifytxtDescripcion").addClass("has-error");
                
            }

        });

        $('#btnSaveModal').click(function () {
            titlemodal = "Guardar"
            $('#myModal').modal('show');
            $('#titlemodal').text(titlemodal);
        });

        $('#myModal').on('hidden.bs.modal', function (e) {
            $("#txtDescripcion").val("");
            $("#notifytxtDescripcion").removeClass("has-error");
        });

        $('#myModal').on('hide.bs.modal', function (e) {
            $("#txtDescripcion").val("");
            $("#notifytxtDescripcion").removeClass("has-error");
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
                        url: '/Curso/deleteCurso/' + id,
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

