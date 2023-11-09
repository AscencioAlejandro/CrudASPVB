Imports System.Reflection

Public Class Myfunctions
    Function ConvertDataTableToList(Of T)(ByVal dataTable As DataTable) As List(Of T)
        Dim list As New List(Of T)

        For Each row As DataRow In dataTable.Rows
            Dim obj As T = Activator.CreateInstance(Of T)()

            For Each prop As PropertyInfo In GetType(T).GetProperties()
                If dataTable.Columns.Contains(prop.Name) AndAlso Not IsDBNull(row(prop.Name)) Then
                    prop.SetValue(obj, Convert.ChangeType(row(prop.Name), prop.PropertyType))
                End If
            Next

            list.Add(obj)
        Next

        Return list
    End Function
End Class
