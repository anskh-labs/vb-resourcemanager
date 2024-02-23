Imports System.Windows
Imports System.Windows.Controls

Namespace VBNetCore.Mvvm.Helpers
    Public Module PasswordBoxHelper
        Public ReadOnly BoundPassword As DependencyProperty = DependencyProperty.RegisterAttached("BoundPassword", GetType(String), GetType(PasswordBoxHelper), New PropertyMetadata(String.Empty, AddressOf OnBoundPasswordChanged))

        Public ReadOnly BindPassword As DependencyProperty = DependencyProperty.RegisterAttached("BindPassword", GetType(Boolean), GetType(PasswordBoxHelper), New PropertyMetadata(False, AddressOf OnBindPasswordChanged))

        Private ReadOnly UpdatingPassword As DependencyProperty = DependencyProperty.RegisterAttached("UpdatingPassword", GetType(Boolean), GetType(PasswordBoxHelper), New PropertyMetadata(False))

        Private Sub OnBoundPasswordChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim box As PasswordBox = TryCast(d, PasswordBox)

            ' only handle this event when the property is attached to a PasswordBox
            ' and when the BindPassword attached property has been set to true
            If box Is Nothing OrElse Not PasswordBoxHelper.GetBindPassword(box) Then Return

            ' avoid recursive updating by ignoring the box's changed event
            RemoveHandler box.PasswordChanged, AddressOf PasswordBoxHelper.HandlePasswordChanged

            Dim newPassword = CStr(e.NewValue)

            If Not PasswordBoxHelper.GetUpdatingPassword(box) Then box.Password = newPassword

            AddHandler box.PasswordChanged, AddressOf PasswordBoxHelper.HandlePasswordChanged
        End Sub

        Private Sub OnBindPasswordChanged(ByVal dp As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            ' when the BindPassword attached property is set on a PasswordBox,
            ' start listening to its PasswordChanged event

            Dim box As PasswordBox = TryCast(dp, PasswordBox)

            If box Is Nothing Then Return

            Dim wasBound As Boolean = e.OldValue
            Dim needToBind As Boolean = e.NewValue

            If wasBound Then RemoveHandler box.PasswordChanged, AddressOf PasswordBoxHelper.HandlePasswordChanged

            If needToBind Then AddHandler box.PasswordChanged, AddressOf PasswordBoxHelper.HandlePasswordChanged
        End Sub

        Private Sub HandlePasswordChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim box As PasswordBox = TryCast(sender, PasswordBox)

            If box Is Nothing Then Return

            ' set a flag to indicate that we're updating the password
            PasswordBoxHelper.SetUpdatingPassword(box, True)
            ' push the new password into the BoundPassword property
            PasswordBoxHelper.SetBoundPassword(box, box.Password)
            PasswordBoxHelper.SetUpdatingPassword(box, False)
        End Sub

        Public Sub SetBindPassword(ByVal dp As DependencyObject, ByVal value As Boolean)
            dp.SetValue(BindPassword, value)
        End Sub

        Public Function GetBindPassword(ByVal dp As DependencyObject) As Boolean
            Return dp.GetValue(BindPassword)
        End Function

        Public Function GetBoundPassword(ByVal dp As DependencyObject) As String
            Return CStr(dp.GetValue(BoundPassword))
        End Function

        Public Sub SetBoundPassword(ByVal dp As DependencyObject, ByVal value As String)
            dp.SetValue(BoundPassword, value)
        End Sub

        Private Function GetUpdatingPassword(ByVal dp As DependencyObject) As Boolean
            Return dp.GetValue(UpdatingPassword)
        End Function

        Private Sub SetUpdatingPassword(ByVal dp As DependencyObject, ByVal value As Boolean)
            dp.SetValue(UpdatingPassword, value)
        End Sub
    End Module
End Namespace
