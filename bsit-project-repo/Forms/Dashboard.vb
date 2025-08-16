Public NotInheritable Class Dashboard

    Private Sub Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.lblFullname.Text = My.Application.Info.ProductName
        Me.lblRole.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.lblUsername.Text = My.Application.Info.Copyright



        Dim id As Integer = Session.UserID
        Dim uname As String = Session.Username
        Dim fname As String = Session.Fullname
        Dim role As String = Session.Role

        lblFullname.Text = fname
        lblUsername.Text = uname
        lblRole.Text = role
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
