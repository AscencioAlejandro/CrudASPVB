@ModelType List(Of CrudASPVB.InscripcionDetalle)
@Imports System.Data
@Code
    ViewData("Title") = "Index"
    Dim dataInscripcion As List(Of InscripcionDetalle) = Model
End Code

<h2>Index</h2>

<div>
    <h4>Docente</h4>
    <hr />
    <dl class="dl-horizontal">
        <a class="btn btn-success text-white" href="@Url.Action("Guardar")">Nuevo Registro</a>
        <div class="bs-example" data-example-id="bordered-table">
            <table class="table-futurista ">
                <thead>
                    <tr>

                        <th>Curso Asignado</th>
                        <th>Nombre de Docente</th>
                        <th>Fecha de Inscripcion</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    @For Each val As InscripcionDetalle In dataInscripcion
                        @<tr>
    <td>@val.descripcion_curso</td>
    <td>@val.descripcion_docente</td>
    <td>@val.fecha</td>
    <td>
        <a class="btn btn-primary text-white" href="@Url.Action("Editar", New With {.id = val.id_inscripcion})">Editar</a>
        <a class="btn btn-danger text-white" href="@Url.Action("Eliminar", New With {.id = val.id_inscripcion})">Eliminar</a>
    </td>
</tr>
                    Next

                </tbody>
            </table>
        </div>


    </dl>
</div>
<p>

    @Html.ActionLink("Back to List", "Index")
</p>

<style>
    /* Estilos generales para la tabla */
    .table-futurista {
        border-collapse: collapse;
        width: 100%;
        border: 1px solid #ccc;
        font-family: Arial, sans-serif;
    }

        /* Estilo para las celdas encabezado */
        .table-futurista th {
            background-color: #333;
            color: #fff;
            padding: 12px;
            text-align: left;
        }

        /* Estilo para las celdas de datos */
        .table-futurista td {
            border: 1px solid #ccc;
            padding: 8px;
        }

        /* Efecto de transición al pasar el cursor sobre las filas */
        .table-futurista tr:hover {
            background-color: #f2f2f2;
            transition: background-color 0.3s;
        }

        /* Estilo para las celdas de encabezado cuando se desplaza la página */
        .table-futurista th.sticky {
            position: sticky;
            top: 0;
            z-index: 2;
        }

        /* Estilo para las celdas de datos con fondo oscuro */
        .table-futurista .dark-cell {
            background-color: #444;
            color: #fff;
        }

        /* Estilo para el texto en negrita */
        .table-futurista strong {
            font-weight: bold;
        }
</style>