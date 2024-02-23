Namespace VBNetCore.Mvvm.Controls
    Public Class ValidationError

        Private _Message As String, _Level As VBNetCore.Mvvm.Controls.ErrorLevel
        Public Sub New(ByVal message As String, ByVal errorLevel As ErrorLevel)
            Me.Message = message
            Level = errorLevel
        End Sub

        Public Property Message As String
            Get
                Return _Message
            End Get
            Private Set(ByVal value As String)
                _Message = value
            End Set
        End Property

        Public Property Level As ErrorLevel
            Get
                Return _Level
            End Get
            Private Set(ByVal value As ErrorLevel)
                _Level = value
            End Set
        End Property
    End Class
End Namespace
