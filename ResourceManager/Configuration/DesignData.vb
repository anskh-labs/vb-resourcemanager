Imports VBNetCore.Cryptography
Imports ResourceManager.Models
Imports System.Collections.Generic

Namespace ResourceManager.Configuration
    Friend Class DesignData
        Public Shared Instance As DesignData = New DesignData()
        Private Sub New()
        End Sub
        Public ReadOnly Property Users As IList(Of User)
            Get
                Return New User() {New User() With {
        .ID = 1,
        .Name = "Administrator",
        .AccountName = "admin",
        .Password = EncryptionManager.Instance.EncryptString("admin", EncryptionManager.KEY)
                    }, New User() With {
        .ID = 2,
        .Name = "User",
        .AccountName = "user",
        .Password = EncryptionManager.Instance.EncryptString("user", EncryptionManager.KEY)
                }}
            End Get
        End Property

        Public ReadOnly Property UserRoles As IList(Of UserRole)
            Get
                Return New UserRole() {New UserRole() With {
.UserID = 1,
.RoleID = 1
}}
            End Get
        End Property

        Public ReadOnly Property Roles As IList(Of Role)
            Get
                Return New Role() {New Role() With {
.ID = 1,
.Name = "Admin",
.Description = "Role as Administrator"
                    }, New Role() With {
.ID = 2,
.Name = "User",
.Description = "Role as User"
}}
            End Get
        End Property

        Public ReadOnly Property Permissions As IList(Of Permission)
            Get
                Return New Permission() {New Permission() With {
.ID = 1,
.Name = VIEW_USER_PERMISSION,
.Description = "Permission for view user menu"
                    }, New Permission() With {
.ID = 2,
.Name = VIEW_PASSWORD_PERMISSION,
.Description = "Permission for view Password menu"
                    }, New Permission() With {
.ID = 3,
.Name = VIEW_REPOSITORY_PERMISSION,
.Description = "Permission for view repository menu"
                    }, New Permission() With {
.ID = 4,
.Name = VIEW_EBOOK_PERMISSION,
.Description = "Permission for view ebook menu"
                    }, New Permission() With {
.ID = 5,
.Name = VIEW_ARTICLE_PERMISSION,
.Description = "Permission for view article menu"
                    }, New Permission() With {
.ID = 6,
.Name = VIEW_ACTIVITY_PERMISSION,
.Description = "Permission for view activity menu"
                    }, New Permission() With {
.ID = 7,
.Name = VIEW_NOTE_PERMISSION,
.Description = "Permission for view note menu"
                    }, New Permission() With {
.ID = 8,
.Name = VIEW_TOOLS_PERMISSION,
.Description = "Permission for view tools menu"
                    }, New Permission() With {
.ID = 9,
.Name = ACTION_ADD_ACTIVITY,
.Description = "Permission for add activity"
                    }, New Permission() With {
.ID = 10,
.Name = ACTION_ADD_ARTICLE,
.Description = "Permission for add article"
                    }, New Permission() With {
.ID = 11,
.Name = ACTION_ADD_EBOOK,
.Description = "Permission for add ebook"
                    }, New Permission() With {
.ID = 12,
.Name = ACTION_ADD_NOTE,
.Description = "Permission for add note"
                    }, New Permission() With {
.ID = 13,
.Name = ACTION_ADD_PASSWORD,
.Description = "Permission for add password"
                    }, New Permission() With {
.ID = 14,
.Name = ACTION_ADD_PERMISSION,
.Description = "Permission for delete add permission"
                    }, New Permission() With {
.ID = 15,
.Name = ACTION_ADD_REPOSITORY,
.Description = "Permission for add repository"
                    }, New Permission() With {
.ID = 16,
.Name = ACTION_ADD_ROLE,
.Description = "Permission for add role"
                    }, New Permission() With {
.ID = 17,
.Name = ACTION_ADD_TAG,
.Description = "Permission for add tag"
}, New Permission() With {
.ID = 18,
.Name = ACTION_ADD_USER,
.Description = "Permission for add user"
}, New Permission() With {
.ID = 19,
.Name = ACTION_DEL_ACTIVITY,
.Description = "Permission for delete activity"
}, New Permission() With {
.ID = 20,
.Name = ACTION_DEL_ARTICLE,
.Description = "Permission for delete article"
}, New Permission() With {
.ID = 21,
.Name = ACTION_DEL_EBOOK,
.Description = "Permission for delete ebook"
}, New Permission() With {
.ID = 22,
.Name = ACTION_DEL_NOTE,
.Description = "Permission for delete note"
}, New Permission() With {
.ID = 23,
.Name = ACTION_DEL_PASSWORD,
.Description = "Permission for delete password"
}, New Permission() With {
.ID = 24,
.Name = ACTION_DEL_PERMISSION,
.Description = "Permission for delete permission"
}, New Permission() With {
.ID = 25,
.Name = ACTION_DEL_REPOSITORY,
.Description = "Permission for delete repository"
}, New Permission() With {
.ID = 26,
.Name = ACTION_DEL_ROLE,
.Description = "Permission for delete role"
}, New Permission() With {
.ID = 27,
.Name = ACTION_DEL_TAG,
.Description = "Permission for delete tag"
}, New Permission() With {
.ID = 28,
.Name = ACTION_DEL_USER,
.Description = "Permission for delete user"
}, New Permission() With {
.ID = 29,
.Name = ACTION_EDIT_ACTIVITY,
.Description = "Permission for edit activity"
}, New Permission() With {
.ID = 30,
.Name = ACTION_EDIT_ARTICLE,
.Description = "Permission for edit article"
}, New Permission() With {
.ID = 31,
.Name = ACTION_EDIT_EBOOK,
.Description = "Permission for edit ebook"
}, New Permission() With {
.ID = 32,
.Name = ACTION_EDIT_NOTE,
.Description = "Permission for edit note"
}, New Permission() With {
.ID = 33,
.Name = ACTION_EDIT_PASSWORD,
.Description = "Permission for edit password"
}, New Permission() With {
.ID = 34,
.Name = ACTION_EDIT_PERMISSION,
.Description = "Permission for edit permission"
}, New Permission() With {
.ID = 35,
.Name = ACTION_EDIT_REPOSITORY,
.Description = "Permission for edit repository"
}, New Permission() With {
.ID = 36,
.Name = ACTION_EDIT_ROLE,
.Description = "Permission for edit role"
}, New Permission() With {
.ID = 37,
.Name = ACTION_EDIT_TAG,
.Description = "Permission for edit tag"
}, New Permission() With {
.ID = 38,
.Name = ACTION_EDIT_USER,
.Description = "Permission for edit user"
}}
            End Get
        End Property

        Public ReadOnly Property RolePermissions As IList(Of RolePermission)
            Get
                Return New RolePermission() {
                    New RolePermission() With {
                    .RoleID = 1,
                    .PermissionID = 1
                    }, New RolePermission() With {
                    .RoleID = 1,
                    .PermissionID = 2
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 3
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 4
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 5
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 6
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 7
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 8
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 9
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 10
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 11
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 12
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 13
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 14
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 15
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 16
                    }, New RolePermission() With {
.RoleID = 1,
.PermissionID = 17
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 18
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 19
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 20
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 21
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 22
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 23
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 24
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 25
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 26
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 27
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 28
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 29
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 30
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 31
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 32
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 33
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 34
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 35
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 36
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 37
}, New RolePermission() With {
.RoleID = 1,
.PermissionID = 38
}}
            End Get
        End Property
    End Class
End Namespace
