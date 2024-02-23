Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System

Namespace ResourceManager.Helpers
    Public Class Filter(Of T As Class)
        Public Function FilteredData(ByVal filterParam As FilterParams, ByVal data As IEnumerable(Of T)) As IEnumerable(Of T)
            Dim filterColumn = GetType(T).GetProperty(filterParam.ColumnName, BindingFlags.IgnoreCase Or BindingFlags.Instance Or BindingFlags.Public)
            If filterColumn IsNot Nothing Then
                Return FilterData(filterParam.FilterOption, data, filterColumn, filterParam.FilterValue)
            End If

            Return Enumerable.Empty(Of T)()
        End Function
        Private Function FilterData(ByVal filterOption As FilterOptions, ByVal data As IEnumerable(Of T), ByVal filterColumn As PropertyInfo, ByVal filterValue As String) As IEnumerable(Of T)
            Dim outValue As Integer
            Dim dateValue As Date

            Select Case filterOption
                Case FilterOptions.StartsWith
                    data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString.ToLower.StartsWith(filterValue.ToLower))
                Case FilterOptions.EndsWith
                    data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString.ToLower.EndsWith(filterValue.ToLower))
                Case FilterOptions.Contains
                    data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString.ToLower.Contains(filterValue.ToLower))
                Case FilterOptions.DoesNotContain
                    data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso Not filterColumn.GetValue(x).ToString.ToLower.Contains(filterValue.ToLower))
                Case FilterOptions.IsEmpty
                    data = data.Where(Function(x) filterColumn.GetValue(x) Is Nothing OrElse (filterColumn.GetValue(x) IsNot Nothing AndAlso String.IsNullOrWhiteSpace(filterColumn.GetValue(x).ToString)))
                Case FilterOptions.IsNotEmpty
                    data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(filterColumn.GetValue(x).ToString()))
                Case FilterOptions.IsGreaterThan
                    If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                        data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) > outValue)
                    ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                        data = data.Where(Function(x) CDate(filterColumn.GetValue(x)) > dateValue)
                    End If
                Case FilterOptions.IsGreaterThanOrEqualTo
                    If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                        data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) >= outValue)
                    ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                        data = data.Where(Function(x) CDate(filterColumn.GetValue(x)) >= dateValue)
                    End If
                Case FilterOptions.IsLessThan
                    If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                        data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) < outValue)
                    ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                        data = data.Where(Function(x) CDate(filterColumn.GetValue(x, Nothing)) < dateValue)
                    End If
                Case FilterOptions.IsLessThanOrEqualTo
                    If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                        data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) <= outValue)
                    ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                        data = data.Where(Function(x) CDate(filterColumn.GetValue(x)) <= dateValue)
                    End If

                Case FilterOptions.IsEqualTo
                    If filterValue = String.Empty Then
                        data = data.Where(Function(x) filterColumn.GetValue(x) Is Nothing OrElse (filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString().ToLower() = String.Empty))
                    Else
                        If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                            data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) = outValue)
                        ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                            data = data.Where(Function(x) CDate(filterColumn.GetValue(x)) = dateValue)
                        Else
                            data = data.Where(Function(x) filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString().ToLower() = filterValue.ToLower())
                        End If
                    End If
                Case FilterOptions.IsNotEqualTo
                    If filterColumn.PropertyType = GetType(Integer) AndAlso Integer.TryParse(filterValue, outValue) Then
                        data = data.Where(Function(x) CInt(filterColumn.GetValue(x)) <> outValue)
                    ElseIf filterColumn.PropertyType = GetType(Date) AndAlso Date.TryParse(filterValue, dateValue) Then
                        data = data.Where(Function(x) CDate(filterColumn.GetValue(x)) <> dateValue)
                    Else
                        data = data.Where(Function(x) filterColumn.GetValue(x) Is Nothing OrElse (filterColumn.GetValue(x) IsNot Nothing AndAlso filterColumn.GetValue(x).ToString().ToLower() <> filterValue.ToLower()))
                    End If
            End Select
            Return data
        End Function
    End Class
End Namespace
