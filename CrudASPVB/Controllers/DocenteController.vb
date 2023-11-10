Imports System.Data.SqlClient
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
        Function deleteDocente(id? As Integer) As String

            Dim objDocente = _objrepo.deleteDocente(id)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(objDocente)
            Return json

        End Function


    End Class
End Namespace