Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text

Namespace VBNetCore.Controls.Behaviors.Decoding
    ' label 0x01
    Friend Class GifPlainTextExtension
        Inherits GifExtension


        Private _BlockSize As Integer, _Left As Integer, _Top As Integer, _Width As Integer, _Height As Integer, _CellWidth As Integer, _CellHeight As Integer, _ForegroundColorIndex As Integer, _BackgroundColorIndex As Integer, _Text As String, _Extensions As System.Collections.Generic.IList(Of VBNetCore.Controls.Behaviors.Decoding.GifExtension)
        Friend Const ExtensionLabel As Integer = &H01

        Public Property BlockSize As Integer
            Get
                Return _BlockSize
            End Get
            Private Set(ByVal value As Integer)
                _BlockSize = value
            End Set
        End Property

        Public Property Left As Integer
            Get
                Return _Left
            End Get
            Private Set(ByVal value As Integer)
                _Left = value
            End Set
        End Property

        Public Property Top As Integer
            Get
                Return _Top
            End Get
            Private Set(ByVal value As Integer)
                _Top = value
            End Set
        End Property

        Public Property Width As Integer
            Get
                Return _Width
            End Get
            Private Set(ByVal value As Integer)
                _Width = value
            End Set
        End Property

        Public Property Height As Integer
            Get
                Return _Height
            End Get
            Private Set(ByVal value As Integer)
                _Height = value
            End Set
        End Property

        Public Property CellWidth As Integer
            Get
                Return _CellWidth
            End Get
            Private Set(ByVal value As Integer)
                _CellWidth = value
            End Set
        End Property

        Public Property CellHeight As Integer
            Get
                Return _CellHeight
            End Get
            Private Set(ByVal value As Integer)
                _CellHeight = value
            End Set
        End Property

        Public Property ForegroundColorIndex As Integer
            Get
                Return _ForegroundColorIndex
            End Get
            Private Set(ByVal value As Integer)
                _ForegroundColorIndex = value
            End Set
        End Property

        Public Property BackgroundColorIndex As Integer
            Get
                Return _BackgroundColorIndex
            End Get
            Private Set(ByVal value As Integer)
                _BackgroundColorIndex = value
            End Set
        End Property

        Public Property Text As String
            Get
                Return _Text
            End Get
            Private Set(ByVal value As String)
                _Text = value
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

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.GraphicRendering
            End Get
        End Property

        Friend Shared Function ReadPlainText(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean) As GifPlainTextExtension
            Dim plainText = New GifPlainTextExtension()
            plainText.Read(stream, controlExtensions, metadataOnly)
            Return plainText
        End Function

        Private Sub Read(ByVal stream As Stream, ByVal controlExtensions As IEnumerable(Of GifExtension), ByVal metadataOnly As Boolean)
            ' Note: at this point, the label (0x01) has already been read

            Dim bytes = New Byte(12) {}
            stream.ReadAll(bytes, 0, bytes.Length)

            BlockSize = bytes(0)
            If BlockSize <> 12 Then Throw InvalidBlockSizeException("Plain Text Extension", 12, BlockSize)

            Left = BitConverter.ToUInt16(bytes, 1)
            Top = BitConverter.ToUInt16(bytes, 3)
            Width = BitConverter.ToUInt16(bytes, 5)
            Height = BitConverter.ToUInt16(bytes, 7)
            CellWidth = bytes(9)
            CellHeight = bytes(10)
            ForegroundColorIndex = bytes(11)
            BackgroundColorIndex = bytes(12)

            Dim dataBytes = ReadDataBlocks(stream, metadataOnly)
            Text = Encoding.ASCII.GetString(dataBytes)
            Extensions = Enumerable.ToList(controlExtensions).AsReadOnly()
        End Sub
    End Class
End Namespace
