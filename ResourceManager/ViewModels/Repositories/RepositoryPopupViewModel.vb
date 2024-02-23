Imports Microsoft.Win32
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class RepositoryPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private oldRepository As Repository
        Private isEdit As Boolean
        Private fileIsChanged As Boolean = False
        Private bgWorker As BackgroundWorker
        Private generatedFilename, destFilename, destPath As String

        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() Title, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Title"))
            AddValidationRule(Function() FilePath, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "File path"))

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
            BrowseFileCommand = New RelayCommand(AddressOf OnBrowseFile)

            IsOnProcess = False
            bgWorker = New BackgroundWorker()
            AddHandler bgWorker.ProgressChanged, AddressOf bgWorker_ProgressChanged
            bgWorker.WorkerReportsProgress = True
            AddHandler bgWorker.DoWork, New DoWorkEventHandler(AddressOf bgWorker_DoWork)
            AddHandler bgWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bgWorker_RunWorkerCompleted)
        End Sub

        Private Sub OnBrowseFile()
            Dim dlg = New OpenFileDialog()
            dlg.Title = "Select repository file"
            dlg.Filter = Application.Settings.RepositorySettings.SupportExtensions
            dlg.CheckFileExists = True
            Dim res = dlg.ShowDialog()
            If res IsNot Nothing AndAlso res.Equals(True) Then
                FilePath = dlg.FileName
                fileIsChanged = True
                If Not isEdit Then
                    Title = If(String.IsNullOrWhiteSpace(Title), Path.GetFileNameWithoutExtension(dlg.FileName), Title)
                End If
            End If
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors AndAlso IsOnProcess = False
        End Function
        Public Sub SetRepo(ByVal repo As Repository)
            isEdit = True
            oldRepository = repo
            ID = repo.ID
            Title = repo.Title
            FilePath = repo.FilePath
            FileType = repo.FileType
            FileSize = repo.FileSize
        End Sub
        Private Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                generatedFilename = String.Concat(Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries))
                If generatedFilename.Length > 250 Then generatedFilename = generatedFilename.Substring(0, 250)
                destFilename = generatedFilename & Path.GetExtension(FilePath)
                destPath = Path.Combine(Application.Settings.RepositorySettings.FolderPath, destFilename)

                Try
                    bgWorker.RunWorkerAsync()
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End If

            Return Task.CompletedTask
        End Function

        Private Sub bgWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
            Result = PopupResult.OK
            OnClose()
        End Sub

        Private Async Sub bgWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Application.Current.Dispatcher.Invoke(Sub() IsOnProcess = True)
            If isEdit Then
                If fileIsChanged Then
                    If Application.Settings.RepositorySettings.DeleteSourceFile Then
                        File.Move(FilePath, destPath, True)
                    Else
                        File.Copy(FilePath, destPath, True)
                    End If
                Else
                    If Not Equals(destFilename, oldRepository.Filename) Then
                        File.Move(oldRepository.FilePath, destPath)
                    End If
                End If
                Dim repo = New Repository() With {
    .Title = Title,
    .Filename = destFilename,
    .FileType = FileType,
    .FileSize = FileSize
}
                Await _dataServiceManager.RepositoryDataService.Update(ID, repo)
            Else
                If Application.Settings.RepositorySettings.DeleteSourceFile Then
                    File.Move(FilePath, destPath, True)
                Else
                    File.Copy(FilePath, destPath, True)
                End If

                Dim repo = New Repository() With {
    .Title = Title,
    .Filename = destFilename,
    .FileType = FileType,
    .FileSize = FileSize
}
                Await _dataServiceManager.RepositoryDataService.Create(repo)
            End If
            Application.Current.Dispatcher.Invoke(Sub() IsOnProcess = False)
        End Sub

        Private Sub bgWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)

        End Sub

        Public Property Title As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property FilePath As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                If SetProperty(value) Then
                    If File.Exists(value) Then
                        Dim info = New FileInfo(value)
                        FileType = info.Extension.Substring(1)
                        Dim size As Integer = info.Length >> 10
                        FileSize = size + 1
                    End If
                End If
            End Set
        End Property
        Public Property FileType As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property FileSize As Integer
            Get
                Return GetProperty(Of Integer)()
            End Get
            Set(ByVal value As Integer)
                SetProperty(value)
            End Set
        End Property

        Public Property Tags As IList(Of Tag)
            Get
                Return GetProperty(Of IList(Of Tag))()
            End Get
            Set(ByVal value As IList(Of Tag))
                SetProperty(value)
            End Set
        End Property
        Public Property IsOnProcess As Boolean
            Get
                Return GetProperty(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property SaveCommand As ICommand
        Public ReadOnly Property BrowseFileCommand As ICommand
    End Class
End Namespace
