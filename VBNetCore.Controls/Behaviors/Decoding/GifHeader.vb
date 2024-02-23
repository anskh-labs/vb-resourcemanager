Imports System.IO

Namespace VBNetCore.Controls.Behaviors.Decoding
    Friend Class GifHeader
        Inherits GifBlock
        Private _Signature As String, _Version As String, _LogicalScreenDescriptor As VBNetCore.Controls.Behaviors.Decoding.GifLogicalScreenDescriptor

        Public Property Signature As String
            Get
                Return _Signature
            End Get
            Private Set(ByVal value As String)
                _Signature = value
            End Set
        End Property

        Public Property Version As String
            Get
                Return _Version
            End Get
            Private Set(ByVal value As String)
                _Version = value
            End Set
        End Property

        Public Property LogicalScreenDescriptor As GifLogicalScreenDescriptor
            Get
                Return _LogicalScreenDescriptor
            End Get
            Private Set(ByVal value As GifLogicalScreenDescriptor)
                _LogicalScreenDescriptor = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.Other
            End Get
        End Property

        Friend Shared Function ReadHeader(ByVal stream As Stream) As GifHeader
            Dim header = New GifHeader()
            header.Read(stream)
            Return header
        End Function

        Private Sub Read(ByVal stream As Stream)
            Signature = ReadString(stream, 3)
            If Not Equals(Signature, "GIF") Then Throw InvalidSignatureException(Signature)
            Version = ReadString(stream, 3)
            If Not Equals(Version, "87a") AndAlso Not Equals(Version, "89a") Then Throw UnsupportedVersionException(Version)
            LogicalScreenDescriptor = GifLogicalScreenDescriptor.ReadLogicalScreenDescriptor(stream)
        End Sub
    End Class
End Namespace
