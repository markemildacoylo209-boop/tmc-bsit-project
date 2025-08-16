Imports MySql.Data.MySqlClient

Public Class Connection
    Private Shared connString As String = "Server=127.0.0.1;Port=3309;Database=vb_db;Uid=emil2;Pwd=vG7n4AP9NSCmXNLg;"
    Private Shared connection As MySqlConnection

    Public Shared Function GetConnection() As MySqlConnection
        If connection Is Nothing Then
            connection = New MySqlConnection(connString)
        End If
        Return connection
    End Function

    ' Test method
    Public Shared Sub TestConnection()
        Try
            Dim conn As MySqlConnection = GetConnection()
            conn.Open()
            MessageBox.Show("Connection successful!", "MySQL Test", MessageBoxButtons.OK, MessageBoxIcon.Information)
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Connection failed: " & ex.Message, "MySQL Test", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' INSERT (dynamic)
    Public Shared Function Insert(ByVal table As String, ByVal data As Dictionary(Of String, Object)) As Boolean
        Dim result As Boolean = False
        Using conn As MySqlConnection = GetConnection()
            conn.Open()

            ' Build column list and parameter list manually
            Dim columns As New List(Of String)()
            Dim parameters As New List(Of String)()
            For Each key As String In data.Keys
                columns.Add(key)
                parameters.Add("@" & key)
            Next

            Dim query As String = "INSERT INTO " & table & " (" & String.Join(",", columns.ToArray()) & ") VALUES (" & String.Join(",", parameters.ToArray()) & ")"

            Using cmd As New MySqlCommand(query, conn)
                For Each kvp As KeyValuePair(Of String, Object) In data
                    cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                Next
                result = cmd.ExecuteNonQuery() > 0
            End Using
        End Using
        Return result
    End Function

    ' UPDATE (dynamic)
    Public Shared Function Update(ByVal table As String, ByVal data As Dictionary(Of String, Object), ByVal whereClause As String, ByVal whereParams As Dictionary(Of String, Object)) As Boolean
        Dim result As Boolean = False
        Using conn As MySqlConnection = GetConnection()
            conn.Open()

            Dim setList As New List(Of String)()
            For Each key As String In data.Keys
                setList.Add(key & "=@" & key)
            Next

            Dim query As String = "UPDATE " & table & " SET " & String.Join(",", setList.ToArray()) & " WHERE " & whereClause

            Using cmd As New MySqlCommand(query, conn)
                For Each kvp As KeyValuePair(Of String, Object) In data
                    cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                Next
                If whereParams IsNot Nothing Then
                    For Each kvp As KeyValuePair(Of String, Object) In whereParams
                        cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                    Next
                End If

                result = cmd.ExecuteNonQuery() > 0
            End Using
        End Using
        Return result
    End Function

    ' SELECT with status='active'
    Public Shared Function SelectActive(ByVal table As String) As DataTable
        Dim dt As New DataTable()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "SELECT * FROM " & table & " WHERE status='active'"
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


End Class
