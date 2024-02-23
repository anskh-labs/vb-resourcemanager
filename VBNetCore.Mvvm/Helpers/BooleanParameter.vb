Namespace VBNetCore.Mvvm.Helpers
    ''' <summary>
    ''' Defines a type that can be used in xaml to define static bool values.
    ''' </summary>
    Public Module BooleanParameter

        ''' <summary>
        ''' Represents a true value
        ''' </summary>
        Public ReadOnly Property [True] As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Represents a false value
        ''' </summary>
        Public ReadOnly Property [False] As Boolean
            Get
                Return False
            End Get
        End Property

    End Module
End Namespace
