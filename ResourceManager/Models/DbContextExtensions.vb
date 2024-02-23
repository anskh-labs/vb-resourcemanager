Imports Microsoft.EntityFrameworkCore
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace ResourceManager.Models
    Friend Module DbContextExtensions
        <Extension()>
        Public Sub UpdateManyToMany(Of T As Class, TKey)(ByVal db As DbContext, ByVal dbEntries As IEnumerable(Of T), ByVal updatedEntries As IEnumerable(Of T), ByVal keyRetrievalFunction As Func(Of T, TKey))
            Dim oldItems = dbEntries.ToList()
            Dim newItems = updatedEntries.ToList()
            Dim toBeRemoved = oldItems.LeftComplementRight(newItems, keyRetrievalFunction)
            Dim toBeAdded = newItems.LeftComplementRight(oldItems, keyRetrievalFunction)
            Dim toBeUpdated = oldItems.Intersect(newItems, keyRetrievalFunction)

            db.Set(Of T)().RemoveRange(toBeRemoved)
            db.Set(Of T)().AddRange(toBeAdded)
            For Each entity In toBeUpdated
                Dim changed = newItems.[Single](Function(i) keyRetrievalFunction.Invoke(i).Equals(keyRetrievalFunction.Invoke(entity)))
                db.Entry(entity).CurrentValues.SetValues(changed)
            Next
        End Sub
        <Extension()>
        Public Function LeftComplementRight(Of T, TKey)(ByVal left As IEnumerable(Of T), ByVal right As IEnumerable(Of T), ByVal keyRetrievalFunction As Func(Of T, TKey)) As IEnumerable(Of T)
            Dim leftSet = left.ToList()
            Dim rightSet = right.ToList()

            Dim leftSetKeys = leftSet.[Select](keyRetrievalFunction)
            Dim rightSetKeys = rightSet.[Select](keyRetrievalFunction)

            Dim deltaKeys = leftSetKeys.Except(rightSetKeys)
            Dim leftComplementRightSet = leftSet.Where(Function(i) deltaKeys.Contains(keyRetrievalFunction.Invoke(i)))

            Return leftComplementRightSet
        End Function

        <Extension()>
        Public Function Intersect(Of T, TKey)(ByVal left As IEnumerable(Of T), ByVal right As IEnumerable(Of T), ByVal keyRetrievalFunction As Func(Of T, TKey)) As IEnumerable(Of T)
            Dim leftSet = left.ToList()
            Dim rightSet = right.ToList()

            Dim leftSetKeys = leftSet.[Select](keyRetrievalFunction)
            Dim rightSetKeys = rightSet.[Select](keyRetrievalFunction)

            Dim intersectKeys = leftSetKeys.Intersect(rightSetKeys)
            Dim intersectionEntities = leftSet.Where(Function(i) intersectKeys.Contains(keyRetrievalFunction.Invoke(i)))

            Return intersectionEntities
        End Function
    End Module
End Namespace
