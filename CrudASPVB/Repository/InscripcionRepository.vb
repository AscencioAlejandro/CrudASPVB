Imports System.Data.SqlClient

Public Class InscripcionRepository

    Private ReadOnly _dbConnection As Connection

    Public Sub New()
        _dbConnection = New Connection()
    End Sub

    Function GetInscripcion() As DataTable

        Dim dtCursos As New DataTable

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("GetallInscripciones", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Using dataReader As SqlDataReader = cmd.ExecuteReader()
                    dtCursos.Load(dataReader)

                End Using

            End Using
        End Using

        Return dtCursos

    End Function


    Function alterInscripcion(objCurso As Inscripcion) As Boolean

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("AlterInscripcion", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim Valueidinscripcion As Integer
                Dim Valueidcurso As Integer
                Dim Valueiddocente As Integer

                Integer.TryParse(objCurso.id_inscripcion, Valueidinscripcion)
                cmd.Parameters.Add(New SqlParameter("@idinscripcion", SqlDbType.Int, 50))
                cmd.Parameters("@idinscripcion").Value = Valueidinscripcion

                Integer.TryParse(objCurso.id_curso, Valueidcurso)
                cmd.Parameters.Add(New SqlParameter("@idcurso", SqlDbType.Int, 50))
                cmd.Parameters("@idcurso").Value = Valueidcurso

                Integer.TryParse(objCurso.id_docente, Valueiddocente)
                cmd.Parameters.Add(New SqlParameter("@iddocente", SqlDbType.Int, 50))
                cmd.Parameters("@iddocente").Value = Valueiddocente

                Dim resultQuery = cmd.ExecuteNonQuery()

                Return resultQuery > 0

            End Using
        End Using

        Return False

    End Function


    Function deleteInscripcion(Valueid As Integer) As Boolean

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("DeleteInscripciones", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim valueSanitized As Integer
                Integer.TryParse(Valueid, valueSanitized)
                cmd.Parameters.Add(New SqlParameter("@idinscripcion", SqlDbType.Int, 50))
                cmd.Parameters("@idinscripcion").Value = valueSanitized

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


    Function GetInscripcionById(Valueid As Integer) As Inscripcion

        Dim objInscripcion As New Inscripcion()

        Using conn As SqlConnection = _dbConnection.OpenConnection()
            Using cmd As New SqlCommand("FindInscripcion", conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim valueSanitized As Integer
                Integer.TryParse(Valueid, valueSanitized)
                cmd.Parameters.Add(New SqlParameter("@idinscripcion", SqlDbType.Int, 50))
                cmd.Parameters("@idinscripcion").Value = valueSanitized

                Using dataReader As SqlDataReader = cmd.ExecuteReader()

                    If dataReader.Read() Then

                        objInscripcion.id_curso = dataReader("id_curso").ToString()
                        objInscripcion.id_docente = dataReader("id_docente").ToString()
                        objInscripcion.id_inscripcion = dataReader("id_inscripcion").ToString()
                        objInscripcion.fecha = dataReader("fecha").ToString()

                    End If

                End Using

            End Using
        End Using

        Return objInscripcion

    End Function

End Class
