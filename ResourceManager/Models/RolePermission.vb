Namespace ResourceManager.Models
    Public Class RolePermission
        Public Property RoleID As Integer
        Public Property PermissionID As Integer

        ' Relationships
        Public Property Role As Role
        Public Property Permission As Permission
    End Class
End Namespace
