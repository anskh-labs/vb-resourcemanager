Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Structure GifColor
        Private ReadOnly _r As Byte
        Private ReadOnly _g As Byte
        Private ReadOnly _b As Byte

        Friend Sub New(ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
            _r = r
            _g = g
            _b = b
        End Sub

        Public ReadOnly Property R As Byte
            Get
                Return _r
            End Get
        End Property
        Public ReadOnly Property G As Byte
            Get
                Return _g
            End Get
        End Property
        Public ReadOnly Property B As Byte
            Get
                Return _b
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("#{0:x2}{1:x2}{2:x2}", _r, _g, _b)
        End Function
    End Structure
End Namespace
