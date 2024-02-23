Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.IO.Packaging
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports VBNetCore.Controls.Behaviors.Decoding
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices

Namespace VBNetCore.Controls.Behaviors
    ''' <summary>
    ''' Provides attached properties that display animated GIFs in a standard Image control.
    ''' </summary>
    Public Module ImageBehavior
#Region "Public attached properties and events"

        ''' <summary>
        ''' Gets the value of the <c>AnimatedSource</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>The currently displayed animated image.</returns>
        <AttachedPropertyBrowsableForType(GetType(Image))>
        Public Function GetAnimatedSource(ByVal obj As Image) As ImageSource
            Return CType(obj.GetValue(AnimatedSourceProperty), ImageSource)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>AnimatedSource</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element on which to set the property value.</param>
        ''' <paramname="value">The animated image to display.</param>
        Public Sub SetAnimatedSource(ByVal obj As Image, ByVal value As ImageSource)
            obj.SetValue(AnimatedSourceProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AnimatedSource</c> attached property.
        ''' </summary>
        Public ReadOnly AnimatedSourceProperty As DependencyProperty = DependencyProperty.RegisterAttached("AnimatedSource", GetType(ImageSource), GetType(ImageBehavior), New PropertyMetadata(Nothing, AddressOf AnimatedSourceChanged))

        ''' <summary>
        ''' Gets the value of the <c>RepeatBehavior</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>The repeat behavior of the animated image.</returns>
        <AttachedPropertyBrowsableForType(GetType(Image))>
        Public Function GetRepeatBehavior(ByVal obj As Image) As RepeatBehavior
            Return obj.GetValue(RepeatBehaviorProperty)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>RepeatBehavior</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element on which to set the property value.</param>
        ''' <paramname="value">The repeat behavior of the animated image.</param>
        Public Sub SetRepeatBehavior(ByVal obj As Image, ByVal value As RepeatBehavior)
            obj.SetValue(RepeatBehaviorProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>RepeatBehavior</c> attached property.
        ''' </summary>
        Public ReadOnly RepeatBehaviorProperty As DependencyProperty = DependencyProperty.RegisterAttached("RepeatBehavior", GetType(RepeatBehavior), GetType(ImageBehavior), New PropertyMetadata(Nothing, AddressOf AnimationPropertyChanged))

        ''' <summary>
        ''' Gets the value of the <c>AnimationSpeedRatio</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>The speed ratio for the animated image.</returns>
        Public Function GetAnimationSpeedRatio(ByVal obj As DependencyObject) As Nullable(Of Double)
            Return obj.GetValue(AnimationSpeedRatioProperty)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>AnimationSpeedRatio</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element on which to set the property value.</param>
        ''' <paramname="value">The speed ratio of the animated image.</param>
        ''' <remarks>The <c>AnimationSpeedRatio</c> and <c>AnimationDuration</c> properties are mutually exclusive, only one can be set at a time.</remarks>
        Public Sub SetAnimationSpeedRatio(ByVal obj As DependencyObject, ByVal value As Double)
            obj.SetValue(AnimationSpeedRatioProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AnimationSpeedRatio</c> attached property.
        ''' </summary>
        Public ReadOnly AnimationSpeedRatioProperty As DependencyProperty = DependencyProperty.RegisterAttached("AnimationSpeedRatio", GetType(Double), GetType(ImageBehavior), New PropertyMetadata(Nothing, AddressOf AnimationPropertyChanged))

        ''' <summary>
        ''' Gets the value of the <c>AnimationDuration</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>The duration for the animated image.</returns>
        Public Function GetAnimationDuration(ByVal obj As DependencyObject) As Nullable(Of Duration)
            Return obj.GetValue(AnimationDurationProperty)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>AnimationDuration</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element on which to set the property value.</param>
        ''' <paramname="value">The duration of the animated image.</param>
        ''' <remarks>The <c>AnimationSpeedRatio</c> and <c>AnimationDuration</c> properties are mutually exclusive, only one can be set at a time.</remarks>
        Public Sub SetAnimationDuration(ByVal obj As DependencyObject, ByVal value As Duration)
            obj.SetValue(AnimationDurationProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AnimationDuration</c> attached property.
        ''' </summary>
        Public ReadOnly AnimationDurationProperty As DependencyProperty = DependencyProperty.RegisterAttached("AnimationDuration", GetType(Duration), GetType(ImageBehavior), New PropertyMetadata(Nothing, AddressOf AnimationPropertyChanged))

        ''' <summary>
        ''' Gets the value of the <c>AnimateInDesignMode</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>true if GIF animations are shown in design mode; false otherwise.</returns>
        Public Function GetAnimateInDesignMode(ByVal obj As DependencyObject) As Boolean
            Return obj.GetValue(AnimateInDesignModeProperty)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>AnimateInDesignMode</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element on which to set the property value.</param>
        ''' <paramname="value">true to show GIF animations in design mode; false otherwise.</param>
        Public Sub SetAnimateInDesignMode(ByVal obj As DependencyObject, ByVal value As Boolean)
            obj.SetValue(AnimateInDesignModeProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AnimateInDesignMode</c> attached property.
        ''' </summary>
        Public ReadOnly AnimateInDesignModeProperty As DependencyProperty = DependencyProperty.RegisterAttached("AnimateInDesignMode", GetType(Boolean), GetType(ImageBehavior), New FrameworkPropertyMetadata(False, FrameworkPropertyMetadataOptions.Inherits, AddressOf AnimateInDesignModeChanged))

        ''' <summary>
        ''' Gets the value of the <c>AutoStart</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <returns>true if the animation should start immediately when loaded. Otherwise, false.</returns>
        <AttachedPropertyBrowsableForType(GetType(Image))>
        Public Function GetAutoStart(ByVal obj As Image) As Boolean
            Return obj.GetValue(AutoStartProperty)
        End Function

        ''' <summary>
        ''' Sets the value of the <c>AutoStart</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="obj">The element from which to read the property value.</param>
        ''' <paramname="value">true if the animation should start immediately when loaded. Otherwise, false.</param>
        ''' <remarks>The default value is true.</remarks>
        Public Sub SetAutoStart(ByVal obj As Image, ByVal value As Boolean)
            obj.SetValue(AutoStartProperty, value)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AutoStart</c> attached property.
        ''' </summary>
        Public ReadOnly AutoStartProperty As DependencyProperty = DependencyProperty.RegisterAttached("AutoStart", GetType(Boolean), GetType(ImageBehavior), New PropertyMetadata(True))

        ''' <summary>
        ''' Gets the animation controller for the specified <c>Image</c> control.
        ''' </summary>
        ''' <paramname="imageControl"></param>
        ''' <returns></returns>
        Public Function GetAnimationController(ByVal imageControl As Image) As ImageAnimationController
            Return CType(imageControl.GetValue(AnimationControllerPropertyKey.DependencyProperty), ImageAnimationController)
        End Function

        Private Sub SetAnimationController(ByVal obj As DependencyObject, ByVal value As ImageAnimationController)
            obj.SetValue(AnimationControllerPropertyKey, value)
        End Sub

        Private ReadOnly AnimationControllerPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterAttachedReadOnly("AnimationController", GetType(ImageAnimationController), GetType(ImageBehavior), New PropertyMetadata(Nothing))

        ''' <summary>
        ''' Gets the value of the <c>IsAnimationLoaded</c> attached property for the specified object.
        ''' </summary>
        ''' <paramname="image">The element from which to read the property value.</param>
        ''' <returns>true if the animation is loaded. Otherwise, false.</returns>
        Public Function GetIsAnimationLoaded(ByVal image As Image) As Boolean
            Return image.GetValue(IsAnimationLoadedProperty)
        End Function

        Private Sub SetIsAnimationLoaded(ByVal image As Image, ByVal value As Boolean)
            image.SetValue(IsAnimationLoadedPropertyKey, value)
        End Sub

        Private ReadOnly IsAnimationLoadedPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsAnimationLoaded", GetType(Boolean), GetType(ImageBehavior), New PropertyMetadata(False))

        ''' <summary>
        ''' Identifies the <c>IsAnimationLoaded</c> attached property.
        ''' </summary>
        Public ReadOnly IsAnimationLoadedProperty As DependencyProperty = IsAnimationLoadedPropertyKey.DependencyProperty

        ''' <summary>
        ''' Identifies the <c>AnimationLoaded</c> attached event.
        ''' </summary>
        Public ReadOnly AnimationLoadedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("AnimationLoaded", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(ImageBehavior))

        ''' <summary>
        ''' Adds a handler for the AnimationLoaded attached event.
        ''' </summary>
        ''' <paramname="image">The UIElement that listens to this event.</param>
        ''' <paramname="handler">The event handler to be added.</param>
        Public Sub AddAnimationLoadedHandler(ByVal image As Image, ByVal handler As RoutedEventHandler)
            If image Is Nothing Then Throw New ArgumentNullException("image")
            If handler Is Nothing Then Throw New ArgumentNullException("handler")
            image.AddHandler(AnimationLoadedEvent, handler)
        End Sub

        ''' <summary>
        ''' Removes a handler for the AnimationLoaded attached event.
        ''' </summary>
        ''' <paramname="image">The UIElement that listens to this event.</param>
        ''' <paramname="handler">The event handler to be removed.</param>
        Public Sub RemoveAnimationLoadedHandler(ByVal image As Image, ByVal handler As RoutedEventHandler)
            If image Is Nothing Then Throw New ArgumentNullException("image")
            If handler Is Nothing Then Throw New ArgumentNullException("handler")
            image.RemoveHandler(AnimationLoadedEvent, handler)
        End Sub

        ''' <summary>
        ''' Identifies the <c>AnimationCompleted</c> attached event.
        ''' </summary>
        Public ReadOnly AnimationCompletedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("AnimationCompleted", RoutingStrategy.Bubble, GetType(RoutedEventHandler), GetType(ImageBehavior))

        ''' <summary>
        ''' Adds a handler for the AnimationCompleted attached event.
        ''' </summary>
        ''' <paramname="d">The UIElement that listens to this event.</param>
        ''' <paramname="handler">The event handler to be added.</param>
        Public Sub AddAnimationCompletedHandler(ByVal d As Image, ByVal handler As RoutedEventHandler)
            Dim element = TryCast(d, UIElement)
            If element Is Nothing Then Return
            element.AddHandler(AnimationCompletedEvent, handler)
        End Sub

        ''' <summary>
        ''' Removes a handler for the AnimationCompleted attached event.
        ''' </summary>
        ''' <paramname="d">The UIElement that listens to this event.</param>
        ''' <paramname="handler">The event handler to be removed.</param>
        Public Sub RemoveAnimationCompletedHandler(ByVal d As Image, ByVal handler As RoutedEventHandler)
            Dim element = TryCast(d, UIElement)
            If element Is Nothing Then Return
            element.RemoveHandler(AnimationCompletedEvent, handler)
        End Sub

#End Region

        Private Sub AnimatedSourceChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim imageControl As Image = TryCast(o, Image)
            If imageControl Is Nothing Then Return

            Dim oldValue = TryCast(e.OldValue, ImageSource)
            Dim newValue = TryCast(e.NewValue, ImageSource)
            If ReferenceEquals(oldValue, newValue) Then
                If imageControl.IsLoaded Then
                    Dim isAnimLoaded = GetIsAnimationLoaded(imageControl)
                    If Not isAnimLoaded Then InitAnimationOrImage(imageControl)
                End If
                Return
            End If
            If oldValue IsNot Nothing Then
                RemoveHandler imageControl.Loaded, AddressOf ImageControlLoaded
                RemoveHandler imageControl.Unloaded, AddressOf ImageControlUnloaded
                RemoveHandler imageControl.IsVisibleChanged, AddressOf VisibilityChanged

                RemoveControlForSource(oldValue, imageControl)
                Dim controller = GetAnimationController(imageControl)
                If controller IsNot Nothing Then controller.Dispose()
                imageControl.Source = Nothing
            End If
            If newValue IsNot Nothing Then
                AddHandler imageControl.Loaded, AddressOf ImageControlLoaded
                AddHandler imageControl.Unloaded, AddressOf ImageControlUnloaded
                AddHandler imageControl.IsVisibleChanged, AddressOf VisibilityChanged

                If imageControl.IsLoaded Then InitAnimationOrImage(imageControl)
            End If
        End Sub

        Private Sub VisibilityChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
            Dim img As Image = TryCast(sender, Image)

            If img IsNot Nothing AndAlso img.IsLoaded Then
                Dim controller = GetAnimationController(img)
                If controller IsNot Nothing Then
                    Dim isVisible As Boolean = e.NewValue
                    controller.SetSuspended(Not isVisible)
                End If
            End If
        End Sub

        Private Sub ImageControlLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim imageControl As Image = TryCast(sender, Image)
            If imageControl Is Nothing Then Return
            InitAnimationOrImage(imageControl)
        End Sub

        Private Sub ImageControlUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim imageControl As Image = TryCast(sender, Image)
            If imageControl Is Nothing Then Return
            Dim source = GetAnimatedSource(imageControl)
            If source IsNot Nothing Then RemoveControlForSource(source, imageControl)
            Dim controller = GetAnimationController(imageControl)
            If controller IsNot Nothing Then controller.Dispose()
        End Sub

        Private Sub AnimationPropertyChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim imageControl As Image = TryCast(o, Image)
            If imageControl Is Nothing Then Return

            Dim source = GetAnimatedSource(imageControl)
            If source IsNot Nothing Then
                If imageControl.IsLoaded Then InitAnimationOrImage(imageControl)
            End If
        End Sub

        Private Sub AnimateInDesignModeChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim imageControl As Image = TryCast(o, Image)
            If imageControl Is Nothing Then Return

            Dim newValue As Boolean = e.NewValue

            Dim source = GetAnimatedSource(imageControl)
            If source IsNot Nothing AndAlso imageControl.IsLoaded Then
                If newValue Then
                    InitAnimationOrImage(imageControl)
                Else
                    imageControl.BeginAnimation(Image.SourceProperty, Nothing)
                End If
            End If
        End Sub

        Private Sub InitAnimationOrImage(ByVal imageControl As Image)
            Dim controller = GetAnimationController(imageControl)
            If controller IsNot Nothing Then controller.Dispose()
            SetAnimationController(imageControl, Nothing)
            SetIsAnimationLoaded(imageControl, False)

            Dim rawSource = GetAnimatedSource(imageControl)
            Dim source As BitmapSource = TryCast(rawSource, BitmapSource)
            If source Is Nothing AndAlso rawSource IsNot Nothing Then
                imageControl.Source = rawSource
                Return
            End If

            Dim isInDesignMode = DesignerProperties.GetIsInDesignMode(imageControl)
            Dim animateInDesignMode = GetAnimateInDesignMode(imageControl)
            Dim shouldAnimate = Not isInDesignMode OrElse animateInDesignMode

            ' For a BitmapImage with a relative UriSource, the loading is deferred until
            ' BaseUri is set. This method will be called again when BaseUri is set.
            Dim isLoadingDeferred = ImageBehavior.IsLoadingDeferred(source, imageControl)

            If source IsNot Nothing AndAlso shouldAnimate AndAlso Not isLoadingDeferred Then
                ' Case of image being downloaded: retry after download is complete
                If source.IsDownloading Then
                    Dim handler As EventHandler = Nothing
                    handler = Sub(sender, args)
                                  RemoveHandler source.DownloadCompleted, handler
                                  InitAnimationOrImage(imageControl)
                              End Sub
                    AddHandler source.DownloadCompleted, handler
                    imageControl.Source = source
                    Return
                End If

                Dim animation = GetAnimation(imageControl, source)
                If animation IsNot Nothing Then
                    If animation.KeyFrames.Count > 0 Then
                        ' For some reason, it sometimes throws an exception the first time... the second time it works.
                        TryTwice(Sub() imageControl.Source = CType(animation.KeyFrames(0).Value, ImageSource))
                    Else
                        imageControl.Source = source
                    End If

                    controller = New ImageAnimationController(imageControl, animation, GetAutoStart(imageControl))
                    SetAnimationController(imageControl, controller)
                    SetIsAnimationLoaded(imageControl, True)
                    imageControl.RaiseEvent(New RoutedEventArgs(AnimationLoadedEvent, imageControl))
                    Return
                End If
            End If
            imageControl.Source = source
            If source IsNot Nothing Then
                SetIsAnimationLoaded(imageControl, True)
                imageControl.RaiseEvent(New RoutedEventArgs(AnimationLoadedEvent, imageControl))
            End If
        End Sub

        Private Function GetAnimation(ByVal imageControl As Image, ByVal source As BitmapSource) As ObjectAnimationUsingKeyFrames
            Dim cacheEntry = [Get](source)
            Dim gifMetadata As GifFile = Nothing
            If cacheEntry Is Nothing Then
                Dim decoder = TryCast(GetDecoder(source, imageControl, gifMetadata), GifBitmapDecoder)
                If decoder IsNot Nothing AndAlso decoder.Frames.Count > 1 Then
                    Dim fullSize = GetFullSize(decoder, gifMetadata)
                    Dim index = 0
                    Dim keyFrames = New ObjectKeyFrameCollection()
                    Dim totalDuration = TimeSpan.Zero
                    Dim baseFrame As BitmapSource = Nothing
                    For Each rawFrame In decoder.Frames
                        Dim metadata = GetFrameMetadata(decoder, gifMetadata, index)

                        Dim frame = MakeFrame(fullSize, rawFrame, metadata, baseFrame)
                        Dim keyFrame = New DiscreteObjectKeyFrame(frame, totalDuration)
                        keyFrames.Add(keyFrame)

                        totalDuration += metadata.Delay

                        Select Case metadata.DisposalMethod
                            Case FrameDisposalMethod.None, FrameDisposalMethod.DoNotDispose
                                baseFrame = frame
                            Case FrameDisposalMethod.RestoreBackground
                                If IsFullFrame(metadata, fullSize) Then
                                    baseFrame = Nothing
                                Else
                                    baseFrame = ClearArea(frame, metadata)
                                End If
                                                            ' Reuse same base frame
                            Case FrameDisposalMethod.RestorePrevious
                        End Select

                        index += 1
                    Next

                    Dim repeatCount = GetRepeatCountFromMetadata(decoder, gifMetadata)
                    cacheEntry = New AnimationCacheEntry(keyFrames, totalDuration, repeatCount)
                    Add(source, cacheEntry)
                End If
            End If

            If cacheEntry IsNot Nothing Then
                Dim animation = New ObjectAnimationUsingKeyFrames With {
    .KeyFrames = cacheEntry.KeyFrames,
    .Duration = cacheEntry.Duration,
    .RepeatBehavior = GetActualRepeatBehavior(imageControl, cacheEntry.RepeatCountFromMetadata),
    .SpeedRatio = GetActualSpeedRatio(imageControl, cacheEntry.Duration)
}

                AddControlForSource(source, imageControl)
                Return animation
            End If

            Return Nothing
        End Function

        Private Function GetActualSpeedRatio(ByVal imageControl As Image, ByVal naturalDuration As Duration) As Double
            Dim speedRatio = GetAnimationSpeedRatio(imageControl)
            Dim duration = GetAnimationDuration(imageControl)

            If speedRatio.HasValue AndAlso duration.HasValue Then Throw New InvalidOperationException("Cannot set both AnimationSpeedRatio and AnimationDuration")

            If speedRatio.HasValue Then Return speedRatio.Value

            If duration.HasValue Then
                If Not duration.Value.HasTimeSpan Then Throw New InvalidOperationException("AnimationDuration cannot be Automatic or Forever")
                If duration.Value.TimeSpan.Ticks <= 0 Then Throw New InvalidOperationException("AnimationDuration must be strictly positive")
                Return naturalDuration.TimeSpan.Ticks / duration.Value.TimeSpan.Ticks
            End If

            Return 1.0
        End Function

        Private Function ClearArea(ByVal frame As BitmapSource, ByVal metadata As FrameMetadata) As BitmapSource
            Dim visual As DrawingVisual = New DrawingVisual()
            Using context = visual.RenderOpen()
                Dim fullRect = New Rect(0, 0, frame.PixelWidth, frame.PixelHeight)
                Dim clearRect = New Rect(metadata.Left, metadata.Top, metadata.Width, metadata.Height)
                Dim clip = Geometry.Combine(New RectangleGeometry(fullRect), New RectangleGeometry(clearRect), GeometryCombineMode.Exclude, Nothing)
                context.PushClip(clip)
                context.DrawImage(frame, fullRect)
            End Using

            Dim bitmap = New RenderTargetBitmap(frame.PixelWidth, frame.PixelHeight, frame.DpiX, frame.DpiY, PixelFormats.Pbgra32)
            bitmap.Render(visual)

            Dim result = New WriteableBitmap(bitmap)

            If result.CanFreeze AndAlso Not result.IsFrozen Then result.Freeze()
            Return result
        End Function

        Private Sub TryTwice(ByVal action As Action)
            Try
                action()
            Catch
                action()
            End Try
        End Sub

        Private Function IsLoadingDeferred(ByVal source As BitmapSource, ByVal imageControl As Image) As Boolean
            Dim bmp = TryCast(source, BitmapImage)
            If bmp Is Nothing Then Return False
            If bmp.UriSource IsNot Nothing AndAlso Not bmp.UriSource.IsAbsoluteUri Then Return bmp.BaseUri Is Nothing AndAlso TryCast(imageControl, IUriContext).BaseUri Is Nothing
            Return False
        End Function

        Private Function GetDecoder(ByVal image As BitmapSource, ByVal imageControl As Image, <Out> ByRef gifFile As GifFile) As BitmapDecoder
            gifFile = Nothing
            Dim decoder As BitmapDecoder = Nothing
            Dim stream As Stream = Nothing
            Dim uri As Uri = Nothing
            Dim createOptions = BitmapCreateOptions.None

            Dim bmp = TryCast(image, BitmapImage)
            If bmp IsNot Nothing Then
                createOptions = bmp.CreateOptions
                If bmp.StreamSource IsNot Nothing Then
                    stream = bmp.StreamSource
                ElseIf bmp.UriSource IsNot Nothing Then
                    uri = bmp.UriSource
                    If Not uri.IsAbsoluteUri Then
                        Dim baseUri = If(bmp.BaseUri, TryCast(imageControl, IUriContext).BaseUri)
                        If baseUri IsNot Nothing Then uri = New Uri(baseUri, uri)
                    End If
                End If
            Else
                Dim frame As BitmapFrame = TryCast(image, BitmapFrame)
                If frame IsNot Nothing Then
                    decoder = frame.Decoder
                    Uri.TryCreate(frame.BaseUri, frame.ToString(), uri)
                End If
            End If

            If decoder Is Nothing Then
                If stream IsNot Nothing Then
                    stream.Position = 0
                    decoder = BitmapDecoder.Create(stream, createOptions, BitmapCacheOption.OnLoad)
                ElseIf uri IsNot Nothing AndAlso uri.IsAbsoluteUri Then
                    decoder = BitmapDecoder.Create(uri, createOptions, BitmapCacheOption.OnLoad)
                End If
            End If

            If TypeOf decoder Is GifBitmapDecoder AndAlso Not CanReadNativeMetadata(decoder) Then
                If stream IsNot Nothing Then
                    stream.Position = 0
                    gifFile = GifFile.ReadGifFile(stream, True)
                ElseIf uri IsNot Nothing Then
                    gifFile = DecodeGifFile(uri)
                Else
                    Throw New InvalidOperationException("Can't get URI or Stream from the source. AnimatedSource should be either a BitmapImage, or a BitmapFrame constructed from a URI.")
                End If
            End If
            If decoder Is Nothing Then
                Throw New InvalidOperationException("Can't get a decoder from the source. AnimatedSource should be either a BitmapImage or a BitmapFrame.")
            End If
            Return decoder
        End Function

        Private Function CanReadNativeMetadata(ByVal decoder As BitmapDecoder) As Boolean
            Try
                Dim m = decoder.Metadata
                Return m IsNot Nothing
            Catch
                Return False
            End Try
        End Function

        Private Function DecodeGifFile(ByVal uri As Uri) As GifFile
            Dim stream As Stream = Nothing
            If Equals(uri.Scheme, PackUriHelper.UriSchemePack) Then
                Dim sri As StreamResourceInfo
                If Equals(uri.Authority, "siteoforigin:,,,") Then
                    sri = Application.GetRemoteStream(uri)
                Else
                    sri = Application.GetResourceStream(uri)
                End If

                If sri IsNot Nothing Then stream = sri.Stream
            Else
#Disable Warning SYSLIB0014 ' Type or member is obsolete
                Dim wc As New WebClient()
#Enable Warning SYSLIB0014 ' Type or member is obsolete
                stream = wc.OpenRead(uri)
            End If
            If stream IsNot Nothing Then
                Using stream
                    Return GifFile.ReadGifFile(stream, True)
                End Using
            End If
            Return Nothing
        End Function

        Private Function IsFullFrame(ByVal metadata As FrameMetadata, ByVal fullSize As Int32Size) As Boolean
            Return metadata.Left = 0 AndAlso metadata.Top = 0 AndAlso metadata.Width = fullSize.Width AndAlso metadata.Height = fullSize.Height
        End Function

        Private Function MakeFrame(ByVal fullSize As Int32Size, ByVal rawFrame As BitmapSource, ByVal metadata As FrameMetadata, ByVal baseFrame As BitmapSource) As BitmapSource
            If baseFrame Is Nothing AndAlso IsFullFrame(metadata, fullSize) Then
                ' No previous image to combine with, and same size as the full image
                ' Just return the frame as is
                Return rawFrame
            End If

            Dim visual As DrawingVisual = New DrawingVisual()
            Using context = visual.RenderOpen()
                If baseFrame IsNot Nothing Then
                    Dim fullRect = New Rect(0, 0, fullSize.Width, fullSize.Height)
                    context.DrawImage(baseFrame, fullRect)
                End If

                Dim rect = New Rect(metadata.Left, metadata.Top, metadata.Width, metadata.Height)
                context.DrawImage(rawFrame, rect)
            End Using
            Dim bitmap = New RenderTargetBitmap(fullSize.Width, fullSize.Height, 96, 96, PixelFormats.Pbgra32)
            bitmap.Render(visual)

            Dim result = New WriteableBitmap(bitmap)

            If result.CanFreeze AndAlso Not result.IsFrozen Then result.Freeze()
            Return result
        End Function

        Private Function GetActualRepeatBehavior(ByVal imageControl As Image, ByVal repeatCountFromMetadata As Integer) As RepeatBehavior
            ' If specified explicitly, use this value
            Dim repeatBehavior = GetRepeatBehavior(imageControl)
            If repeatBehavior <> Nothing Then Return repeatBehavior

            If repeatCountFromMetadata = 0 Then Return RepeatBehavior.Forever
            Return New RepeatBehavior(repeatCountFromMetadata)
        End Function

        Private Function GetRepeatCountFromMetadata(ByVal decoder As BitmapDecoder, ByVal gifMetadata As GifFile) As Integer
            If gifMetadata IsNot Nothing Then
                Return gifMetadata.RepeatCount
            Else
                Dim ext = GetApplicationExtension(decoder, "NETSCAPE2.0")
                If ext IsNot Nothing Then
                    Dim bytes = ext.GetQueryOrNull(Of Byte())("/Data")
                    If bytes IsNot Nothing AndAlso bytes.Length >= 4 Then Return BitConverter.ToUInt16(bytes, 2)
                End If
                Return 1
            End If
        End Function

        Private Function GetApplicationExtension(ByVal decoder As BitmapDecoder, ByVal application As String) As BitmapMetadata
            Dim count = 0
            Dim query = "/appext"
            Dim extension = decoder.Metadata.GetQueryOrNull(Of BitmapMetadata)(query)
            While extension IsNot Nothing
                Dim bytes = extension.GetQueryOrNull(Of Byte())("/Application")
                If bytes IsNot Nothing Then
                    Dim extApplication = Encoding.ASCII.GetString(bytes)
                    If Equals(extApplication, application) Then Return extension
                End If
                query = String.Format("/[{0}]appext", Threading.Interlocked.Increment(count))
                extension = decoder.Metadata.GetQueryOrNull(Of BitmapMetadata)(query)
            End While
            Return Nothing
        End Function

        Private Function GetFrameMetadata(ByVal decoder As BitmapDecoder, ByVal gifMetadata As GifFile, ByVal frameIndex As Integer) As FrameMetadata
            If gifMetadata IsNot Nothing AndAlso gifMetadata.Frames.Count > frameIndex Then
                Return GetFrameMetadata(gifMetadata.Frames(frameIndex))
            End If

            Return GetFrameMetadata(decoder.Frames(frameIndex))
        End Function

        Private Function GetFrameMetadata(ByVal frame As BitmapFrame) As FrameMetadata
            Dim metadata = CType(frame.Metadata, BitmapMetadata)
            Dim delay = TimeSpan.FromMilliseconds(100)
            Dim metadataDelay = metadata.GetQueryOrDefault("/grctlext/Delay", 10)
            If metadataDelay <> 0 Then delay = TimeSpan.FromMilliseconds(metadataDelay * 10)
            Dim disposalMethod = CType(metadata.GetQueryOrDefault("/grctlext/Disposal", 0), FrameDisposalMethod)
            Dim frameMetadata = New FrameMetadata With {
    .Left = metadata.GetQueryOrDefault("/imgdesc/Left", 0),
    .Top = metadata.GetQueryOrDefault("/imgdesc/Top", 0),
    .Width = metadata.GetQueryOrDefault("/imgdesc/Width", frame.PixelWidth),
    .Height = metadata.GetQueryOrDefault("/imgdesc/Height", frame.PixelHeight),
    .Delay = delay,
    .DisposalMethod = disposalMethod
}
            Return frameMetadata
        End Function

        Private Function GetFrameMetadata(ByVal gifMetadata As GifFrame) As FrameMetadata
            Dim d = gifMetadata.Descriptor
            Dim frameMetadata = New FrameMetadata With {
    .Left = d.Left,
    .Top = d.Top,
    .Width = d.Width,
    .Height = d.Height,
    .Delay = TimeSpan.FromMilliseconds(100),
    .DisposalMethod = FrameDisposalMethod.None
}

            Dim gce = gifMetadata.Extensions.OfType(Of GifGraphicControlExtension)().FirstOrDefault()
            If gce IsNot Nothing Then
                If gce.Delay <> 0 Then frameMetadata.Delay = TimeSpan.FromMilliseconds(gce.Delay)
                frameMetadata.DisposalMethod = CType(gce.DisposalMethod, FrameDisposalMethod)
            End If
            Return frameMetadata
        End Function

        Private Function GetFullSize(ByVal decoder As BitmapDecoder, ByVal gifMetadata As GifFile) As Int32Size
            If gifMetadata IsNot Nothing Then
                Dim lsd = gifMetadata.Header.LogicalScreenDescriptor
                Return New Int32Size(lsd.Width, lsd.Height)
            End If
            Dim width = decoder.Metadata.GetQueryOrDefault("/logscrdesc/Width", 0)
            Dim height = decoder.Metadata.GetQueryOrDefault("/logscrdesc/Height", 0)
            Return New Int32Size(width, height)
        End Function

        Private Structure Int32Size

            Private _Width As Integer, _Height As Integer
            Public Sub New(ByVal width As Integer, ByVal height As Integer)
                Me.New()
                Me.Width = width
                Me.Height = height
            End Sub

            Public Property Width As Integer
                Get
                    Return _Width
                End Get
                Private Set(ByVal value As Integer)
                    _Width = value
                End Set
            End Property

            Public Property Height As Integer
                Get
                    Return _Height
                End Get
                Private Set(ByVal value As Integer)
                    _Height = value
                End Set
            End Property
        End Structure

        Private Class FrameMetadata
            Public Property Left As Integer
            Public Property Top As Integer
            Public Property Width As Integer
            Public Property Height As Integer
            Public Property Delay As TimeSpan
            Public Property DisposalMethod As FrameDisposalMethod
        End Class

        Private Enum FrameDisposalMethod
            None = 0
            DoNotDispose = 1
            RestoreBackground = 2
            RestorePrevious = 3
        End Enum

        <Extension()>
        Private Function GetQueryOrDefault(Of T)(ByVal metadata As BitmapMetadata, ByVal query As String, ByVal defaultValue As T) As T
            If metadata.ContainsQuery(query) Then Return Convert.ChangeType(metadata.GetQuery(query), GetType(T))
            Return defaultValue
        End Function

        <Extension()>
        Private Function GetQueryOrNull(Of T As Class)(ByVal metadata As BitmapMetadata, ByVal query As String) As T
            If metadata.ContainsQuery(query) Then Return TryCast(metadata.GetQuery(query), T)
            Return Nothing
        End Function

    End Module
End Namespace
