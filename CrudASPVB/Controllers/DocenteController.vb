Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web.Mvc

Namespace Controllers
    Public Class DocenteController
        Inherits Controller

        Private _objrepo As DocenteRepository = New DocenteRepository()
        Private _myfunction As Myfunctions = New Myfunctions()

        Function getDocentes() As List(Of Docente)
            Dim dtDocentes As DataTable = _objrepo.GetDocente()
            Dim ltsDocente As List(Of Docente) = _myfunction.ConvertDataTableToList(Of Docente)(dtDocentes)
            Return ltsDocente
        End Function

        Function Guardar() As ActionResult

            Dim objDocente As New Docente()
            Return View("Alter", objDocente)

        End Function
        Function Editar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objDocente = _objrepo.GetDocenteById(id)
            Return View("Alter", objDocente)

        End Function

        Function Eliminar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objDocente = _objrepo.deleteDocente(id)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Editar(docente As Docente) As ActionResult

            Dim objDocente = _objrepo.alterDocente(docente)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Guardar(docente As Docente) As ActionResult

            Dim objDocente = _objrepo.alterDocente(docente)
            Return RedirectToAction(NameOf(Index))

        End Function
        Function Index() As ActionResult
            Dim ltsDocentes As New List(Of Docente)()
            ltsDocentes = getDocentes()
            Return View(ltsDocentes)
        End Function


    End Class
End Namespace