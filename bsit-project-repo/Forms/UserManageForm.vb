Public Class UserManageForm

    Private Sub UserManageForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadUsers()

    End Sub

    ' Function para i-load/reload ang DataGridView
    Private Sub LoadUsers()
        Try
            ' Font ng rows
            DataGridView1.DefaultCellStyle.Font = New Font("Arial", 10)
            ' Font ng headers
            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Bold)

            ' Bind data galing sa DB
            DataGridView1.DataSource = User.GetAllUsers()

            ' Auto resize columns
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception
            MessageBox.Show("Error reloading users: " & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        ' Siguraduhin na hindi header row
        If e.RowIndex >= 0 Then
            ' Kunin ang buong row na na-click
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Kunin ang ID
            Dim id As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)

            ' I-save sa shared property
            User.SelectedUserId = id

            ' Kunin details ng user gamit ang id
            Dim userRow As DataRow = User.GetUserById(id)
            If userRow IsNot Nothing Then
                txtFullname.Text = userRow("fullname").ToString()
                txtUsername.Text = userRow("username").ToString()
                comboRole.Text = userRow("role").ToString()
                comboStatus.Text = userRow("status").ToString()

                ' Password usually naka-hash
                txtpassword.Text = ""
                txtConfirmPassword.Text = ""
            End If
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim fullname As String = txtFullname.Text
        Dim role As String = comboRole.Text
        Dim status As String = comboStatus.Text
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        Dim confirm_password As String = txtConfirmPassword.Text

        'If User.SelectedUserId <> 0 Then
        ' Id have a value or they have value 
        'Console.WriteLine(User.SelectedUserId)
        'Else
        ' No selected Value
        'Console.WriteLine("No selected Value")
        'End If

        'User.SelectedUserId = 0

        ' Simple validation
        If fullname = "" Or role = "" Or status = "" Or username = "" Or password = "" Then
            MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If password <> confirm_password Then
            MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try

            ' Call InsertUser from User class
            Dim success As Boolean = User.InsertUser(username, password, fullname, role, status)

            If success Then
                MessageBox.Show("User successfully inserted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Optional: clear input fields
                txtFullname.Text = ""
                txtUsername.Text = ""
                txtpassword.Text = ""
                txtConfirmPassword.Text = ""
                comboRole.SelectedIndex = -1
                comboStatus.SelectedIndex = -1

                LoadUsers()
            Else
                MessageBox.Show("Failed to insert user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)

            MessageBox.Show("Error inserting user: " & ex.Message)
        End Try
       
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim fullname As String = txtFullname.Text
        Dim role As String = comboRole.Text
        Dim status As String = comboStatus.Text
        Dim username As String = txtUsername.Text

        ' Check if user is selected
        If User.SelectedUserId = 0 Then
            MessageBox.Show("Please select a user first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim success As Boolean = User.UpdateUser(User.SelectedUserId, txtFullname.Text, comboRole.Text, comboStatus.Text)
            If success Then
                MessageBox.Show("User successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear input fields
                txtFullname.Text = ""
                txtUsername.Text = ""
                txtpassword.Text = ""
                txtConfirmPassword.Text = ""
                comboRole.SelectedIndex = -1
                comboStatus.SelectedIndex = -1

                ' Reset selected ID
                User.SelectedUserId = 0

                ' Reload DataGridView
                LoadUsers()
            Else
                MessageBox.Show("Failed to update user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating user: " & ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' Check if a user is selected
        If User.SelectedUserId = 0 Then
            MessageBox.Show("Please select a user first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Show confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Try
                Dim success As Boolean = User.DeleteUser(User.SelectedUserId)
                If success Then
                    MessageBox.Show("User successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Clear input fields
                    txtFullname.Text = ""
                    txtUsername.Text = ""
                    txtpassword.Text = ""
                    txtConfirmPassword.Text = ""
                    comboRole.SelectedIndex = -1
                    comboStatus.SelectedIndex = -1

                    ' Reset selected ID
                    User.SelectedUserId = 0

                    ' Reload DataGridView
                    LoadUsers()
                Else
                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("Error deleting user: " & ex.Message)
            End Try
        End If
    End Sub

    

    
End Class