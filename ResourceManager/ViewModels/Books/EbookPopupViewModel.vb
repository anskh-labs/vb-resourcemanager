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
    Public Class EbookPopupViewModel
        Inherits PopupViewModelBase
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Private ID As Integer
        Private coverIsChanged As Boolean = False
        Private fileIsChanged As Boolean = False
        Private isEdit As Boolean
        Private oldBook As Book

        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            _dataServiceManager = dataServiceManager
            isEdit = False

            AddValidationRule(Function() Title, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(EbookPopupViewModel.Title)))
            AddValidationRule(Function() Author, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(EbookPopupViewModel.Author)))
            AddValidationRule(Function() FilePath, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(EbookPopupViewModel.FilePath)))
            AddValidationRule(Function() Publisher, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(EbookPopupViewModel.Publisher)))

            SaveCommand = New AsyncRelayCommand(AddressOf OnSaveAsync, AddressOf CanSave)
            BrowseFileCommand = New RelayCommand(AddressOf OnBrowseFile)
            BrowseCoverCommand = New RelayCommand(AddressOf OnBrowseCover)
        End Sub

        Private Sub OnBrowseCover()
            Dim dlg = New OpenFileDialog With {
                .Title = "Select cover file",
                .Filter = Application.Settings.EbookSettings.CoverFileExtensions,
                .CheckFileExists = True
            }
            Dim res = dlg.ShowDialog()
            If res IsNot Nothing AndAlso res.Equals(True) Then
                CoverPath = dlg.FileName
                coverIsChanged = True
            End If
        End Sub

        Private Sub OnBrowseFile()
            Dim dlg = New OpenFileDialog()
            dlg.Title = "Select ebook file"
            dlg.Filter = Application.Settings.EbookSettings.SupportExtensions
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
        Public Sub SetBook(ByVal book As Book)
            isEdit = True
            oldBook = book
            ID = book.ID
            Title = book.Title
            Author = book.Author
            Publisher = book.Publisher
            FilePath = book.FilePath
            CoverPath = book.CoverPath
            Abstraction = book.Abstraction
        End Sub
        Private Async Function OnSaveAsync() As Task
            ValidateAllRules()

            If Not HasErrors Then
                Dim generatedFilename As String = FileValidator.Instance.Filter(Title)
                If generatedFilename.Length > 250 Then generatedFilename = generatedFilename.Substring(0, 250)
                Try
                    If isEdit Then
                        Dim destFilename = generatedFilename & Path.GetExtension(FilePath)
                        Dim destPath = Path.Combine(Application.Settings.EbookSettings.FolderPath, destFilename)
                        If fileIsChanged Then
                            If Application.Settings.EbookSettings.DeleteSourceFile Then
                                File.Move(FilePath, destPath, True)
                            Else
                                File.Copy(FilePath, destPath, True)
                            End If
                        Else
                            If Not Equals(destFilename, oldBook.Filename) Then
                                File.Move(oldBook.FilePath, destPath)
                            End If
                        End If
                        Dim coverFilename = generatedFilename & Path.GetExtension(Me.CoverPath)
                        Dim coverPath = Path.Combine(Application.Settings.EbookSettings.CoverPath, coverFilename)
                        If coverIsChanged Then
                            If Not String.IsNullOrEmpty(Me.CoverPath) Then
                                If Application.Settings.EbookSettings.DeleteSourceFile Then
                                    File.Move(Me.CoverPath, coverPath, True)
                                Else
                                    File.Copy(Me.CoverPath, coverPath, True)
                                End If
                            End If
                        Else
                            If Not String.IsNullOrEmpty(Me.CoverPath) Then
                                If Not Equals(coverFilename, oldBook.Cover) Then
                                    File.Move(Me.CoverPath, coverPath)
                                End If
                            Else
                                coverFilename = String.Empty
                            End If
                        End If
                        Dim book = New Book() With {
    .Title = Title,
    .Author = Author,
    .Publisher = Publisher,
    .Filename = destFilename,
    .Cover = coverFilename,
    .Abstraction = Abstraction
}
                        Await _dataServiceManager.BookDataService.Update(ID, book)

                        Result = PopupResult.OK
                    Else
                        Dim destFilename = generatedFilename & Path.GetExtension(FilePath)
                        Dim destPath = Path.Combine(Application.Settings.EbookSettings.FolderPath, destFilename)
                        If Application.Settings.EbookSettings.DeleteSourceFile Then
                            File.Move(FilePath, destPath, True)
                        Else
                            File.Copy(FilePath, destPath, True)
                        End If
                        Dim coverFilename = String.Empty
                        Dim coverPath = String.Empty
                        If Not String.IsNullOrEmpty(Me.CoverPath) Then
                            coverFilename = generatedFilename & Path.GetExtension(Me.CoverPath)
                            coverPath = Path.Combine(Application.Settings.EbookSettings.CoverPath, coverFilename)
                            If Application.Settings.EbookSettings.DeleteSourceFile Then
                                File.Move(Me.CoverPath, coverPath, True)
                            Else
                                File.Copy(Me.CoverPath, coverPath, True)
                            End If
                        End If
                        Dim book = New Book() With {
    .Title = Title,
    .Author = Author,
    .Publisher = Publisher,
    .Filename = destFilename,
    .Cover = coverFilename,
    .Abstraction = Abstraction
}
                        Await _dataServiceManager.BookDataService.Create(book)

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
        Public Property CoverPath As String
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
