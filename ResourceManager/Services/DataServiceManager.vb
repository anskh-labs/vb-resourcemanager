Imports Microsoft.Extensions.DependencyInjection
Imports ResourceManager.Services.Abstractions

Namespace ResourceManager.Services
    Public Class DataServiceManager
        Implements IDataServiceManager
        Public ReadOnly Property UserDataService As IUserDataService Implements IDataServiceManager.UserDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IUserDataService)()
            End Get
        End Property
        Public ReadOnly Property RoleDataService As IRoleDataService Implements IDataServiceManager.RoleDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IRoleDataService)()
            End Get
        End Property
        Public ReadOnly Property PermissionDataService As IPermissionDataService Implements IDataServiceManager.PermissionDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IPermissionDataService)()
            End Get
        End Property
        Public ReadOnly Property PasswordDataService As IPasswordDataService Implements IDataServiceManager.PasswordDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IPasswordDataService)()
            End Get
        End Property
        Public ReadOnly Property BookDataService As IBookDataService Implements IDataServiceManager.BookDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IBookDataService)()
            End Get
        End Property
        Public ReadOnly Property RepositoryDataService As IRepositoryDataService Implements IDataServiceManager.RepositoryDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IRepositoryDataService)()
            End Get
        End Property
        Public ReadOnly Property ArticleDataService As IArticleDataService Implements IDataServiceManager.ArticleDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IArticleDataService)()
            End Get
        End Property
        Public ReadOnly Property ActivityDataService As IActivityDataService Implements IDataServiceManager.ActivityDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of IActivityDataService)()
            End Get
        End Property
        Public ReadOnly Property TagDataService As ITagDataService Implements IDataServiceManager.TagDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of ITagDataService)()
            End Get
        End Property
        Public ReadOnly Property NoteDataService As INoteDataService Implements IDataServiceManager.NoteDataService
            Get
                Return Application.ServiceProvider.GetRequiredService(Of INoteDataService)()
            End Get
        End Property
    End Class
End Namespace
