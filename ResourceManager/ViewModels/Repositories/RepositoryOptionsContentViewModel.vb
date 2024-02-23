Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Options
Imports VBNetCore.Mvvm.Abstractions
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Settings
Imports System

Namespace ResourceManager.ViewModels
    Public Class RepositoryOptionsContentViewModel
        Inherits PageContentViewModel
        Private ReadOnly Property appSettings As AppSettings
            Get
                Return ServiceProviderServiceExtensions.GetRequiredService(Of IOptionsMonitor(Of AppSettings))(Application.ServiceProvider).CurrentValue
            End Get
        End Property
        Public Sub New()
            MenuTitle = "Options"
            MenuIcon = AssetManager.Instance.GetImage("Options.png")
            Title = "Repository Options"
            PageColor = RepositoryPageColor

            FolderPath = appSettings.RepositorySettings.FolderPath
            SupportExtensions = appSettings.RepositorySettings.SupportExtensions
            DeleteSourceFile = appSettings.RepositorySettings.DeleteSourceFile.ToString()
        End Sub
        Public ReadOnly Property FolderPath As String
        Public ReadOnly Property SupportExtensions As String
        Public ReadOnly Property DeleteSourceFile As String
    End Class
End Namespace
