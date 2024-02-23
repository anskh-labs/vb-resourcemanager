Imports System

Namespace VBNetCore.Controls.Behaviors.Decoding
    <Serializable>
    Friend Class GifDecoderException
        Inherits Exception
        Friend Sub New()
        End Sub
        Friend Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
        Friend Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub
        Protected Sub New(ByVal info As Runtime.Serialization.SerializationInfo, ByVal context As Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class
End Namespace
