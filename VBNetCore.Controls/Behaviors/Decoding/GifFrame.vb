Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifFrame
        Inherits GifBlock

        Private _Descriptor As VBNetCore.Controls.Behaviors.Decoding.GifImageDescriptor, _LocalColorTable As VBNetCore.Controls.Behaviors.Decoding.GifColor(), _Extensions As System.Collections.Generic.IList(Of VBNetCore.Controls.Behaviors.Decoding.GifExtension), _ImageData As VBNetCore.Controls.Behaviors.Decoding.GifImageData
        Friend Const ImageSeparator As Integer = &H2C

        Public Property Descriptor As GifImageDescriptor
            Get
                Return _Descriptor
            End Get
            Private Set(ByVal value As GifImageDescriptor)
                _Descriptor = value
            End Set
        End Property

        Public Property LocalColorTable As GifColor()
            Get
                Return _LocalColorTable
            End Get
            Private Set(ByVal value As GifColor())
                _LocalColorTable = value
            End Set
        End Property

        Public Property Extensions As IList(Of GifExtension)
            Get
                Return _Extensions
            End Get
            Private Set(ByVal value As IList(Of GifExtension))
                _Extensions = value
            End Set
        End Property

        Public Property ImageData As GifImageData
            Get
                Return _ImageData
            End Get
            Private Set(ByVal value As GifImageData)
                _ImageData = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.GraphicRendering
            End Get
        End Property

        Friend Shared Function ReadFrame(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean) As GifFrame
            Dim frame = New GifFrame()

            frame.Read(stream, controlExtensions, metadataOnly)

            Return frame
        End Function

        Private Sub Read(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean)
            ' Note: at this point, the Image Separator (0x2C) has already been read

            Descriptor = GifImageDescriptor.ReadImageDescriptor(stream)
            If Descriptor.HasLocalColorTable Then
                LocalColorTable = ReadColorTable(stream, Descriptor.LocalColorTableSize)
            End If
            ImageData = GifImageData.ReadImageData(stream, metadataOnly)
            Extensions = Enumerable.ToList(controlExtensions).AsReadOnly()
        End Sub
    End Class
End Namespace
