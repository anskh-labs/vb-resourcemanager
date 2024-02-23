Imports System
Imports System.IO
Imports System.Text
Imports System.Runtime.CompilerServices

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Module GifHelpers
        Public Function ReadString(ByVal stream As Stream, ByVal length As Integer) As String
            Dim bytes = New Byte(length - 1) {}
            stream.ReadAll(bytes, 0, length)
            Return Encoding.ASCII.GetString(bytes)
        End Function

        Public Function ReadDataBlocks(ByVal stream As Stream, ByVal discard As Boolean) As Byte()
            Dim ms As MemoryStream = If(discard, Nothing, New MemoryStream())
            Using ms
                Dim len As Integer = stream.ReadByte()
                While len > 0
                    Dim bytes = New Byte(len - 1) {}
                    stream.ReadAll(bytes, 0, len)
                    If ms IsNot Nothing Then ms.Write(bytes, 0, len)
                    len = stream.ReadByte()
                End While
                If ms IsNot Nothing Then Return ms.ToArray()
                Return Nothing
            End Using
        End Function

        Public Function ReadColorTable(ByVal stream As Stream, ByVal size As Integer) As GifColor()
            Dim length = 3 * size
            Dim bytes = New Byte(length - 1) {}
            stream.ReadAll(bytes, 0, length)
            Dim colorTable = New GifColor(size - 1) {}
            For i = 0 To size - 1
                Dim r = bytes(3 * i)
                Dim g = bytes(3 * i + 1)
                Dim b = bytes(3 * i + 2)
                colorTable(i) = New GifColor(r, g, b)
            Next
            Return colorTable
        End Function

        Public Function IsNetscapeExtension(ByVal ext As GifApplicationExtension) As Boolean
            Return Equals(ext.ApplicationIdentifier, "NETSCAPE") AndAlso Equals(Encoding.ASCII.GetString(ext.AuthenticationCode), "2.0")
        End Function

        Public Function GetRepeatCount(ByVal ext As GifApplicationExtension) As UShort
            If ext.Data.Length >= 3 Then
                Return BitConverter.ToUInt16(ext.Data, 1)
            End If
            Return 1
        End Function

        Public Function UnexpectedEndOfStreamException() As Exception
            Return New GifDecoderException("Unexpected end of stream before trailer was encountered")
        End Function

        Public Function UnknownBlockTypeException(ByVal blockId As Integer) As Exception
            Return New GifDecoderException("Unknown block type: 0x" & blockId.ToString("x2"))
        End Function

        Public Function UnknownExtensionTypeException(ByVal extensionLabel As Integer) As Exception
            Return New GifDecoderException("Unknown extension type: 0x" & extensionLabel.ToString("x2"))
        End Function

        Public Function InvalidBlockSizeException(ByVal blockName As String, ByVal expectedBlockSize As Integer, ByVal actualBlockSize As Integer) As Exception
            Return New GifDecoderException(String.Format("Invalid block size for {0}. Expected {1}, but was {2}", blockName, expectedBlockSize, actualBlockSize))
        End Function

        Public Function InvalidSignatureException(ByVal signature As String) As Exception
            Return New GifDecoderException("Invalid file signature: " & signature)
        End Function

        Public Function UnsupportedVersionException(ByVal version As String) As Exception
            Return New GifDecoderException("Unsupported version: " & version)
        End Function

        <Extension()>
        Public Sub ReadAll(ByVal stream As Stream, ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer)
            Dim totalRead = 0
            While totalRead < count
                totalRead += stream.Read(buffer, offset + totalRead, count - totalRead)
            End While
        End Sub

    End Module
End Namespace
