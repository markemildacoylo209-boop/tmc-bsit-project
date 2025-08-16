Public Class LoginForm

    Private Sub LoginFrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        'Connection.TestConnection()
        'Dim hash As String = Security.HashPassword("admin123")
        'Console.Write("(" & hash & ")")

    End Sub


    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        If Auth.LoginWithSql(username, password) Then
            MessageBox.Show("Login successful!")
            Me.Hide()
            Dim dash As New Main
            dash.ShowDialog()
            Me.Close()

        Else
            MessageBox.Show("Invalid username or password.")
        End If
    End Sub

    Private Sub showPassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showPassword.CheckedChanged
        If showPassword.Checked Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub
End Class
