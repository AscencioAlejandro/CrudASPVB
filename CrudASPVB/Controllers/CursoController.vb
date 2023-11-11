Imports System.Net
Imports System.Web.Mvc
Imports CrudASPVB.Repository
Imports System.Net.Http


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
        Function alterCurso(Curso As Curso) As String

            'If Not ModelState.IsValid Then
            '    Return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            'End If

            Dim objCurso = _objrepo.alterCurso(Curso)
            Dim json As String = _serializer.Serialize(objCurso)
            Return json

        End Function

        <HttpPost>
        Function deleteCurso(id? As Integer) As String

            If Not ModelState.IsValid Then
                Return _serializer.Serialize(ModelState)
            End If

            Dim objCurso = _objrepo.deleteCurso(id)
            Dim json As String = _serializer.Serialize(objCurso)
            Return json

        End Function



    End Class
End Namespace