Imports MySql.Data.MySqlClient
Imports System.Data

Public Class User

    Private Shared _selectedUserId As Integer

    Public Shared Property SelectedUserId() As Integer
        Get
            Return _selectedUserId
        End Get
        Set(ByVal value As Integer)
            _selectedUserId = value
        End Set
    End Property

    ' =============================
    ' Get user info  using by id 
    ' =============================

    Public Shared Function GetUserById(ByVal id As Integer) As DataRow
        Dim dt As New DataTable()
        Try
            Using conn As MySqlConnection = Connection.GetConnection()
                conn.Open()
                Dim query As String = "SELECT * FROM users WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", id)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error getting user: " & ex.Message)
        End Try
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    ' =============================
    ' INSERT user
    ' =============================
    Public Shared Function InsertUser(ByVal username As String, ByVal password As String, ByVal fullname As String, ByVal role As String, ByVal status As String) As Boolean
        Dim result As Boolean = False
        Dim hash As String = Security.HashPassword(password)
        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Dim query As String = "INSERT INTO users (username, password, fullname, role, status) VALUES (@username, @password, @fullname, @role, @status)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", hash)
                cmd.Parameters.AddWithValue("@fullname", fullname)
                cmd.Parameters.AddWithValue("@role", role)
                cmd.Parameters.AddWithValue("@status", status)
                result = cmd.ExecuteNonQuery() > 0
            End Using
        End Using
        Return result
    End Function

    ' =============================
    ' UPDATE user (by ID)
    ' =============================
    Public Shared Function UpdateUser(ByVal id As Integer, ByVal fullname As String, ByVal role As String, ByVal status As String) As Boolean
        Dim result As Boolean = False
        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Dim query As String = "UPDATE users SET fullname=@fullname, role=@role, status=@status WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@fullname", fullname)
                cmd.Parameters.AddWithValue("@role", role)
                cmd.Parameters.AddWithValue("@status", status)
                cmd.Parameters.AddWithValue("@id", id)
                result = cmd.ExecuteNonQuery() > 0
            End Using
        End Using
        Return result
    End Function

    ' =============================
    ' SELECT all active users
    ' =============================
    Public Shared Function GetActiveUsers() As DataTable
        Dim dt As New DataTable()
        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Dim query As String = "SELECT id, username, fullname, role, status, created_at FROM users WHERE status='active'"
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' =============================
    ' SELECT user by username
    ' =============================
    Public Shared Function GetUserByUsername(ByVal username As String) As DataTable
        Dim dt As New DataTable()
        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Dim query As String = "SELECT * FROM users WHERE username=@username"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' =============================
    ' DELETE user (by ID)
    ' =============================
    Public Shared Function DeleteUser(ByVal id As Integer) As Boolean
        Dim result As Boolean = False
        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Dim query As String = "DELETE FROM users WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                result = cmd.ExecuteNonQuery() > 0
            End Using
        End Using
        Return result
    End Function


    Public Shared Function GetAllUsers() As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As MySqlConnection = Connection.GetConnection()
                conn.Open()

                ' Piliin lang ang gusto mong columns
                Dim query As String = "SELECT id AS 'ID', fullname AS 'Fullname', role AS 'Role', status AS 'Status' FROM users"
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message)
        End Try

        Return dt
    End Function

End Class
