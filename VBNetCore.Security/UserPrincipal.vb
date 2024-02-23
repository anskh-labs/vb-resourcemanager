Imports System.Linq
Imports System.Security.Principal

Namespace VBNetCore.Security
    Public Class UserPrincipal
        Implements IPrincipal
        Private _identity As UserIdentity
        Public Property Identity As UserIdentity
            Get
                Return If(_identity, Function()
                                         _identity = New AnonymousIdentity()
                                         Return _identity
                                     End Function())
            End Get
            Set(ByVal value As UserIdentity)
                _identity = value
            End Set
        End Property
        Private ReadOnly Property PIdentity As IIdentity Implements IPrincipal.Identity
            Get
                Return Identity
            End Get
        End Property

        Public Function IsInRole(ByVal role As String) As Boolean Implements IPrincipal.IsInRole
            Return _identity.Roles.Contains(role)
        End Function
        Public Function IsInPermission(ByVal permission As String) As Boolean
            Return _identity.Permissions.Contains(permission)
        End Function
    End Class
End Namespace
