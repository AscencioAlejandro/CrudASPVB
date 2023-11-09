Imports System.Data.SqlClient
Imports System.Web.Mvc

Namespace Repository
    Public Class CursoRepository

        Private ReadOnly _dbConnection As Connection

        Public Sub New()
            _dbConnection = New Connection()
        End Sub

        Function GetCurso() As DataTable

            Dim dtCursos As New DataTable

            Using conn As SqlConnection = _dbConnection.OpenConnection()
                Using cmd As New SqlCommand("GetallCursos", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    Using dataReader As SqlDataReader = cmd.ExecuteReader()
                        dtCursos.Load(dataReader)

                    End Using

                End Using
            End Using

            Return dtCursos

        End Function


        Function alterCurso(objCurso As Curso) As Boolean

            Using conn As SqlConnection = _dbConnection.OpenConnection()
                Using cmd As New SqlCommand("AlterCursos", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    Dim Valueid As Integer

                    Integer.TryParse(objCurso.id_curso, Valueid)
                    cmd.Parameters.Add(New SqlParameter("@idcurso", SqlDbType.Int, 50))
                    cmd.Parameters("@idCurso").Value = Valueid

                    cmd.Parameters.Add(New SqlParameter("@nombre", SqlDbType.NVarChar, 50))
                    cmd.Parameters("@nombre").Value = objCurso.nombre


                    Dim resultQuery = cmd.ExecuteNonQuery()

                    If resultQuery > 1 Then
                        Return True
                    End If

                End Using
            End Using

            Return False

        End Function


        Function deleteCurso(Valueid As Integer) As Boolean

            Using conn As SqlConnection = _dbConnection.OpenConnection()
                Using cmd As New SqlCommand("DeleteCursos", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    Dim valueSanitized As Integer
                    Integer.TryParse(Valueid, valueSanitized)
                    cmd.Parameters.Add(New SqlParameter("@idcurso", SqlDbType.Int, 50))
                    cmd.Parameters("@idcurso").Value = valueSanitized

                    Try
                        Dim resultQuery = cmd.ExecuteNonQuery()

                        If resultQuery > 1 Then
                            Return True
                        End If

                    Catch ex As System.Data.SqlClient.SqlException

                        Console.WriteLine("Error al eliminar el registro: " & ex.Message)

                    End Try


                End Using
            End Using

            Return False

        End Function


        Function GetCursoById(Valueid As Integer) As Curso

            Dim objCurso As New Curso()

            Using conn As SqlConnection = _dbConnection.OpenConnection()
                Using cmd As New SqlCommand("FindCursos", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    Dim valueSanitized As Integer
                    Integer.TryParse(Valueid, valueSanitized)
                    cmd.Parameters.Add(New SqlParameter("@idcurso", SqlDbType.Int, 50))
                    cmd.Parameters("@idcurso").Value = valueSanitized

                    Using dataReader As SqlDataReader = cmd.ExecuteReader()

                        If dataReader.Read() Then
                            objCurso.id_curso = dataReader("id_curso").ToString()
                            objCurso.nombre = dataReader("nombre").ToString()

                        End If

                    End Using

                End Using
            End Using

            Return objCurso

        End Function
    End Class
End Namespace