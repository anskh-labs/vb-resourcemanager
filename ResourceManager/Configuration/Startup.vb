Imports Microsoft.EntityFrameworkCore.Design
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Microsoft.Extensions.Logging
Imports VBNetCore.Mvvm.Abstractions
Imports VBNetCore.Mvvm.Extensions
Imports ResourceManager.Models
Imports ResourceManager.Services.Abstractions
Imports ResourceManager.Services
Imports ResourceManager.Settings
Imports ResourceManager.ViewModels
Imports VBNetCore.DatabaseToolkit.SQLite
Imports ResourceManager.Views

Namespace ResourceManager.Configuration
    Public Module Startup
        Public Sub ConfigureAppConfiguration(ByVal context As HostBuilderContext, ByVal builder As IConfigurationBuilder)
            builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
            builder.AddJsonFile(JsonFileName, [optional]:=False, reloadOnChange:=True)
            builder.AddEnvironmentVariables()
        End Sub

        Public Sub ConfigureLogging(ByVal context As HostBuilderContext, ByVal builder As ILoggingBuilder)
            builder.AddConfiguration(context.Configuration.GetSection(LogSettings.SectionName))
            builder.AddDebug()
        End Sub

        Public Sub ConfigureServices(ByVal configuration As IConfiguration, ByVal services As IServiceCollection)
            'register core mvvm
            services.AddMvvm()

            ' Add functionality to inject IOptions<T>
            services.AddOptions()

            'Can access to generic IConfiguration
            services.AddSingleton(configuration)

            'Add custom Config object so it can be injected as IOptions<AppSettings>
            services.Configure(Of AppSettings)(configuration.GetSection(AppSettings.SectionName))
            services.Configure(Of SystemIntegrationSettings)(configuration.GetSection(SystemIntegrationSettings.SectionName))
            services.Configure(Of DBSettings)(configuration.GetSection(DBSettings.ConnectionStrings))

            'register services
            services.AddScoped(Of IDesignTimeDbContextFactory(Of ResourceManagerDBContext), ResourceManagerDBContextFactory)()
            services.AddScoped(Of IDataServiceManager, DataServiceManager)()
            services.AddScoped(Of IUserDataService, UserDataService)()
            services.AddScoped(Of IRoleDataService, RoleDataService)()
            services.AddScoped(Of IPasswordDataService, PasswordDataService)()
            services.AddScoped(Of IPermissionDataService, PermissionDataService)()
            services.AddScoped(Of IBookDataService, BookDataService)()
            services.AddScoped(Of IRepositoryDataService, RepositoryDataService)()
            services.AddScoped(Of IActivityDataService, ActivityDataService)()
            services.AddScoped(Of IArticleDataService, ArticleDataService)()
            services.AddScoped(Of ITagDataService, TagDataService)()
            services.AddScoped(Of INoteDataService, NoteDataService)()
            services.AddScoped(Of ISQLiteToolkit, SQLiteToolkit)()

            'register mainvm
            services.AddSingleton(Of IPopupable, MainViewModel)()
            services.AddSingleton(Of ViewModelLocator)()

            'register relation viewmodel-view
            services.AddMvvmSingleton(Of MainViewModel, MainView)()
            services.AddMvvmTransient(Of LoginPageViewModel, LoginPageView)()
            services.AddMvvmTransient(Of MainPageViewModel, MainPageView)()
            services.AddMvvmTransient(Of UserPageViewModel, UserPageView)()
            services.AddMvvmTransient(Of PasswordPageViewModel, PasswordPageView)()
            services.AddMvvmTransient(Of EbookPageViewModel, EbookPageView)()
            services.AddMvvmTransient(Of EbookContentViewModel, EbookContentView)()
            services.AddMvvmTransient(Of EbookPopupViewModel, EbookPopupView)()
            services.AddMvvmTransient(Of RepositoryPageViewModel, RepositoryPageView)()
            services.AddMvvmTransient(Of RepositoryContentViewModel, RepositoryContentView)()
            services.AddMvvmTransient(Of RepositoryPopupViewModel, RepositoryPopupView)()
            services.AddMvvmTransient(Of ArticlePageViewModel, ArticlePageView)()
            services.AddMvvmTransient(Of ArticleContentViewModel, ArticleContentView)()
            services.AddMvvmTransient(Of ArticlePopupViewModel, ArticlePopupView)()
            services.AddMvvmTransient(Of ActivityPageViewModel, ActivityPageView)()
            services.AddMvvmTransient(Of ActivityContentViewModel, ActivityContentView)()
            services.AddMvvmTransient(Of ActivityPopupViewModel, ActivityPopupView)()
            services.AddMvvmTransient(Of NotePageViewModel, NotePageView)()
            services.AddMvvmTransient(Of NoteContentViewModel, NoteContentView)()
            services.AddMvvmTransient(Of NotePopupViewModel, NotePopupView)()
            services.AddMvvmTransient(Of UserContentViewModel, UserContentView)()
            services.AddMvvmTransient(Of RoleContentViewModel, RoleContentView)()
            services.AddMvvmTransient(Of PermissionContentViewModel, PermissionContentView)()
            services.AddMvvmTransient(Of UserPopupViewModel, UserPopupView)()
            services.AddMvvmTransient(Of FilterPopupViewModel, FilterPopupView)()
            services.AddMvvmTransient(Of UserRolesPopupViewModel, UserRolesPopupView)()
            services.AddMvvmTransient(Of RolePopupViewModel, RolePopupView)()
            services.AddMvvmTransient(Of RolePermissionsPopupViewModel, RolePermissionsPopupView)()
            services.AddMvvmTransient(Of PermissionPopupViewModel, PermissionPopupView)()
            services.AddMvvmTransient(Of PasswordContentViewModel, PasswordContentView)()
            services.AddMvvmTransient(Of PasswordPopupViewModel, PasswordPopupView)()
            services.AddMvvmTransient(Of TagsContentViewModel, TagsContentView)()
            services.AddMvvmTransient(Of TagsPopupViewModel, TagsPopupView)()
            services.AddMvvmTransient(Of ToolsPageViewModel, ToolsPageView)()
            services.AddMvvmTransient(Of DatabaseContentViewModel, DatabaseContentView)()
            services.AddMvvmTransient(Of EbookOptionsContentViewModel, EbookOptionsContentView)()
            services.AddMvvmTransient(Of ArticleOptionsContentViewModel, ArticleOptionsContentView)()
            services.AddMvvmTransient(Of RepositoryOptionsContentViewModel, RepositoryOptionsContentView)()
        End Sub

        Public Sub ConfigureServices(ByVal context As HostBuilderContext, ByVal services As IServiceCollection)
            ConfigureServices(context.Configuration, services)
        End Sub
    End Module
End Namespace
