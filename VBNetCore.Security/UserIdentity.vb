Imports System.Security.Principal

Namespace VBNetCore.Security
    Public Class UserIdentity
        Implements IIdentity
        Private _ID As Integer, _Name As String, _Permissions As String(), _Roles As String()
        Public Sub New(ByVal id As Integer, ByVal name As String, ByVal roles As String(), ByVal permissions As String())
            Me.ID = id
            Me.Name = name
            Me.Roles = roles
            Me.Permissions = permissions
        End Sub

        Public Property ID As Integer
            Get
                Return _ID
            End Get
            Private Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Public Property Name As String Implements IIdentity.Name
            Get
                Return _Name
            End Get
            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Permissions As String()
            Get
                Return _Permissions
            End Get
            Private Set(ByVal value As String())
                _Permissions = value
            End Set
        End Property

        Public Property Roles As String()
            Get
                Return _Roles
            End Get
            Private Set(ByVal value As String())
                _Roles = value
            End Set
        End Property

#Region "IIdentity Members"
        Public ReadOnly Property AuthenticationType As String Implements IIdentity.AuthenticationType
            Get
                Return "User Authentication"
            End Get
        End Property

        Public ReadOnly Property IsAuthenticated As Boolean Implements IIdentity.IsAuthenticated
            Get
                Return Not String.IsNullOrEmpty(Name)
            End Get
        End Property
#End Region
    End Class
End Namespace
