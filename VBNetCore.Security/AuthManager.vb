Imports System
Imports System.Security.Principal
Imports System.Threading

Namespace VBNetCore.Security
    Public Class AuthManager
        Public Shared Sub SetCurrentPrincipal(ByVal Optional _principal As IPrincipal = Nothing)
            AppDomain.CurrentDomain.SetThreadPrincipal(If(_principal, New UserPrincipal()))
        End Sub
        Public Shared ReadOnly Property User As UserPrincipal
            Get
                Dim principal = TryCast(Thread.CurrentPrincipal, UserPrincipal)
                If principal Is Nothing Then Throw New ArgumentException("The application's default thread principal must be set to a UserPrincipal object on startup.")

                Return principal
            End Get
        End Property
    End Class
End Namespace
