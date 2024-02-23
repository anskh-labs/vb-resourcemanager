Imports System.Windows
Imports System.Windows.Controls

Namespace VBNetCore.Controls
    Public Class InputTextBox
        Inherits TextBox
        Shared Sub New()
            Call DefaultStyleKeyProperty.OverrideMetadata(GetType(InputTextBox), New FrameworkPropertyMetadata(GetType(InputTextBox)))
        End Sub
    End Class
End Namespace
