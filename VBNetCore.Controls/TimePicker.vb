Imports System.Windows
Imports System.Windows.Controls

Namespace VBNetCore.Controls
    Public Class TimePicker
        Inherits UserControl
        Private _currentTextbox As TextBox

        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(TimePicker), New FrameworkPropertyMetadata(GetType(TimePicker)))
        End Sub

        Public Sub New()
            SetCurrentValue(TimeProperty, Date.Now)
        End Sub

        Public Overrides Sub OnApplyTemplate()
            Dim btn_up = FindChild(Of Button)(Me, "Up_Button")
            AddHandler btn_up.Click, AddressOf Up_Click
            Dim btn_down = FindChild(Of Button)(Me, "Down_Button")
            AddHandler btn_down.Click, AddressOf Down_Click
            Dim txt_hour = FindChild(Of TextBox)(Me, "Txt_Hour")
            AddHandler txt_hour.LostFocus, AddressOf Txt_LostFocus
            Dim txt_minute = FindChild(Of TextBox)(Me, "Txt_Minute")
            AddHandler txt_minute.LostFocus, AddressOf Txt_LostFocus
            MyBase.OnApplyTemplate()
        End Sub

        Private Sub Txt_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _currentTextbox = TryCast(e.Source, TextBox)
        End Sub

        Public Property Time As Date
            Get
                Return GetValue(TimeProperty)
            End Get
            Set(ByVal value As Date)
                SetValue(TimeProperty, value)
            End Set
        End Property

        Public Shared ReadOnly TimeProperty As DependencyProperty = DependencyProperty.Register("Time", GetType(Date), GetType(TimePicker), New FrameworkPropertyMetadata(Date.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AddressOf OnTimeChanged))

        Private Shared Sub OnTimeChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            If e.NewValue IsNot Nothing Then
                'TimePicker tp = (TimePicker)sender;
            End If
        End Sub

        Private Sub Up_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If _currentTextbox IsNot Nothing Then
                If _currentTextbox.Name.Equals("Txt_Hour") Then
                    Time = Time.AddHours(1)
                ElseIf _currentTextbox.Name.Equals("Txt_Minute") Then
                    Time = Time.AddMinutes(1)
                End If
            End If
        End Sub

        Private Sub Down_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If _currentTextbox IsNot Nothing Then
                If _currentTextbox.Name.Equals("Txt_Hour") Then
                    Time = Time.AddHours(-1)
                ElseIf _currentTextbox.Name.Equals("Txt_Minute") Then
                    Time = Time.AddMinutes(-1)
                End If
            End If
        End Sub
    End Class
End Namespace
