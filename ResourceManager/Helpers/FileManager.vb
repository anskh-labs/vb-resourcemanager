Imports System.Diagnostics
Imports System.IO

Namespace ResourceManager.Helpers
    Friend Class FileManager
        Private Shared ReadOnly _instance As FileManager = New FileManager()
        Private Sub New()
        End Sub
        Public Shared ReadOnly Property Instance As FileManager
            Get
                Return _instance
            End Get
        End Property

        Public Function ExploreFile(ByVal filePath As String) As Boolean
            If Not File.Exists(filePath) Then
                Return False
            End If
            'Clean up file path so it can be navigated OK
            filePath = Path.GetFullPath(filePath)
            Process.Start("explorer.exe", String.Format("/select,""{0}""", filePath))
            Return True
        End Function
    End Class
End Namespace
