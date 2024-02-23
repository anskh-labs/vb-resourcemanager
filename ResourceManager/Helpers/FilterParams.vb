
Namespace ResourceManager.Helpers
    ''' <summary>  
    ''' Filter parameters Model Class  
    ''' </summary>  
    Public Class FilterParams
        Public Property ColumnName As String = String.Empty
        Public Property FilterValue As String = String.Empty
        Public Property FilterOption As FilterOptions = FilterOptions.Contains
    End Class
End Namespace
