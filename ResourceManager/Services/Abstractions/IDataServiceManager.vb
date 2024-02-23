Namespace ResourceManager.Services.Abstractions
    Public Interface IDataServiceManager
        ReadOnly Property UserDataService As IUserDataService
        ReadOnly Property RoleDataService As IRoleDataService
        ReadOnly Property PermissionDataService As IPermissionDataService
        ReadOnly Property PasswordDataService As IPasswordDataService
        ReadOnly Property BookDataService As IBookDataService
        ReadOnly Property RepositoryDataService As IRepositoryDataService
        ReadOnly Property ArticleDataService As IArticleDataService
        ReadOnly Property ActivityDataService As IActivityDataService
        ReadOnly Property TagDataService As ITagDataService
        ReadOnly Property NoteDataService As INoteDataService
    End Interface
End Namespace
