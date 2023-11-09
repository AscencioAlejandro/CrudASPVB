Imports System.Web.Mvc
Imports CrudASPVB.Repository

Namespace Controllers
    Public Class InscripcionController
        Inherits Controller

        Private _objrepo As InscripcionRepository = New InscripcionRepository()
        Private _objcurso As CursoRepository = New CursoRepository()
        Private _objdocente As DocenteRepository = New DocenteRepository()
        Private _myfunction As Myfunctions = New Myfunctions()


        Function getInscripciones() As List(Of InscripcionDetalle)

            Dim dtInscripciones As DataTable = _objrepo.GetInscripcion()
            Dim ltsInscripcion As List(Of InscripcionDetalle) = _myfunction.ConvertDataTableToList(Of InscripcionDetalle)(dtInscripciones)
            Return ltsInscripcion

        End Function

        Function Guardar() As ActionResult

            'Dim selectedcursos = New List(Of SelectListItem)()
            'Dim selecteddocentes = New List(Of SelectListItem)()

            Dim ltscursos = New List(Of Curso)()
            Dim ltsdocentes = New List(Of Docente)()

            Dim dtcurso = _objcurso.GetCurso()
            Dim dtdocente = _objdocente.GetDocente()

            ltscursos = _myfunction.ConvertDataTableToList(Of Curso)(dtcurso)
            ltsdocentes = _myfunction.ConvertDataTableToList(Of Docente)(dtdocente)

            ViewBag.SelectListDeCursos = New SelectList(ltscursos, "id_curso", "nombre")
            ViewBag.SelectListDeDocentes = New SelectList(ltsdocentes, "id_docente", "nombreCompleto")

            Dim objInscripcion As New Inscripcion()
            Return View("Alter", objInscripcion)

        End Function
        Function Editar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objInscripcion = _objrepo.GetInscripcionById(id)

            Dim ltscursos = New List(Of Curso)()
            Dim ltsdocentes = New List(Of Docente)()

            Dim dtcurso = _objcurso.GetCurso()
            Dim dtdocente = _objdocente.GetDocente()

            ltscursos = _myfunction.ConvertDataTableToList(Of Curso)(dtcurso)
            ltsdocentes = _myfunction.ConvertDataTableToList(Of Docente)(dtdocente)

            ViewBag.SelectListDeCursos = New SelectList(ltscursos, "id_curso", "nombre")
            ViewBag.SelectListDeDocentes = New SelectList(ltsdocentes, "id_docente", "nombreCompleto")

            ViewBag.IdCursoSeleccionado = objInscripcion.id_curso
            ViewBag.IdDocenteSeleccionado = objInscripcion.id_docente

            Return View("Alter", objInscripcion)

        End Function

        Function Eliminar(id? As Integer) As ActionResult
            If id Is Nothing Then
                Return View()
            End If

            Dim objInscripcion = _objrepo.deleteInscripcion(id)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Editar(Inscripcion As Inscripcion) As ActionResult

            Dim objInscripcion = _objrepo.alterInscripcion(Inscripcion)
            Return RedirectToAction(NameOf(Index))

        End Function

        <HttpPost>
        Function Guardar(Inscripcion As Inscripcion) As ActionResult

            Dim objInscripcion = _objrepo.alterInscripcion(Inscripcion)
            Return RedirectToAction(NameOf(Index))

        End Function
        Function Index() As ActionResult

            Dim ltsInscripcions As New List(Of InscripcionDetalle)()
            ltsInscripcions = getInscripciones()
            Return View(ltsInscripcions)

        End Function

    End Class
End Namespace