Imports System.ComponentModel
Imports System.Windows.Media.Animation
Imports System
Imports System.Windows.Controls
Imports System.Linq

Namespace VBNetCore.Controls.Behaviors
    ''' <summary>
    ''' Provides a way to pause, resume or seek a GIF animation.
    ''' </summary>
    Public Class ImageAnimationController
        Implements IDisposable

        ''' <summary>
        ''' Returns a value that indicates whether the animation is paused.
        ''' </summary>
        Private _IsPaused As Boolean
        Private Shared ReadOnly _sourceDescriptor As DependencyPropertyDescriptor

        Shared Sub New()
            _sourceDescriptor = DependencyPropertyDescriptor.FromProperty(Image.SourceProperty, GetType(Image))
        End Sub

        Private ReadOnly _image As Image
        Private ReadOnly _animation As ObjectAnimationUsingKeyFrames
        Private ReadOnly _clock As AnimationClock
        Private ReadOnly _clockController As ClockController

        Friend Sub New(ByVal image As Image, ByVal animation As ObjectAnimationUsingKeyFrames, ByVal autoStart As Boolean)
            _image = image
            _animation = animation
            AddHandler _animation.Completed, AddressOf AnimationCompleted
            _clock = _animation.CreateClock()
            _clockController = _clock.Controller
            Call _sourceDescriptor.AddValueChanged(image, New EventHandler(AddressOf ImageSourceChanged))

            ' ReSharper disable once PossibleNullReferenceException
            _clockController.Pause()

            _image.ApplyAnimationClock(Image.SourceProperty, _clock)

            IsPaused = Not autoStart
            If autoStart Then _clockController.Resume()
        End Sub

        Private Sub AnimationCompleted(ByVal sender As Object, ByVal e As EventArgs)
            _image.RaiseEvent(New Windows.RoutedEventArgs(AnimationCompletedEvent, _image))
        End Sub

        Private Sub ImageSourceChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnCurrentFrameChanged()
        End Sub

        ''' <summary>
        ''' Returns the number of frames in the image.
        ''' </summary>
        Public ReadOnly Property FrameCount As Integer
            Get
                Return _animation.KeyFrames.Count
            End Get
        End Property

        ''' <summary>
        ''' Returns the duration of the animation.
        ''' </summary>
        Public ReadOnly Property Duration As TimeSpan
            Get
                Return If(_animation.Duration.HasTimeSpan, _animation.Duration.TimeSpan, TimeSpan.Zero)
            End Get
        End Property

        Public Property IsPaused As Boolean
            Get
                Return _IsPaused
            End Get
            Private Set(ByVal value As Boolean)
                _IsPaused = value
            End Set
        End Property

        ''' <summary>
        ''' Returns a value that indicates whether the animation is complete.
        ''' </summary>
        Public ReadOnly Property IsComplete As Boolean
            Get
                Return _clock.CurrentState = ClockState.Filling
            End Get
        End Property

        ''' <summary>
        ''' Seeks the animation to the specified frame index.
        ''' </summary>
        ''' <paramname="index">The index of the frame to seek to</param>
        Public Sub GotoFrame(ByVal index As Integer)
            Dim frame = _animation.KeyFrames(index)
            _clockController.Seek(frame.KeyTime.TimeSpan, TimeSeekOrigin.BeginTime)
        End Sub

        ''' <summary>
        ''' Returns the current frame index.
        ''' </summary>
        Public ReadOnly Property CurrentFrame As Integer
            Get
                Dim time = _clock.CurrentTime
                Dim frameAndIndex = _animation.KeyFrames.Cast(Of ObjectKeyFrame)().[Select](Function(f, i) New With {
.Time = f.KeyTime.TimeSpan,
.Index = i
}).FirstOrDefault(Function(fi) fi.Time >= time)
                If frameAndIndex IsNot Nothing Then Return frameAndIndex.Index
                Return -1
            End Get
        End Property

        ''' <summary>
        ''' Pauses the animation.
        ''' </summary>
        Public Sub Pause()
            IsPaused = True
            _clockController.Pause()
        End Sub

        ''' <summary>
        ''' Starts or resumes the animation. If the animation is complete, it restarts from the beginning.
        ''' </summary>
        Public Sub Play()
            IsPaused = False
            If Not _isSuspended Then _clockController.Resume()
        End Sub

        Private _isSuspended As Boolean
        Friend Sub SetSuspended(ByVal isSuspended As Boolean)
            If isSuspended = _isSuspended Then Return

            Dim wasSuspended = _isSuspended
            _isSuspended = isSuspended
            If wasSuspended Then
                If Not IsPaused Then
                    _clockController.Resume()
                End If
            Else
                _clockController.Pause()
            End If
        End Sub

        ''' <summary>
        ''' Raised when the current frame changes.
        ''' </summary>
        Public Event CurrentFrameChanged As EventHandler

        Private Sub OnCurrentFrameChanged()
            Dim handler = CurrentFrameChangedEvent
            If handler IsNot Nothing Then handler(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Finalizes the current object.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

        ''' <summary>
        ''' Disposes the current object.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Disposes the current object
        ''' </summary>
        ''' <paramname="disposing">true to dispose both managed an unmanaged resources, false to dispose only managed resources</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                _image.BeginAnimation(Image.SourceProperty, Nothing)
                RemoveHandler _animation.Completed, AddressOf AnimationCompleted
                Call _sourceDescriptor.RemoveValueChanged(_image, New EventHandler(AddressOf ImageSourceChanged))
                _image.Source = Nothing
            End If
        End Sub
    End Class
End Namespace
