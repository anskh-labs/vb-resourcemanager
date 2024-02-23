Imports System
Imports System.Collections.Generic
Imports System.Linq.Expressions
Imports System.Threading.Tasks

Namespace ResourceManager.Services.Abstractions
    Public Interface IDataService(Of T)
        Function GetAll() As Task(Of IList(Of T))
        Function [Get](ByVal id As Integer) As Task(Of T)
        Function Create(ByVal entity As T) As Task(Of T)
        Function Update(ByVal id As Integer, ByVal entity As T) As Task(Of T)
        Function Delete(ByVal id As Integer) As Task(Of Boolean)
        Function GetWithRawSql(ByVal query As String, ParamArray parameters As Object()) As Task(Of IList(Of T))
        Function GetWhere(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of IList(Of T))
        Function FirstOrDefault(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of T)
        Function CountAll() As Task(Of Integer)
        Function CountWhere(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of Integer)
        Function SingleOrDefault(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Task(Of T)
    End Interface
End Namespace
