Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Namespace VBNetCore.Controls
    Public Class NumericTextBox
        Inherits InputTextBox
        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(NumericTextBox), New FrameworkPropertyMetadata(GetType(NumericTextBox)))
        End Sub

        Public Sub New()
            SetCurrentValue(ValueProperty, 0)
        End Sub

        Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Integer), GetType(NumericTextBox), New FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AddressOf OnValueChanged))

        Private Shared Sub OnValueChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            If e.NewValue IsNot Nothing Then
                Dim ntb = CType(sender, NumericTextBox)
                ntb.Text = ntb.Value.ToString()
            End If
        End Sub

        Public Property Value As Integer
            Get
                Return GetValue(ValueProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(ValueProperty, value)
            End Set
        End Property
        Protected Overrides Sub OnPreviewTextInput(ByVal e As TextCompositionEventArgs)
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+")
        End Sub

        Protected Overrides Sub OnTextChanged(ByVal e As TextChangedEventArgs)
            SetValue(ValueProperty, If(Text.Length = 0, 0, Integer.Parse(Text)))
        End Sub
    End Class
End Namespace
