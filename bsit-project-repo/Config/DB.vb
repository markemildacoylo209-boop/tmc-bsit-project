Imports System.IO
Imports MySql.Data.MySqlClient

Public Class DB
    Private Shared connection As MySqlConnection

    ' Load connection string from config.env
    Private Shared Function GetConnectionString() As String
        Dim configPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.env")
        Dim config As New Dictionary(Of String, String)

        ' Read the config.env
        If File.Exists(configPath) Then
            For Each line As String In File.ReadAllLines(configPath)
                If line.Trim() = "" OrElse line.StartsWith("#") Then Continue For

                Dim index As Integer = line.IndexOf("="c)
                If index > -1 Then
                    Dim key As String = line.Substring(0, index).Trim()
                    Dim value As String = line.Substring(index + 1).Trim()
                    config(key) = value
                End If
            Next
        Else
            Throw New FileNotFoundException("config.env file not found at: " & configPath)
        End If

        ' Create MySQL connection string
        Dim connString As String = "Server=" & config("ENDPOINT_CONNECTIT") & _
                                   ";Port=" & config("PORT_CONNECTIT") & _
                                   ";User ID=" & config("USER_CONNECTIT") & _
                                   ";Password=" & config("PASSWORD_CONNECTIT") & _
                                   ";Database=" & config("DATABASE_CONNECTIT_LOGS") & ";"

        Return connString
    End Function

    Public Shared Function GetConnection() As MySqlConnection
        If connection Is Nothing Then
            connection = New MySqlConnection(GetConnectionString())
        End If
        Return connection
    End Function
End Class
