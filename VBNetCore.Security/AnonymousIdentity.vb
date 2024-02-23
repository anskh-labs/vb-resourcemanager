Imports System

Namespace VBNetCore.Security
    Public Class AnonymousIdentity
        Inherits UserIdentity
        Public Sub New()
            MyBase.New(0, String.Empty, Array.Empty(Of String)(), Array.Empty(Of String)())
        End Sub
    End Class
End Namespace
