Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Options
Imports VBNetCore.Mvvm.Abstractions
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Settings
Imports System

Namespace ResourceManager.ViewModels
    Public Class ArticleOptionsContentViewModel
        Inherits PageContentViewModel
        Private ReadOnly Property appSettings As AppSettings
            Get
                Return ServiceProviderServiceExtensions.GetRequiredService(Of IOptionsMonitor(Of AppSettings))(Application.ServiceProvider).CurrentValue
            End Get
        End Property
        Public Sub New()
            MenuTitle = "Options"
            MenuIcon = AssetManager.Instance.GetImage("Options.png")
            Title = "Article Options"
            PageColor = ArticlePageColor

            FolderPath = appSettings.ArticleSettings.FolderPath
            SupportExtensions = appSettings.ArticleSettings.SupportExtensions
            DeleteSourceFile = appSettings.ArticleSettings.DeleteSourceFile.ToString()
        End Sub
        Public ReadOnly Property FolderPath As String
        Public ReadOnly Property SupportExtensions As String
        Public ReadOnly Property DeleteSourceFile As String
    End Class
End Namespace
