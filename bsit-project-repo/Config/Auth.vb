Imports MySql.Data.MySqlClient

Public Class Auth
    Public Shared Function Login(ByVal username As String, ByVal password As String) As Boolean
        Try
            If username = "admin" AndAlso password = "admin" Then
                ' ==== SET DATA TO SESSION ====
                Session.UserID = 1
                Session.Username = "emil2"
                Session.Fullname = "Emil Ago"
                Session.Role = "admin"
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("Login error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function LoginWithSql(ByVal username As String, ByVal password As String) As Boolean
        Dim hashInput As String = Security.HashPassword(password)
        Dim result As Boolean = False

        Dim query As String = "SELECT id, username, fullname, role, password FROM users WHERE username=@username"

        Using conn As MySqlConnection = Connection.GetConnection()
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim dbHash As String = reader("password").ToString()

                        ' Check if password is correct
                        If dbHash = hashInput Then
                            ' Set Session values
                            Session.UserID = Convert.ToInt32(reader("id"))
                            Session.Username = reader("username").ToString()
                            Session.Fullname = reader("fullname").ToString()
                            Session.Role = reader("role").ToString()

                            result = True
                        End If
                    End If
                End Using
            End Using
            conn.Close()
        End Using

        Return result
    End Function

End Class
