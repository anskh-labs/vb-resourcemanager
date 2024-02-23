Imports System.Collections.Generic
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports System.Windows
Imports System
Imports System.Windows.Controls
Imports Microsoft.Xaml.Behaviors.Media

Namespace VBNetCore.Controls.Behaviors
    Friend Module AnimationCache
        Private Structure CacheKey
            Private ReadOnly _source As ImageSource

            Public Sub New(ByVal source As ImageSource)
                _source = source
            End Sub

            Private Overloads Function Equals(ByVal other As CacheKey) As Boolean
                Return ImageEquals(_source, other._source)
            End Function

            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                If ReferenceEquals(Nothing, obj) Then Return False
                If obj.GetType() IsNot [GetType]() Then Return False
                Return Equals(CType(obj, CacheKey))
            End Function

            Public Overrides Function GetHashCode() As Integer
                Return ImageGetHashCode(_source)
            End Function

            Private Shared Function ImageGetHashCode(ByVal image As ImageSource) As Integer
                If image IsNot Nothing Then
                    Dim uri = GetUri(image)
                    If uri IsNot Nothing Then Return uri.GetHashCode()
                End If
                Return 0
            End Function

            Private Shared Function ImageEquals(ByVal x As ImageSource, ByVal y As ImageSource) As Boolean
                If Equals(x, y) Then Return True
                ' They can't both be null or Equals would have returned true
                ' and if any is null, the previous would have detected it
                ' ReSharper disable PossibleNullReferenceException
                If x.GetType() <> y.GetType() Then Return False
                ' ReSharper restore PossibleNullReferenceException
                Dim xUri = GetUri(x)
                Dim yUri = GetUri(y)
                Return xUri IsNot Nothing AndAlso xUri = yUri
            End Function

            Private Shared Function GetUri(ByVal image As ImageSource) As Uri
                Dim bmp = TryCast(image, BitmapImage)
                If bmp IsNot Nothing AndAlso bmp.UriSource IsNot Nothing Then
                    If bmp.UriSource.IsAbsoluteUri Then Return bmp.UriSource
                    If bmp.BaseUri IsNot Nothing Then Return New Uri(bmp.BaseUri, bmp.UriSource)
                End If
                Dim frame = TryCast(image, BitmapFrame)
                If frame IsNot Nothing Then
                    Dim s As String = frame.ToString()
                    If Not Equals(s, frame.GetType().FullName) Then
                        Dim fUri As Uri = Nothing
                        If Uri.TryCreate(s, UriKind.RelativeOrAbsolute, fUri) Then
                            If fUri.IsAbsoluteUri Then Return fUri
                            If frame.BaseUri IsNot Nothing Then Return New Uri(frame.BaseUri, fUri)
                        End If
                    End If
                End If
                Return Nothing
            End Function
        End Structure

        Private ReadOnly _animationCache As Dictionary(Of CacheKey, AnimationCacheEntry) = New Dictionary(Of CacheKey, AnimationCacheEntry)()
        Private ReadOnly _imageControls As Dictionary(Of CacheKey, HashSet(Of Image)) = New Dictionary(Of CacheKey, HashSet(Of Image))()

        Public Sub AddControlForSource(ByVal source As ImageSource, ByVal imageControl As Image)
            Dim cacheKey = New CacheKey(source)
            Dim controls As HashSet(Of Image) = Nothing

            If Not _imageControls.TryGetValue(cacheKey, controls) Then
                If controls Is Nothing Then controls = New HashSet(Of Image)()
                _imageControls(cacheKey) = controls
            End If

            controls.Add(imageControl)
        End Sub

        Public Sub RemoveControlForSource(ByVal source As ImageSource, ByVal imageControl As Image)
            Dim cacheKey = New CacheKey(source)
            Dim controls As HashSet(Of Image) = Nothing

            If _imageControls.TryGetValue(cacheKey, controls) Then
                If controls.Remove(imageControl) Then
                    If controls.Count = 0 Then
                        _animationCache.Remove(cacheKey)
                        _imageControls.Remove(cacheKey)
                    End If
                End If
            End If
        End Sub

        Public Sub Add(ByVal source As ImageSource, ByVal entry As AnimationCacheEntry)
            Dim key = New CacheKey(source)
            _animationCache(key) = entry
        End Sub

        Public Sub Remove(ByVal source As ImageSource)
            Dim key = New CacheKey(source)
            _animationCache.Remove(key)
        End Sub

        Public Function [Get](ByVal source As ImageSource) As AnimationCacheEntry
            Dim key = New CacheKey(source)
            Dim entry As AnimationCacheEntry = Nothing
            _animationCache.TryGetValue(key, entry)
            Return entry
        End Function

    End Module

    Friend Class AnimationCacheEntry
        Public Sub New(ByVal keyFrames As ObjectKeyFrameCollection, ByVal duration As Duration, ByVal repeatCountFromMetadata As Integer)
            Me.KeyFrames = keyFrames
            Me.Duration = duration
            Me.RepeatCountFromMetadata = repeatCountFromMetadata
        End Sub

        Public ReadOnly Property KeyFrames As ObjectKeyFrameCollection
        Public ReadOnly Property Duration As Duration
        Public ReadOnly Property RepeatCountFromMetadata As Integer
    End Class
End Namespace
