Imports ResourceManager.Configuration
Imports ResourceManager.Helpers

Namespace ResourceManager.ViewModels
    Public Class EbookOptionsContentViewModel
        Inherits PageContentViewModel
        Public Sub New()
            MenuTitle = "Options"
            MenuIcon = AssetManager.Instance.GetImage("Options.png")
            Title = "Ebook Options"
            PageColor = EbookPageColor

            FolderPath = Application.Settings.EbookSettings.FolderPath
            SupportExtensions = Application.Settings.EbookSettings.SupportExtensions
            CoverPath = Application.Settings.EbookSettings.CoverPath
            CoverFileExtensions = Application.Settings.EbookSettings.CoverFileExtensions
            DeleteSourceFile = Application.Settings.EbookSettings.DeleteSourceFile.ToString()
        End Sub
        Public ReadOnly Property FolderPath As String
        Public ReadOnly Property SupportExtensions As String
        Public ReadOnly Property CoverPath As String
        Public ReadOnly Property CoverFileExtensions As String
        Public ReadOnly Property DeleteSourceFile As String
    End Class
End Namespace
