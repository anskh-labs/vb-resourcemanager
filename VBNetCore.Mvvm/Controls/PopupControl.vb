Imports System.Windows
Imports System.Windows.Controls

Namespace VBNetCore.Mvvm.Controls
    Public Class PopupControl
        Inherits ContentControl
        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(PopupControl), New FrameworkPropertyMetadata(GetType(PopupControl)))
        End Sub
    End Class
End Namespace
