Imports System.Web.Mvc
Imports CrudASPVB.Repository

Namespace Controllers
    Public Class CursoController
        Inherits Controller
        Private _objrepo As CursoRepository = New CursoRepository()
        Private _myfunction As Myfunctions = New Myfunctions()

        Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet>
        Public Function getCursos() As String

            Dim dtCursos As DataTable = _objrepo.GetCurso()
            Dim ltsCurso As List(Of Curso) = _myfunction.ConvertDataTableToList(Of Curso)(dtCursos)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(ltsCurso)
            Return json

        End Function

        <HttpPost>
        Function alterCurso(Curso As Curso) As String

            Dim objCurso = _objrepo.alterCurso(Curso)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(objCurso)
            Return json

        End Function

        <HttpPost>
        Function deleteCurso(id? As Integer) As String

            Dim objCurso = _objrepo.deleteCurso(id)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(objCurso)
            Return json

        End Function



    End Class
End Namespace