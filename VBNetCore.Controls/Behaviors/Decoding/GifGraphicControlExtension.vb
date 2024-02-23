Imports System
Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    ' label 0xF9
    Friend Class GifGraphicControlExtension
        Inherits GifExtension

        Private _BlockSize As Integer, _DisposalMethod As Integer, _UserInput As Boolean, _HasTransparency As Boolean, _Delay As Integer, _TransparencyIndex As Integer
        Friend Const ExtensionLabel As Integer = &HF9

        Public Property BlockSize As Integer
            Get
                Return _BlockSize
            End Get
            Private Set(ByVal value As Integer)
                _BlockSize = value
            End Set
        End Property

        Public Property DisposalMethod As Integer
            Get
                Return _DisposalMethod
            End Get
            Private Set(ByVal value As Integer)
                _DisposalMethod = value
            End Set
        End Property

        Public Property UserInput As Boolean
            Get
                Return _UserInput
            End Get
            Private Set(ByVal value As Boolean)
                _UserInput = value
            End Set
        End Property

        Public Property HasTransparency As Boolean
            Get
                Return _HasTransparency
            End Get
            Private Set(ByVal value As Boolean)
                _HasTransparency = value
            End Set
        End Property

        Public Property Delay As Integer
            Get
                Return _Delay
            End Get
            Private Set(ByVal value As Integer)
                _Delay = value
            End Set
        End Property

        Public Property TransparencyIndex As Integer
            Get
                Return _TransparencyIndex
            End Get
            Private Set(ByVal value As Integer)
                _TransparencyIndex = value
            End Set
        End Property

        Private Sub New()

        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.Control
            End Get
        End Property

        Friend Shared Function ReadGraphicsControl(ByVal stream As Stream) As GifGraphicControlExtension
            Dim ext = New GifGraphicControlExtension()
            ext.Read(stream)
            Return ext
        End Function

        Private Sub Read(ByVal stream As Stream)
            ' Note: at this point, the label (0xF9) has already been read

            Dim bytes = New Byte(5) {}
            stream.ReadAll(bytes, 0, bytes.Length)
            BlockSize = bytes(0) ' should always be 4
            If BlockSize <> 4 Then Throw InvalidBlockSizeException("Graphic Control Extension", 4, BlockSize)
            Dim packedFields = bytes(1)
            DisposalMethod = (packedFields And &H1C) >> 2
            UserInput = (packedFields And &H02) <> 0
            HasTransparency = (packedFields And &H01) <> 0
            Delay = BitConverter.ToUInt16(bytes, 2) * 10 ' milliseconds
            TransparencyIndex = bytes(4)
        End Sub
    End Class
End Namespace
