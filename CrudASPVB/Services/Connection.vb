Imports System.Data.SqlClient

Public Class Connection

    Private ReadOnly _connectionString As String
    Private ReadOnly _connection As SqlConnection

    Public Sub New()
        _connectionString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        _connection = New SqlConnection(_connectionString)
    End Sub

    Public Function OpenConnection() As SqlConnection
        If _connection.State <> ConnectionState.Open Then
            _connection.Open()
        End If
        Return _connection
    End Function

    Public Sub CloseConnection()
        If _connection.State = ConnectionState.Open Then
            _connection.Close()
        End If
    End Sub

End Class
