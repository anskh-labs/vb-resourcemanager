Imports System.IO
Imports System.Text

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifCommentExtension
        Inherits GifExtension

        Private _Text As String
        Friend Const ExtensionLabel As Integer = &HFE

        Public Property Text As String
            Get
                Return _Text
            End Get
            Private Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.SpecialPurpose
            End Get
        End Property

        Friend Shared Function ReadComment(ByVal stream As Stream) As GifCommentExtension
            Dim comment = New GifCommentExtension()
            comment.Read(stream)
            Return comment
        End Function

        Private Sub Read(ByVal stream As Stream)
            ' Note: at this point, the label (0xFE) has already been read

            Dim bytes = ReadDataBlocks(stream, False)
            If bytes IsNot Nothing Then Text = Encoding.ASCII.GetString(bytes)
        End Sub
    End Class
End Namespace
