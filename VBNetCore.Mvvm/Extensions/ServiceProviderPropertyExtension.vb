Imports System
Imports System.Windows

Namespace VBNetCore.Mvvm.Extensions
    ''' <summary>
    ''' Defines the attached property <seecref="ServiceProviderProperty"/>.
    ''' </summary>
    ''' <remarks>
    ''' This property is used to attach a <seecref="IServiceProvider"/>
    ''' to a <seecref="DependencyObject"/>.
    ''' </remarks>
    Public Module ServiceProviderPropertyExtension
        ''' <summary>
        ''' Defines a dependency property to attach a <seecref="IServiceProvider"/>
        ''' to a <seecref="DependencyObject"/>
        ''' </summary>
        Public ReadOnly ServiceProviderProperty As DependencyProperty = DependencyProperty.RegisterAttached("ServiceProvider", GetType(IServiceProvider), GetType(ServiceProviderPropertyExtension), New PropertyMetadata(Nothing))

        ''' <summary>
        ''' Attaches a <seecref="IServiceProvider"/> to <paramrefname="element"/>.
        ''' </summary>
        ''' <paramname="element">The dependency object.</param>
        ''' <paramname="value">The <seecref="IServiceProvider"/>to attach to <paramrefname="element"/>.</param>
        Public Sub SetServiceProvider(ByVal element As DependencyObject, ByVal value As IServiceProvider)
            element.SetValue(ServiceProviderProperty, value)
        End Sub

        ''' <summary>
        ''' Gets the <seecref="IServiceProvider"/> attached to <paramrefname="element"/>.
        ''' </summary>
        ''' <paramname="element"></param>
        Public Function GetServiceProvider(ByVal element As DependencyObject) As IServiceProvider
            Return CType(element.GetValue(ServiceProviderProperty), IServiceProvider)
        End Function
    End Module
End Namespace
