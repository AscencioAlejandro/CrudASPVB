Imports System.Net
Imports System.Web.Mvc
Imports CrudASPVB.Repository
Imports Newtonsoft.Json

Namespace Controllers
    Public Class InscripcionController
        Inherits Controller

        Private _objrepo As InscripcionRepository = New InscripcionRepository()
        Private _objcurso As CursoRepository = New CursoRepository()
        Private _objdocente As DocenteRepository = New DocenteRepository()
        Private _myfunction As Myfunctions = New Myfunctions()

        <HttpGet>
        Function getInscripciones() As String

            Dim dtInscripciones As DataTable = _objrepo.GetInscripcion()
            Dim ltsInscripcion As List(Of InscripcionDetalle) = _myfunction.ConvertDataTableToList(Of InscripcionDetalle)(dtInscripciones)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(ltsInscripcion)

            Return json

        End Function

        <HttpPost>
        Function deleteInscripcion(id? As Integer) As ActionResult

            Try

                Dim objInscripcion = _objrepo.deleteInscripcion(id)

                Return Json(objInscripcion, JsonRequestBehavior.AllowGet)
                ModelState.Clear()

            Catch ex As Exception

                Response.StatusCode = HttpStatusCode.InternalServerError
                Return Content("Error al procesar la solicitud: " & ex.Message)

            End Try

        End Function

        <HttpPost>
        Function alterInscripcion(Inscripcion As Inscripcion) As ActionResult

            Try

                If Not ModelState.IsValid Then
                    Dim errorsJson = JsonConvert.SerializeObject(ModelState)
                    Response.StatusCode = HttpStatusCode.InternalServerError
                    Return Content(errorsJson, "application/json")
                End If

                Dim objInscripcion = _objrepo.alterInscripcion(Inscripcion)
                Return Json(objInscripcion, JsonRequestBehavior.AllowGet)
                ModelState.Clear()

            Catch ex As Exception

                Response.StatusCode = HttpStatusCode.InternalServerError
                Return Content("Error al procesar la solicitud: " & ex.Message)

            End Try

        End Function


        Function Index() As ActionResult

            Return View()

        End Function


    End Class
End Namespace