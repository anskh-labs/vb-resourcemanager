Imports Microsoft.Extensions.Hosting

Namespace ResourceManager.Configuration
    Friend Module HostFactory
        Public Function Create() As IHost
            Dim hostBuilder = Host.CreateDefaultBuilder().ConfigureAppConfiguration(AddressOf ConfigureAppConfiguration).ConfigureServices(AddressOf ConfigureServices).ConfigureLogging(New Action(Of HostBuilderContext, Microsoft.Extensions.Logging.ILoggingBuilder)(AddressOf ConfigureLogging))

            Return hostBuilder.Build()
        End Function
    End Module
End Namespace
