Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Commands
Imports VBNetCore.Mvvm.Controls
Imports ResourceManager.Configuration
Imports ResourceManager.Helpers
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports System.Linq
Imports System.Windows.Input
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager.ViewModels
    Public Class PasswordContentViewModel
        Inherits PageContentWithListViewModel(Of Password)
        Private ReadOnly _dataServiceManager As IDataServiceManager
        Public Sub New(ByVal dataServiceManager As IDataServiceManager)
            MyBase.New(dataServiceManager.PasswordDataService)
            _dataServiceManager = dataServiceManager
            MenuTitle = "Password"
            MenuIcon = AssetManager.Instance.GetImage("Passwords.png")
            Title = "Password Manager"
            PageColor = PasswordPageColor

            _add_permission_name = ACTION_ADD_PASSWORD
            _edit_permission_name = ACTION_EDIT_PASSWORD
            _delete_permission_name = ACTION_DEL_PASSWORD

            TagsCommand = New RelayCommand(Of Password)(AddressOf OnTags)

            MyBase.OnRefresh()
        End Sub

        Private Async Sub OnTags(ByVal pass As Password)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of TagsPopupViewModel)()
            vm.Caption = String.Format("Manage tag for {0}", pass.GetCaption())
            Dim tags = Await _dataServiceManager.TagDataService.GetTagObjectForPasswordID(pass.ID)
            vm.Tags = tags.ToList()
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                Await _dataServiceManager.PasswordDataService.UpdateTags(pass, vm.Tags)
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnEdit(ByVal password As Password)
            Dim vm = Application.ServiceProvider.GetRequiredService(Of PasswordPopupViewModel)()
            vm.Caption = "Edit Password"
            vm.SetPassword(password)
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub

        Protected Overrides Sub OnAdd()
            Dim vm = Application.ServiceProvider.GetRequiredService(Of PasswordPopupViewModel)()
            vm.Caption = "Add Password"
            Dim popup = ShowPopup(vm)
            If popup.Result = PopupResult.OK Then
                MyBase.OnRefresh()
            End If
        End Sub
        Protected Overrides Sub OnFilter()
            MyBase.Filter(FilterColumns.Password, "FIlter Password")
        End Sub

        Public ReadOnly Property TagsCommand As ICommand
    End Class
End Namespace
