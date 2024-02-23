Imports System.Windows
Imports System.Windows.Input
Imports Microsoft.Xaml.Behaviors

Namespace VBNetCore.Mvvm.Helpers
    ''' <summary>
    ''' TriggerAction which links events to commands.
    ''' </summary>
    Public Class TriggerActionCommand
        Inherits TriggerAction(Of DependencyObject)

#Region "Dependency Properties"

        ''' <summary>
        ''' Command DP
        ''' </summary>
        Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.RegisterAttached("Command", GetType(ICommand), GetType(TriggerActionCommand), New FrameworkPropertyMetadata(Nothing))

        ''' <summary>
        ''' Command that should be called when event is triggered.
        ''' </summary>
        Public Property Command As ICommand
            Get
                Return CType(GetValue(CommandProperty), ICommand)
            End Get
            Set(ByVal value As ICommand)
                SetValue(CommandProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' CommandParameter DP
        ''' </summary>
        Public Shared ReadOnly CommandParameterProperty As DependencyProperty = DependencyProperty.RegisterAttached("CommandParameter", GetType(Object), GetType(TriggerActionCommand), New FrameworkPropertyMetadata(Nothing))

        ''' <summary>
        ''' CommandParameter which should be passed with the Command.
        ''' </summary>
        Public Property CommandParameter As Object
            Get
                Return GetValue(CommandParameterProperty)
            End Get
            Set(ByVal value As Object)
                SetValue(CommandParameterProperty, value)
            End Set
        End Property

#End Region

        ''' <summary>
        ''' Invokes the action.
        ''' </summary>
        ''' <paramname="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        Protected Overrides Sub Invoke(ByVal parameter As Object)
            Dim command = Me.Command
            Dim commandParameter = Me.CommandParameter

            'If an aditional command parameter is specified construct a tuple
            If commandParameter IsNot Nothing Then parameter = (parameter, commandParameter)

            If command.CanExecute(parameter) = True Then command.Execute(parameter)
        End Sub

    End Class
End Namespace
