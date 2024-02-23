Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifFile
        Private _Header As VBNetCore.Controls.Behaviors.Decoding.GifHeader

        Public Property Header As GifHeader
            Get
                Return _Header
            End Get
            Private Set(ByVal value As GifHeader)
                _Header = value
            End Set
        End Property
        Public Property GlobalColorTable As GifColor()
        Public Property Frames As IList(Of GifFrame)
        Public Property Extensions As IList(Of GifExtension)
        Public Property RepeatCount As UShort

        Private Sub New()
        End Sub

        Friend Shared Function ReadGifFile(ByVal stream As Stream, ByVal metadataOnly As Boolean) As GifFile
            Dim file = New GifFile()
            file.Read(stream, metadataOnly)
            Return file
        End Function

        Private Sub Read(ByVal stream As Stream, ByVal metadataOnly As Boolean)
            Header = GifHeader.ReadHeader(stream)

            If Header.LogicalScreenDescriptor.HasGlobalColorTable Then
                GlobalColorTable = ReadColorTable(stream, Header.LogicalScreenDescriptor.GlobalColorTableSize)
            End If
            ReadFrames(stream, metadataOnly)

            Dim netscapeExtension = Extensions.OfType(Of GifApplicationExtension)().FirstOrDefault(New Func(Of GifApplicationExtension, Boolean)(AddressOf IsNetscapeExtension))

            If netscapeExtension IsNot Nothing Then
                RepeatCount = GetRepeatCount(netscapeExtension)
            Else
                RepeatCount = 1
            End If
        End Sub

        Private Sub ReadFrames(ByVal stream As Stream, ByVal metadataOnly As Boolean)
            Dim frames As List(Of GifFrame) = New List(Of GifFrame)()
            Dim controlExtensions As List(Of GifExtension) = New List(Of GifExtension)()
            Dim specialExtensions As List(Of GifExtension) = New List(Of GifExtension)()
            While True
                Dim block = GifBlock.ReadBlock(stream, controlExtensions, metadataOnly)

                If block.Kind = GifBlockKind.GraphicRendering Then controlExtensions = New List(Of GifExtension)()

                If TypeOf block Is GifFrame Then
                    frames.Add(CType(block, GifFrame))
                ElseIf TypeOf block Is GifExtension Then
                    Dim extension = CType(block, GifExtension)
                    Select Case extension.Kind
                        Case GifBlockKind.Control
                            controlExtensions.Add(extension)
                        Case GifBlockKind.SpecialPurpose
                            specialExtensions.Add(extension)
                    End Select
                ElseIf TypeOf block Is GifTrailer Then
                    Exit While
                End If
            End While

            Me.Frames = frames.AsReadOnly()
            Extensions = specialExtensions.AsReadOnly()
        End Sub
    End Class
End Namespace
