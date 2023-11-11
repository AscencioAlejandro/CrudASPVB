Imports System.ComponentModel.DataAnnotations

Public Class Curso

    Public Property id_curso As Integer

    <Display(Name:="Descripcion del Curso")>
    <Required>
    Public Property nombre As String

End Class
