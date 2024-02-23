Imports System.Collections.Generic
Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend MustInherit Class GifBlock
        Friend Shared Function ReadBlock(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean) As GifBlock
            Dim blockId As Integer = stream.ReadByte()
            If blockId < 0 Then Throw UnexpectedEndOfStreamException()
            Select Case blockId
                Case GifExtension.ExtensionIntroducer
                    Return GifExtension.ReadExtension(stream, controlExtensions, metadataOnly)
                Case GifFrame.ImageSeparator
                    Return GifFrame.ReadFrame(stream, controlExtensions, metadataOnly)
                Case GifTrailer.TrailerByte
                    Return GifTrailer.ReadTrailer()
                Case Else
                    Throw UnknownBlockTypeException(blockId)
            End Select
        End Function

        Friend MustOverride ReadOnly Property Kind As GifBlockKind
    End Class
End Namespace
