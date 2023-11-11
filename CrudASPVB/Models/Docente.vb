Imports System.ComponentModel.DataAnnotations

Public Class Docente
    Public Property id_docente As Integer

    <Display(Name:="Nombre de Docente")>
    <Required>
    Public Property nombre As String

    <Display(Name:="Apellido de Docente")>
    <Required>
    Public Property apellido As String

    Public ReadOnly Property nombreCompleto As String
        Get
            Return nombre & " " & apellido
        End Get
    End Property
End Class
