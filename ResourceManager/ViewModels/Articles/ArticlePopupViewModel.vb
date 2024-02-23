Imports Microsoft.Win32
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Mvvm.ViewModels
Imports VBNetCore.Validators
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace ResourceManager.ViewModels
    Public Class ArticlePopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private fileIsChanged As Boolean = False
        Private isEdit As Boolean
        Private oldArticle As Article

        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() Title, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Title"))
            AddValidationRule(Function() Author, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "Author"))
            AddValidationRule(Function() FilePath, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", "File Path"))

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
            BrowseFileCommand = New RelayCommand(AddressOf OnBrowseFile)
        End Sub


        Private Sub OnBrowseFile()
            Dim dlg = New OpenFileDialog()
            dlg.Title = "Select article file"
            dlg.Filter = Application.Settings.ArticleSettings.SupportExtensions
            dlg.CheckFileExists = True
            Dim res = dlg.ShowDialog()
            If res IsNot Nothing AndAlso res.Equals(True) Then
                FilePath = dlg.FileName
                fileIsChanged = True
                If Not isEdit Then
                    Title = If(String.IsNullOrWhiteSpace(Title), Path.GetFileNameWithoutExtension(dlg.FileName), Title)
                    Author = If(String.IsNullOrWhiteSpace(Author), "-", String.Empty)
                    Publisher = If(String.IsNullOrWhiteSpace(Publisher), "-", String.Empty)
                End If
            End If
        End Sub

        Private Function CanSave() As Boolean
            Return Not HasErrors
        End Function
        Public Sub SetArticle(ByVal article As Article)
            isEdit = True
            oldArticle = article
            ID = article.ID
            Title = article.Title
            Author = article.Author
            FilePath = article.FilePath
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Dim generatedFilename As String = String.Concat(Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries))
                If generatedFilename.Length > 250 Then generatedFilename = generatedFilename.Substring(0, 250)
                Try
                    If isEdit Then
                        Dim destFilename = generatedFilename & Path.GetExtension(FilePath)
                        Dim destPath = Path.Combine(Application.Settings.ArticleSettings.FolderPath, destFilename)
                        If fileIsChanged Then
                            If Application.Settings.ArticleSettings.DeleteSourceFile Then
                                File.Move(FilePath, destPath, True)
                            Else
                                File.Copy(FilePath, destPath, True)
                            End If
                        Else
                            If Not Equals(destFilename, oldArticle.Filename) Then
                                File.Move(oldArticle.FilePath, destPath)
                            End If
                        End If
                        Dim article = New Article() With {
    .Title = Title,
    .Author = Author,
    .Filename = destFilename
}
                        Await _dataServiceManager.ArticleDataService.Update(ID, article)

                        Result = PopupResult.OK
                    Else

                        Dim destFilename = generatedFilename & Path.GetExtension(FilePath)
                        Dim destPath = Path.Combine(Application.Settings.ArticleSettings.FolderPath, destFilename)
                        If Application.Settings.ArticleSettings.DeleteSourceFile Then
                            File.Move(FilePath, destPath, True)
                        Else
                            File.Copy(FilePath, destPath, True)
                        End If

                        Dim article = New Article() With {
    .Title = Title,
    .Author = Author,
    .Filename = destFilename
}
                        Await _dataServiceManager.ArticleDataService.Create(article)

                        Result = PopupResult.OK
                    End If
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
                OnClose()
            End If
        End Function

        Public Property Title As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Author As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Publisher As String
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
                SetProperty(value)
            End Set
        End Property
        Public Property Cover As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public Property Abstraction As String
            Get
                Return GetProperty(Of String)()
            End Get
            Set(ByVal value As String)
                SetProperty(value)
            End Set
        End Property
        Public ReadOnly Property SaveCommand As ICommand
        Public ReadOnly Property BrowseFileCommand As ICommand
        Public ReadOnly Property BrowseCoverCommand As ICommand
    End Class
End Namespace
