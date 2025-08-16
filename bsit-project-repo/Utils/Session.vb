Public Class Session
    Private Shared _UserID As Integer
    Private Shared _Username As String
    Private Shared _Fullname As String
    Private Shared _Role As String

    Public Shared Property UserID() As Integer
        Get
            Return _UserID
        End Get
        Set(ByVal value As Integer)
            _UserID = value
        End Set
    End Property

    Public Shared Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property

    Public Shared Property Fullname() As String
        Get
            Return _Fullname
        End Get
        Set(ByVal value As String)
            _Fullname = value
        End Set
    End Property

    Public Shared Property Role() As String
        Get
            Return _Role
        End Get
        Set(ByVal value As String)
            _Role = value
        End Set
    End Property

    Public Shared Sub Clear()
        _UserID = 0
        _Username = Nothing
        _Fullname = Nothing
        _Role = Nothing
    End Sub
End Class
