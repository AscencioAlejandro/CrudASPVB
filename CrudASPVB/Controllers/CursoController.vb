Imports System.Net
Imports System.Web.Mvc
Imports CrudASPVB.Repository
Imports System.Net.Http
Imports Newtonsoft.Json

Namespace Controllers
    Public Class CursoController
        Inherits Controller
        Private _objrepo As CursoRepository = New CursoRepository()
        Private _myfunction As Myfunctions = New Myfunctions()
        Private _serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet>
        Public Function getCursos() As String

            Dim dtCursos As DataTable = _objrepo.GetCurso()
            Dim ltsCurso As List(Of Curso) = _myfunction.ConvertDataTableToList(Of Curso)(dtCursos)
            Dim json As String = _serializer.Serialize(ltsCurso)
            Return json

        End Function

        <HttpPost>
        Function alterCurso(Curso As Curso) As ActionResult
            Try

                Dim objCurso = _objrepo.alterCurso(Curso)
                Return Json(objCurso, JsonRequestBehavior.AllowGet)

            Catch ex As Exception

                Response.StatusCode = HttpStatusCode.InternalServerError
                Return Content("Error al procesar la solicitud: " & ex.Message)
            End Try

        End Function

        <HttpPost>
        Function deleteCurso(id? As Integer) As ActionResult

            Try

                Dim objCurso = _objrepo.deleteCurso(id)
                If Not objCurso Then
                    Response.StatusCode = HttpStatusCode.InternalServerError
                    Return Content("Este Registro esta Relacionado en otra Tabla")
                End If
                Return json(objCurso, JsonRequestBehavior.AllowGet)

            Catch ex As Exception

                Response.StatusCode = HttpStatusCode.InternalServerError
                Return Content("Error al procesar la solicitud: " & ex.Message)
            End Try

        End Function



    End Class
End Namespace