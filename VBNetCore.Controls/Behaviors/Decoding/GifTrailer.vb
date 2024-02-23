Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifTrailer
        Inherits GifBlock
        Friend Const TrailerByte As Integer = &H3B

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.Other
            End Get
        End Property

        Friend Shared Function ReadTrailer() As GifTrailer
            Return New GifTrailer()
        End Function
    End Class
End Namespace
