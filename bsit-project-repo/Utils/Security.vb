Imports System.Security.Cryptography
Imports System.Text

Public Class Security
    Public Shared Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim sb As New StringBuilder()
            For Each b As Byte In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return "$25" & sb.ToString()
        End Using
    End Function
End Class
