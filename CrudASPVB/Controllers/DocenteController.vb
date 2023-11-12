Imports System.Data.SqlClient
Imports System.Net
Imports System.Reflection
Imports System.Web.Mvc
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    Public Class DocenteController
        Inherits Controller

        Private _objrepo As DocenteRepository = New DocenteRepository()
        Private _myfunction As Myfunctions = New Myfunctions()

        Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet>
        Public Function getDocentes() As String

            Dim dtDocente As DataTable = _objrepo.GetDocente()
            Dim ltsCurso As List(Of Docente) = _myfunction.ConvertDataTableToList(Of Docente)(dtDocente)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(ltsCurso)
            Return json

        End Function

        <HttpPost>
        Function alterDocente(objDocente As Docente) As String

            Dim resultQuery = _objrepo.alterDocente(objDocente)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(resultQuery)
            Return json

        End Function

        <HttpPost>
        Function deleteDocente(id? As Integer) As ActionResult

            Try

                Dim objCurso = _objrepo.deleteDocente(id)
                If Not objCurso Then
                    Response.StatusCode = HttpStatusCode.InternalServerError
                    Return Content("Este Registro esta Relacionado en otra Tabla")
                End If
                Return Json(objCurso, JsonRequestBehavior.AllowGet)

            Catch ex As Exception

                Response.StatusCode = HttpStatusCode.InternalServerError
                Return Content("Error al procesar la solicitud: " & ex.Message)
            End Try

        End Function


    End Class
End Namespace