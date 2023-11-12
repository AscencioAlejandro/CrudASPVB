Imports System.Data.SqlClient

Public Class DocenteRepository

    Private ReadOnly _dbConnection As Connection

    Public Sub New()
        _dbConnection = New Connection()
    End Sub

    Function GetDocente() As DataTable

        Dim dtDocentes As New DataTable

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("GetallDocentes", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Using dataReader As SqlDataReader = cmd.ExecuteReader()
                    dtDocentes.Load(dataReader)

                End Using

            End Using
        End Using

        Return dtDocentes

    End Function


    Function alterDocente(objDocente As Docente) As Boolean

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("AlterDocentes", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim Valueid As Integer

                Integer.TryParse(objDocente.id_docente, Valueid)
                cmd.Parameters.Add(New SqlParameter("@iddocente", SqlDbType.Int, 50))
                cmd.Parameters("@iddocente").Value = Valueid

                cmd.Parameters.Add(New SqlParameter("@nombredocente", SqlDbType.NVarChar, 50))
                cmd.Parameters("@nombredocente").Value = objDocente.nombre

                cmd.Parameters.Add(New SqlParameter("@apellidodocente", SqlDbType.NVarChar, 50))
                cmd.Parameters("@apellidodocente").Value = objDocente.apellido

                Dim resultQuery = cmd.ExecuteNonQuery()

                Return resultQuery > 0

            End Using
        End Using

        Return False

    End Function


    Function deleteDocente(Valueid As Integer) As Boolean

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("DeleteDocentes", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim valueSanitized As Integer
                Integer.TryParse(Valueid, valueSanitized)
                cmd.Parameters.Add(New SqlParameter("@iddocente", SqlDbType.Int, 50))
                cmd.Parameters("@iddocente").Value = valueSanitized

                Try
                    Dim resultQuery = cmd.ExecuteNonQuery()

                    Return resultQuery > 0

                Catch ex As System.Data.SqlClient.SqlException

                    Console.WriteLine("Error al eliminar el registro: " & ex.Message)

                End Try


            End Using
        End Using

        Return False

    End Function


    Function GetDocenteById(Valueid As Integer) As Docente

        Dim objDocente As New Docente()

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("FindDocentes", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim valueSanitized As Integer
                Integer.TryParse(Valueid, valueSanitized)
                cmd.Parameters.Add(New SqlParameter("@iddocente", SqlDbType.Int, 50))
                cmd.Parameters("@iddocente").Value = valueSanitized

                Using dataReader As SqlDataReader = cmd.ExecuteReader()

                    If dataReader.Read() Then
                        objDocente.id_docente = dataReader("id_docente").ToString()
                        objDocente.nombre = dataReader("nombre").ToString()
                        objDocente.apellido = dataReader("apellido").ToString()
                    End If

                End Using

            End Using
        End Using

        Return objDocente

    End Function



End Class
