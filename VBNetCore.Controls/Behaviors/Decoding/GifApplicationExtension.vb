Imports System
Imports System.IO
Imports System.Text

Namespace VBNetCore.Controls.Behaviors.Decoding
    ' label 0xFF
    Friend Class GifApplicationExtension
        Inherits GifExtension

        Private _BlockSize As Integer, _ApplicationIdentifier As String, _AuthenticationCode As Byte(), _Data As Byte()
        Friend Const ExtensionLabel As Integer = &HFF

        Public Property BlockSize As Integer
            Get
                Return _BlockSize
            End Get
            Private Set(ByVal value As Integer)
                _BlockSize = value
            End Set
        End Property

        Public Property ApplicationIdentifier As String
            Get
                Return _ApplicationIdentifier
            End Get
            Private Set(ByVal value As String)
                _ApplicationIdentifier = value
            End Set
        End Property

        Public Property AuthenticationCode As Byte()
            Get
                Return _AuthenticationCode
            End Get
            Private Set(ByVal value As Byte())
                _AuthenticationCode = value
            End Set
        End Property

        Public Property Data As Byte()
            Get
                Return _Data
            End Get
            Private Set(ByVal value As Byte())
                _Data = value
            End Set
        End Property

        Private Sub New()
        End Sub

        Friend Overrides ReadOnly Property Kind As GifBlockKind
            Get
                Return GifBlockKind.SpecialPurpose
            End Get
        End Property

        Friend Shared Function ReadApplication(ByVal stream As Stream) As GifApplicationExtension
            Dim ext = New GifApplicationExtension()
            ext.Read(stream)
            Return ext
        End Function

        Private Sub Read(ByVal stream As Stream)
            ' Note: at this point, the label (0xFF) has already been read

            Dim bytes = New Byte(11) {}
            stream.ReadAll(bytes, 0, bytes.Length)
            BlockSize = bytes(0) ' should always be 11
            If BlockSize <> 11 Then Throw InvalidBlockSizeException("Application Extension", 11, BlockSize)

            ApplicationIdentifier = Encoding.ASCII.GetString(bytes, 1, 8)
            Dim authCode = New Byte(2) {}
            Array.Copy(bytes, 9, authCode, 0, 3)
            AuthenticationCode = authCode
            Data = ReadDataBlocks(stream, False)
        End Sub
    End Class
End Namespace
