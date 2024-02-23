Imports System.Diagnostics
Imports System.Threading

Namespace ResourceManager.Helpers
    Friend Module MutexLockHelper
        Private mutex As System.Threading.Mutex = Nothing

        Public Function CreateMutex() As Boolean
            If MutexLockHelper.mutex IsNot Nothing Then
                Return True
            End If

            Dim createdNew As Boolean = Nothing
            MutexLockHelper.mutex = New Mutex(True, GetCurrentProcessName(), createdNew)

            If createdNew Then
                Return True
            Else
                ResourceManager.Helpers.MutexLockHelper.mutex = Nothing
                Return False
            End If
        End Function

        Public Sub ReleaseMutex()
            If MutexLockHelper.mutex Is Nothing Then
                Return
            End If

            Call MutexLockHelper.mutex.ReleaseMutex()
        End Sub

        Private Function GetCurrentProcessName() As String
            Using currentProcess = Process.GetCurrentProcess()
                Return currentProcess.ProcessName
            End Using
        End Function
    End Module
End Namespace
