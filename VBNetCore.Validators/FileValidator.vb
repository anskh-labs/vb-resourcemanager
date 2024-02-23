Imports System.IO

Namespace VBNetCore.Validators
    Public Class FileValidator
        Private Shared ReadOnly _instance As FileValidator = New FileValidator()
        Public Shared ReadOnly Property Instance As FileValidator
            Get
                Return _instance
            End Get
        End Property
        Public Function Filter(ByVal FileName As String) As String
            Dim invalid As String = New String(Path.GetInvalidFileNameChars) & New String(Path.GetInvalidPathChars)

            For Each c As Char In invalid
                FileName = FileName.Replace(c.ToString, "")
            Next
            Return FileName
        End Function
    End Class
End Namespace