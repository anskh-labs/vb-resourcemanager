
Namespace ResourceManager.Helpers
    Friend Module FilterManager
        Public Function Create(Of T As Class)() As Filter(Of T)
            Return New Filter(Of T)()
        End Function

    End Module
End Namespace
