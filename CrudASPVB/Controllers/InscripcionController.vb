Imports System.Web.Mvc
Imports CrudASPVB.Repository

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
        Function deleteInscripcion(id? As Integer) As String

            Dim resultQuery = _objrepo.deleteInscripcion(id)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(resultQuery)
            Return json

        End Function

        <HttpPost>
        Function alterInscripcion(Inscripcion As Inscripcion) As String

            Dim resultQuery = _objrepo.alterInscripcion(Inscripcion)
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim json As String = serializer.Serialize(resultQuery)
            Return json

        End Function


        Function Index() As ActionResult

            Return View()

        End Function


    End Class
End Namespace