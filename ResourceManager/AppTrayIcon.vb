Imports VBNetCore.Mvvm.Abstractions
Imports System
Imports System.Windows
Imports Microsoft.Extensions.DependencyInjection

Namespace ResourceManager
    Public Class AppTrayIcon
        Implements IDisposable

        Private trayIcon As System.Windows.Forms.NotifyIcon = Nothing

        Private ReadOnly Property UiExecution As IUiExecution
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IUiExecution)()
            End Get
        End Property
        Protected ReadOnly Property WindowManager As IWindowManager
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IWindowManager)()
            End Get
        End Property
        Public Property IsVisible As Boolean
            Get
                Return Me.trayIcon IsNot Nothing AndAlso Me.trayIcon.Visible
            End Get
            Set(ByVal value As Boolean)
                If value Then
                    InitializeTrayIcon()
                Else
                    DisposeTrayIcon()
                End If
            End Set
        End Property

        Private Sub InitializeTrayIcon()
            UiExecution.Execute(Sub()
                                    Me.trayIcon = New Forms.NotifyIcon()
                                    Me.trayIcon.Text = "Resource Manager"
                                    Me.trayIcon.Icon = My.Resources.Resources.TrayIcon
                                    Me.trayIcon.ContextMenuStrip = BuildTrayContextMenu()
                                    AddHandler Me.trayIcon.DoubleClick, Sub(sender, args) ShowApplicationMainWindow()
                                    Me.trayIcon.Visible = True
                                End Sub)
        End Sub

        Private Function BuildTrayContextMenu() As Forms.ContextMenuStrip
            Dim contextMenu = New Forms.ContextMenuStrip()

            contextMenu.Items.Add("Show Application", Nothing, Sub(sender, args) ShowApplicationMainWindow())

            contextMenu.Items.Add(New Forms.ToolStripSeparator())

            contextMenu.Items.Add("Exit", Nothing, Sub(sender, args) ExitApplication())

            Return contextMenu
        End Function

        Private Sub ShowApplicationMainWindow()
            UiExecution.Execute(Sub()
                                    Dim mainWindow = Application.Current.MainWindow
                                    If mainWindow Is Nothing Then
                                        Return
                                    End If

                                    If mainWindow.IsVisible Then
                                        If mainWindow.WindowState = WindowState.Minimized Then
                                            mainWindow.WindowState = WindowState.Normal
                                        End If

                                        mainWindow.Activate()
                                    Else
                                        mainWindow.Show()
                                    End If
                                End Sub)
        End Sub

        Private Sub ExitApplication()
            Dim result = WindowManager.ShowMessageBox("Close application", MessageBoxButton.YesNo, MessageBoxImage.Question)
            If result = MessageBoxResult.Yes Then WindowManager.ShutdownApplication()

        End Sub

        Private Sub DisposeTrayIcon()
            UiExecution.Execute(Sub()
                                    If Me.trayIcon Is Nothing Then
                                        Return
                                    End If

                                    Me.trayIcon.Visible = False
                                    Me.trayIcon = Nothing
                                End Sub)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            DisposeTrayIcon()
        End Sub
    End Class
End Namespace
