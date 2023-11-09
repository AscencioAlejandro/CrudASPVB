Imports System.ComponentModel.DataAnnotations

Public Class Inscripcion
    Public Property id_inscripcion As Integer

    <Display(Name:="Descripcion del Curso")>
    Public Property id_curso As Integer

    <Display(Name:="Descripcion del Docente")>
    Public Property id_docente As Integer
    Public Property fecha As DateTime
End Class
