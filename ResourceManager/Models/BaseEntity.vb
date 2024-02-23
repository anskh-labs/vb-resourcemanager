Imports ResourceManager.Models.Abstractions

Namespace ResourceManager.Models
    Public Class BaseEntity
        Implements IEntity
        Public Property ID As Integer Implements IEntity.ID
        Public Property CreatedDate As Date
        Public Property ModifiedDate As Date
        Public Overridable Function GetCaption() As String Implements IEntity.GetCaption
            Return String.Format("{0} with ID {1}", [GetType]().Name, ID)
        End Function
    End Class
End Namespace
