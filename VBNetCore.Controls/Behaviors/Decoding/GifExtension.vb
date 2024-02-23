Imports System.Collections.Generic
Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend MustInherit Class GifExtension
        Inherits GifBlock
        Friend Const ExtensionIntroducer As Integer = &H21

        Friend Shared Function ReadExtension(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean) As GifExtension
            ' Note: at this point, the Extension Introducer (0x21) has already been read

            Dim label As Integer = stream.ReadByte()
            If label < 0 Then Throw UnexpectedEndOfStreamException()
            Select Case label
                Case GifGraphicControlExtension.ExtensionLabel
                    Return GifGraphicControlExtension.ReadGraphicsControl(stream)
                Case GifCommentExtension.ExtensionLabel
                    Return GifCommentExtension.ReadComment(stream)
                Case GifPlainTextExtension.ExtensionLabel
                    Return GifPlainTextExtension.ReadPlainText(stream, controlExtensions, metadataOnly)
                Case GifApplicationExtension.ExtensionLabel
                    Return GifApplicationExtension.ReadApplication(stream)
                Case Else
                    Throw UnknownExtensionTypeException(label)
            End Select
        End Function
    End Class
End Namespace
