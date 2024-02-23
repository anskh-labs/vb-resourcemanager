Imports Microsoft.Extensions.DependencyInjection
Imports VBNetCore.Mvvm.Extensions
Imports VBNetCore.Mvvm.Helpers
Imports System
Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Controls

Namespace VBNetCore.Mvvm.Controls
    ''' <summary>
    ''' A content control presenting a view for a given view model via binding.
    ''' </summary>
    Public Class ViewModelPresenter
        Inherits ContentControl
        ''' <summary>
        ''' The dependency property for the bindable view model.
        ''' </summary>
        Public Shared ReadOnly ViewModelProperty As DependencyProperty = DependencyProperty.Register("ViewModel", GetType(Object), GetType(ViewModelPresenter), New PropertyMetadata(Nothing, AddressOf OnViewModelChanged))

        ''' <summary>
        ''' The view model for which this control should display the corresponding view.
        ''' </summary>
        Public Property ViewModel As Object
            Get
                Return GetValue(ViewModelProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(ViewModelProperty, value)
            End Set
        End Property

        Private Shared Sub OnViewModelChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            If DesignerProperties.GetIsInDesignMode(d) Then
                Return
            End If

            Dim self = CType(d, ViewModelPresenter)
            self.Content = Nothing

            If e.NewValue IsNot Nothing Then
                Dim serviceProvider = GetServiceProvider(d)
                Dim view = serviceProvider.GetRequiredService(Of ViewLocator).GetViewForViewModel(e.NewValue)
                self.Content = view
            End If
        End Sub

        Private Shared Function GetServiceProvider(ByVal dependencyObject As DependencyObject) As IServiceProvider
            Dim runner = dependencyObject
            While runner IsNot Nothing
                Dim serviceProvider = ServiceProviderPropertyExtension.GetServiceProvider(runner)
                If serviceProvider IsNot Nothing Then
                    Return serviceProvider
                End If

                runner = runner.GetParentObject()
            End While

            Throw New Exception("Could not locate IServiceProvider in visual tree.")
        End Function
    End Class
End Namespace
