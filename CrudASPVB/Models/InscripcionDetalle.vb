﻿Imports System.ComponentModel.DataAnnotations

Public Class InscripcionDetalle

    Public Property id_inscripcion As Integer

    <Display(Name:="Descripcion del Curso")>
    Public Property descripcion_curso As String

    <Display(Name:="Descripcion del Docente")>
    Public Property descripcion_docente As String

    <Display(Name:="Fecha de Inscripcion")>
    Public Property fecha As DateTime

End Class
