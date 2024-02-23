Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifImageData
        Public Property LzwMinimumCodeSize As Byte
        Public Property CompressedData As Byte()

        Private Sub New()
        End Sub

        Friend Shared Function ReadImageData(ByVal stream As Stream, ByVal metadataOnly As Boolean) As GifImageData
            Dim imgData = New GifImageData()
            imgData.Read(stream, metadataOnly)
            Return imgData
        End Function

        Private Sub Read(ByVal stream As Stream, ByVal metadataOnly As Boolean)
            LzwMinimumCodeSize = CByte(stream.ReadByte())
            CompressedData = ReadDataBlocks(stream, metadataOnly)
        End Sub
    End Class
End Namespace
