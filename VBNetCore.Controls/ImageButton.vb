Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace VBNetCore.Controls
    Public Class ImageButton
        Inherits Button
        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(ImageButton), New FrameworkPropertyMetadata(GetType(ImageButton)))
        End Sub

        Public Sub New()
            SetCurrentValue(ImageLocationProperty, Controls.ImageLocation.Left)
        End Sub

        Public Property ImageWidth As Integer
            Get
                Return GetValue(ImageWidthProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(ImageWidthProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ImageWidthProperty As DependencyProperty = DependencyProperty.Register("ImageWidth", GetType(Integer), GetType(ImageButton), New PropertyMetadata(30))

        Public Property ImageHeight As Integer
            Get
                Return GetValue(ImageHeightProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(ImageHeightProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ImageHeightProperty As DependencyProperty = DependencyProperty.Register("ImageHeight", GetType(Integer), GetType(ImageButton), New PropertyMetadata(30))

        Public Property ImageLocation As ImageLocation
            Get
                Return CType(GetValue(ImageLocationProperty), ImageLocation)
            End Get
            Set(ByVal value As ImageLocation)
                SetValue(ImageLocationProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ImageLocationProperty As DependencyProperty = DependencyProperty.Register("ImageLocation", GetType(ImageLocation), GetType(ImageButton), New PropertyMetadata(ImageLocation.Left, AddressOf PropertyChangedCallback))

        Private Shared Sub PropertyChangedCallback(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim btn = CType(d, ImageButton)
            Dim newLocation As ImageLocation

            If e.NewValue IsNot Nothing Then
                newLocation = CType(e.NewValue, ImageLocation)
            Else
                newLocation = ImageLocation.Left
            End If

            Select Case newLocation
                Case Controls.ImageLocation.Left
                    btn.SetCurrentValue(ImageButton.RowIndexProperty, 1)
                    btn.SetCurrentValue(ImageButton.ColumnIndexProperty, 0)
                Case Controls.ImageLocation.Top
                    btn.SetCurrentValue(ImageButton.RowIndexProperty, 0)
                    btn.SetCurrentValue(ImageButton.ColumnIndexProperty, 1)
                Case Controls.ImageLocation.Right
                    btn.SetCurrentValue(ImageButton.RowIndexProperty, 1)
                    btn.SetCurrentValue(ImageButton.ColumnIndexProperty, 2)
                Case Controls.ImageLocation.Bottom
                    btn.SetCurrentValue(ImageButton.RowIndexProperty, 2)
                    btn.SetCurrentValue(ImageButton.ColumnIndexProperty, 1)
                Case Controls.ImageLocation.Center
                    btn.SetCurrentValue(ImageButton.RowIndexProperty, 1)
                    btn.SetCurrentValue(ImageButton.ColumnIndexProperty, 1)
                Case Else
                    Throw New ArgumentOutOfRangeException()
            End Select
        End Sub

        Public Property ImageSource As ImageSource
            Get
                Return CType(GetValue(ImageSourceProperty), ImageSource)
            End Get
            Set(ByVal value As ImageSource)
                SetValue(ImageSourceProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ImageSourceProperty As DependencyProperty = DependencyProperty.Register("ImageSource", GetType(ImageSource), GetType(ImageButton), New PropertyMetadata(Nothing))

        Public Property RowIndex As Integer
            Get
                Return GetValue(RowIndexProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(RowIndexProperty, value)
            End Set
        End Property

        Public Shared ReadOnly RowIndexProperty As DependencyProperty = DependencyProperty.Register("RowIndex", GetType(Integer), GetType(ImageButton), New PropertyMetadata(1))

        Public Property ColumnIndex As Integer
            Get
                Return GetValue(ColumnIndexProperty)
            End Get
            Set(ByVal value As Integer)
                SetValue(ColumnIndexProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ColumnIndexProperty As DependencyProperty = DependencyProperty.Register("ColumnIndex", GetType(Integer), GetType(ImageButton), New PropertyMetadata(0))
    End Class
End Namespace
