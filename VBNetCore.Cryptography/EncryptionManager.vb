Imports System
Imports System.Security.Cryptography
Imports System.Text

Namespace VBNetCore.Cryptography
    Public Class EncryptionManager
        Public Const KEY As String = "@bcdefghijklmnopqrstuvwxyz1234567890#+-="
        Private Shared _instance As EncryptionManager = Nothing
        Private Sub New()
        End Sub
        Public Shared ReadOnly Property Instance As EncryptionManager
            Get
                If _instance Is Nothing Then _instance = New EncryptionManager()
                Return _instance
            End Get
        End Property
        Public Function PasswordVerify(ByVal hashedPassword As String, ByVal plainPassword As String) As Boolean
            Dim a = EncryptString(plainPassword, KEY)
            Return hashedPassword.Equals(a)
        End Function

        Public Function EncryptString(ByVal Message As String, ByVal Passphrase As String) As String
            Dim uTF8Encoding As UTF8Encoding = New UTF8Encoding()
            Dim md5 = Security.Cryptography.MD5.Create()
            Dim key = md5.ComputeHash(uTF8Encoding.GetBytes(Passphrase))
            Dim tripelDES = TripleDES.Create()
            tripelDES.Key = key
            tripelDES.Mode = CipherMode.ECB
            tripelDES.Padding = PaddingMode.PKCS7
            Dim bytes = uTF8Encoding.GetBytes(Message)
            Dim inArray As Byte()
            Try
                Dim cryptoTransform As ICryptoTransform = tripelDES.CreateEncryptor()
                inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length)
            Finally
                tripelDES.Clear()
                tripelDES.Clear()
            End Try
            Return Convert.ToBase64String(inArray)
        End Function
        Public Function DecryptString(ByVal Message As String, ByVal Passphrase As String) As String
            Dim uTF8Encoding As UTF8Encoding = New UTF8Encoding()
            Dim md5 = Security.Cryptography.MD5.Create()
            Dim key = md5.ComputeHash(uTF8Encoding.GetBytes(Passphrase))
            Dim tripleDES = Security.Cryptography.TripleDES.Create()
            tripleDES.Key = key
            tripleDES.Mode = CipherMode.ECB
            tripleDES.Padding = PaddingMode.PKCS7
            Dim array = Convert.FromBase64String(Message)
            Dim bytes As Byte()
            Try
                Dim cryptoTransform As ICryptoTransform = tripleDES.CreateDecryptor()
                bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length)
            Finally
                tripleDES.Clear()
                tripleDES.Clear()
            End Try
            Return uTF8Encoding.GetString(bytes)
        End Function
    End Class
End Namespace
