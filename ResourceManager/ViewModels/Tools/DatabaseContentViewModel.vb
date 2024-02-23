Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Options
Imports Microsoft.Win32
Imports VBNetCore.DatabaseToolkit.SQLite
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports VBNetCore.Validators
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Settings
Imports System
Imports System.Windows.Input
Imports SQLitePCL

Namespace ResourceManager.ViewModels
    Public Class DatabaseContentViewModel
        Inherits PageContentViewModel
        Private ReadOnly Property appSettings As AppSettings
            Get
                Return ServiceProviderServiceExtensions.GetRequiredService(Of IOptions(Of AppSettings))(Application.ServiceProvider).Value
            End Get
        End Property
        Public Sub New()
            MenuTitle = "Database"
            MenuIcon = AssetManager.Instance.GetImage("Database.png")
            Title = "Database Tools"
            PageColor = ToolsPageColor

            AddValidationRule(Function() FilePath, Function(x) TextValidator.Instance.Required(x), TextValidator.Instance.ErrorMessage("Required", NameOf(DatabaseContentViewModel.FilePath)))

            ExecuteCommand = New RelayCommand(AddressOf OnExecute, AddressOf CanExecute)
            OpenCommand = New RelayCommand(AddressOf OnOpen)

            IsBackup = True
        End Sub

        Private Sub OnOpen()
            If IsBackup Then
                Dim dlg = New SaveFileDialog()
                dlg.DefaultExt = ".bak"
                dlg.Filter = "Backup File|*.bak"
                dlg.Title = "Save file to backup"
                Dim b = dlg.ShowDialog()
                If b IsNot Nothing And b.Equals(True) Then
                    FilePath = dlg.FileName
                End If
            Else
                Dim dlg = New OpenFileDialog()
                dlg.DefaultExt = ".bak"
                dlg.Filter = "Backup File|*.bak"
                dlg.Title = "Open file to restore"
                Dim b = dlg.ShowDialog()
                If b IsNot Nothing And b.Equals(True) Then
                    FilePath = dlg.FileName
                End If
            End If
        End Sub

        Private Function CanExecute() As Boolean
            Return Not HasErrors
        End Function

        Private Sub OnExecute()
            ValidateAllRules()
            If Not HasErrors Then
                Try
                    Dim tools As ISQLiteToolkit = Application.ServiceProvider.GetRequiredService(Of ISQLiteToolkit)()
                    Dim dbFilename = Constants.DbFileName
                    If IsBackup Then
                        Dim x = tools.BackupDatabase(dbFilename, FilePath)
                        If x.Item1 = 0 Then
                            ShowPopupMessage("Database backed up succesfully.", appSettings.AppName, PopupButton.OK, PopupImage.Information)
                        Else
                            ShowPopupMessage("Backup database failed. Message:" & x.Item2, appSettings.AppName, PopupButton.OK, PopupImage.Warning)
                        End If
                    Else
                        Dim x = tools.RestoreDatabase(dbFilename, FilePath)
                        If x.Item1 = 0 Then
                            ShowPopupMessage("Database restored succesfully.", appSettings.AppName, PopupButton.OK, PopupImage.Information)
                        Else
                            ShowPopupMessage("Restore database failed. Message:" & x.Item2, appSettings.AppName, PopupButton.OK, PopupImage.Warning)
                        End If
                    End If
                Catch e As Exception
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error)
                End Try
            End If
        End Sub

        Public Property IsBackup As Boolean
            Get
                Return GetProperty(Of Boolean)()
            End Get
            Set(ByVal value As Boolean)
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

        Public ReadOnly Property ExecuteCommand As ICommand
        Public ReadOnly Property OpenCommand As ICommand
    End Class
End Namespace
