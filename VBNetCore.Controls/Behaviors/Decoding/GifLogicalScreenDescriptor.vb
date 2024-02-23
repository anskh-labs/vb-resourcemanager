Imports System
Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifLogicalScreenDescriptor
        Private _Width As Integer, _Height As Integer, _HasGlobalColorTable As Boolean, _ColorResolution As Integer, _IsGlobalColorTableSorted As Boolean, _GlobalColorTableSize As Integer, _BackgroundColorIndex As Integer, _PixelAspectRatio As Double

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

        Public Property HasGlobalColorTable As Boolean
            Get
                Return _HasGlobalColorTable
            End Get
            Private Set(ByVal value As Boolean)
                _HasGlobalColorTable = value
            End Set
        End Property

        Public Property ColorResolution As Integer
            Get
                Return _ColorResolution
            End Get
            Private Set(ByVal value As Integer)
                _ColorResolution = value
            End Set
        End Property

        Public Property IsGlobalColorTableSorted As Boolean
            Get
                Return _IsGlobalColorTableSorted
            End Get
            Private Set(ByVal value As Boolean)
                _IsGlobalColorTableSorted = value
            End Set
        End Property

        Public Property GlobalColorTableSize As Integer
            Get
                Return _GlobalColorTableSize
            End Get
            Private Set(ByVal value As Integer)
                _GlobalColorTableSize = value
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

        Public Property PixelAspectRatio As Double
            Get
                Return _PixelAspectRatio
            End Get
            Private Set(ByVal value As Double)
                _PixelAspectRatio = value
            End Set
        End Property

        Friend Shared Function ReadLogicalScreenDescriptor(ByVal stream As Stream) As GifLogicalScreenDescriptor
            Dim descriptor = New GifLogicalScreenDescriptor()
            descriptor.Read(stream)
            Return descriptor
        End Function

        Private Sub Read(ByVal stream As Stream)
            Dim bytes = New Byte(6) {}
            stream.ReadAll(bytes, 0, bytes.Length)

            Width = BitConverter.ToUInt16(bytes, 0)
            Height = BitConverter.ToUInt16(bytes, 2)
            Dim packedFields = bytes(4)
            HasGlobalColorTable = (packedFields And &H80) <> 0
            ColorResolution = ((packedFields And &H70) >> 4) + 1
            IsGlobalColorTableSorted = (packedFields And &H08) <> 0
            GlobalColorTableSize = 1 << (packedFields And &H07) + 1
            BackgroundColorIndex = bytes(5)
            PixelAspectRatio = If(bytes(5) = 0, 0.0, (15 + bytes(5)) / 64.0)
        End Sub
    End Class
End Namespace
