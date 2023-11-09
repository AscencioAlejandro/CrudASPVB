@ModelType CrudASPVB.Inscripcion

@Code
    Dim titleGeneric, functionDescripcion As String
    @If Model Is Nothing Then
        ViewData("Title") = "Save Inscripcion "
        titleGeneric = "Save"
        functionDescripcion = "Guardar"
    Else
        ViewData("Title") = "Update Inscripcion "
        titleGeneric = "Update"
        functionDescripcion = "Editar"
    End If

    End Code



<h2>@titleGeneric</h2>

@Using (Html.BeginForm(functionDescripcion, "Inscripcion", FormMethod.Post))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    <h4>Docente</h4>
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-group">
        <div class="col-md-10">
            @Html.HiddenFor(Function(model) model.id_inscripcion, New With {.Value = Model.id_inscripcion})
        </div>
    </div>
    <div class="form-group">
        @Html.LabelFor(Function(model) model.id_curso, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @*@Html.DropDownListFor(Function(model) model.id_curso, CType(ViewBag.SelectListDeCursos, SelectList), "-- Seleccione un curso --", New With {.class = "form-control"})*@
            @If ViewBag.IdCursoSeleccionado IsNot Nothing Then
                @Html.DropDownListFor(Function(model) model.id_curso, CType(ViewBag.SelectListDeCursos, SelectList), "-- Seleccione un curso --", New With {.class = "form-control", .Value = ViewBag.IdCursoSeleccionado})
            Else
                @Html.DropDownListFor(Function(model) model.id_curso, CType(ViewBag.SelectListDeCursos, SelectList), "-- Seleccione un curso --", New With {.class = "form-control"})
            End If
            @Html.ValidationMessageFor(Function(model) model.id_curso, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.id_docente, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @If ViewBag.IdCursoSeleccionado IsNot Nothing Then
                @Html.DropDownListFor(Function(model) model.id_docente, CType(ViewBag.SelectListDeDocentes, SelectList), "-- Seleccione un docente --", New With {.class = "form-control", .Value = ViewBag.IdDocenteSeleccionado})
            Else
                @Html.DropDownListFor(Function(model) model.id_docente, CType(ViewBag.SelectListDeDocentes, SelectList), "-- Seleccione un docente --", New With {.class = "form-control"})
            End If

            @Html.ValidationMessageFor(Function(model) model.id_docente, "", New With {.class = "text-danger"})
        </div>
    </div>



    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Save" class="btn btn-default" />
        </div>
    </div>
</div>
End Using

        <div>
            @Html.ActionLink("Back to List", "Index")
        </div>

        @Section Scripts
            @Scripts.Render("~/bundles/jqueryval")
        End Section
