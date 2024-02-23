Namespace ResourceManager.Models
    Public Class UserRole
        Public Property UserID As Integer
        Public Property RoleID As Integer

        'Relationships
        Public Property User As User
        Public Property Role As Role
    End Class
End Namespace
