Public Class Main


    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        'LoadFormInPanel(New UserManageForm())
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    ' Help menu Click
    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    ' Attendance
    Private Sub ManageAttendanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageAttendanceToolStripMenuItem.Click

    End Sub

    'Student Manage
    Private Sub StudentManageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StudentManageToolStripMenuItem.Click

    End Sub

    'Year
    Private Sub YearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YearToolStripMenuItem.Click

    End Sub

    ' Block

    Private Sub BlockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlockToolStripMenuItem.Click

    End Sub


    ' Course
    Private Sub CourseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CourseToolStripMenuItem.Click

    End Sub

    'UserManage
    Private Sub UserManageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserManageToolStripMenuItem.Click
        LoadFormInPanel(New UserManageForm())
    End Sub

    ' Logout
    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        Dim login As New LoginForm
        login.ShowDialog()
        Me.Close()

    End Sub


    Public Sub LoadFormInPanel(ByVal frm As Form)
        PanelContainer.Controls.Clear() ' Clean panel first
        frm.TopLevel = False            ' Make a child
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill       ' Full panel
        PanelContainer.Controls.Add(frm)
        frm.Show()
    End Sub



End Class