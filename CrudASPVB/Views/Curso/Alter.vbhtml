@ModelType CrudASPVB.Curso
@Code
    Dim titleGeneric, functionDescripcion As String
    @If Model Is Nothing Then
        ViewData("Title") = "Save Curso "
        titleGeneric = "Save"
        functionDescripcion = "Guardar"
    Else
        ViewData("Title") = "Update Curso "
        titleGeneric = "Update"
        functionDescripcion = "Editar"
    End If

    End Code

<h2>@titleGeneric</h2>

@Using (Html.BeginForm(functionDescripcion, "Curso", FormMethod.Post))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Curso</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
     <div class="form-group">        
         <div class="col-md-10">            
             @Html.HiddenFor(Function(model) model.id_curso, New With {.Value = Model.id_curso})
         </div>
     </div>
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.nombre, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        
                        @Html.EditorFor(Function(model) model.nombre, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.nombre, "", New With {.class = "text-danger"})
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
