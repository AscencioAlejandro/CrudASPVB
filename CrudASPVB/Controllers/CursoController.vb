Imports System.Web.Mvc
Imports CrudASPVB.Repository

Namespace Controllers
    Public Class CursoController
        Inherits Controller
        Private _objrepo As CursoRepository = New CursoRepository()
        Private _myfunction As Myfunctions = New Myfunctions()

        Function getCursos() As List(Of Curso)
            Dim dtCursos As DataTable = _objrepo.GetCurso()
            Dim ltsCurso As List(Of Curso) = _myfunction.ConvertDataTableToList(Of Curso)(dtCursos)
            Return ltsCurso
        End Function

        Function Guardar() As ActionResult

            Dim objCurso As New Curso()
            Return View("Alter", objCurso)

        End Function
        Function Editar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objCurso = _objrepo.GetCursoById(id)
            Return View("Alter", objCurso)

        End Function

        Function Eliminar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objCurso = _objrepo.deleteCurso(id)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Editar(Curso As Curso) As ActionResult

            Dim objCurso = _objrepo.alterCurso(Curso)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Guardar(Curso As Curso) As ActionResult

            Dim objCurso = _objrepo.alterCurso(Curso)
            Return RedirectToAction(NameOf(Index))

        End Function
        Function Index() As ActionResult
            Dim ltsCursos As New List(Of Curso)()
            ltsCursos = getCursos()
            Return View(ltsCursos)
        End Function

    End Class
End Namespace