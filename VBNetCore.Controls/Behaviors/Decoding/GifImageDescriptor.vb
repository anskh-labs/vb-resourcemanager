Imports System
Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifImageDescriptor
        Private _Left As Integer, _Top As Integer, _Width As Integer, _Height As Integer, _HasLocalColorTable As Boolean, _Interlace As Boolean, _IsLocalColorTableSorted As Boolean, _LocalColorTableSize As Integer

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

        Public Property HasLocalColorTable As Boolean
            Get
                Return _HasLocalColorTable
            End Get
            Private Set(ByVal value As Boolean)
                _HasLocalColorTable = value
            End Set
        End Property

        Public Property Interlace As Boolean
            Get
                Return _Interlace
            End Get
            Private Set(ByVal value As Boolean)
                _Interlace = value
            End Set
        End Property

        Public Property IsLocalColorTableSorted As Boolean
            Get
                Return _IsLocalColorTableSorted
            End Get
            Private Set(ByVal value As Boolean)
                _IsLocalColorTableSorted = value
            End Set
        End Property

        Public Property LocalColorTableSize As Integer
            Get
                Return _LocalColorTableSize
            End Get
            Private Set(ByVal value As Integer)
                _LocalColorTableSize = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Friend Shared Function ReadImageDescriptor(ByVal stream As Stream) As GifImageDescriptor
            Dim descriptor = New GifImageDescriptor()
            descriptor.Read(stream)
            Return descriptor
        End Function

        Private Sub Read(ByVal stream As Stream)
            Dim bytes = New Byte(8) {}
            stream.ReadAll(bytes, 0, bytes.Length)
            Left = BitConverter.ToUInt16(bytes, 0)
            Top = BitConverter.ToUInt16(bytes, 2)
            Width = BitConverter.ToUInt16(bytes, 4)
            Height = BitConverter.ToUInt16(bytes, 6)
            Dim packedFields = bytes(8)
            HasLocalColorTable = (packedFields And &H80) <> 0
            Interlace = (packedFields And &H40) <> 0
            IsLocalColorTableSorted = (packedFields And &H20) <> 0
            LocalColorTableSize = 1 << (packedFields And &H07) + 1
        End Sub
    End Class
End Namespace
