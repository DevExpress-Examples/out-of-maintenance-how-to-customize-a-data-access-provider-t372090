Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports DevExpress.DataAccess.Native.Sql
Imports DevExpress.DataAccess.Native.Sql.ConnectionProviders
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Helpers

Namespace ConsoleProviderCustomization
    Public Class CustomMsSqlConnectionProvider
        Inherits DataAccessMSSqlConnectionProvider

        Public Sub New(ByVal connection As IDbConnection, ByVal autoCreateOption As AutoCreateOption)
            MyBase.New(connection, autoCreateOption)
        End Sub

        Private Const aliasLead As String = "["
        Private Const aliasEnd As String = "]"

        Shared Sub New()
            DataConnectionHelper.RegisterProvider(XpoProviderTypeString, AddressOf CustomCreateProviderFromString)
            DataConnectionHelper.RegisterFactory(New CustomMsSqlProviderFactory())
        End Sub

        Public Shared Sub RegProvider()
        End Sub

        Public Shared Function CustomCreateProviderFromString(ByVal connectionString As String, ByVal autoCreateOption As AutoCreateOption, <System.Runtime.InteropServices.Out()> ByRef objectsToDisposeOnDisconnect() As IDisposable) As IDataStore
            Dim connection As IDbConnection = New SqlConnection(connectionString)
            objectsToDisposeOnDisconnect = New IDisposable() {connection}
            Return CreateCustomProviderFromConnection(connection, autoCreateOption)
        End Function

        Public Shared Function CreateCustomProviderFromConnection(ByVal connection As IDbConnection, ByVal autoCreateOption As AutoCreateOption) As IDataStore
            Return New CustomMsSqlConnectionProvider(connection, autoCreateOption)
        End Function

        Public Overrides Function FormatColumn(ByVal columnName As String) As String
            Return EncloseAlias(columnName, aliasLead, aliasEnd)
        End Function

        Public Overrides Function FormatColumn(ByVal columnName As String, ByVal tableAlias As String) As String
            Return String.Format("{0}.{1}", EncloseAlias(tableAlias, aliasLead, aliasEnd), FormatColumn(columnName))
        End Function

        Public Overrides Function FormatTable(ByVal schema As String, ByVal tableName As String, ByVal tableAlias As String) As String

            Dim encloseAlias_Renamed As String = EncloseAlias(tableAlias, aliasLead, aliasEnd)
            Return If(String.IsNullOrEmpty(encloseAlias_Renamed), FormatTable(schema, tableName), FormatTable(schema, tableName) & " " & encloseAlias_Renamed)
        End Function

        Public Overrides Function FormatTable(ByVal schema As String, ByVal tableName As String) As String
            If schema <> String.Empty Then
                Return String.Format("{0}.{1}", EncloseAlias(schema, aliasLead, aliasEnd), EncloseAlias(tableName, aliasLead, aliasEnd))
            End If
            If String.IsNullOrEmpty(ObjectsOwner) Then
                Return aliasLead + tableName & aliasEnd
            End If
            Return String.Format("{0}.{1}", EncloseAlias(ObjectsOwner, aliasLead, aliasEnd), EncloseAlias(tableName, aliasLead, aliasEnd))
        End Function

        Private Function EncloseAlias(ByVal [alias] As String, ByVal lead As String, ByVal [end] As String) As String
            If String.IsNullOrEmpty([alias]) OrElse String.IsNullOrEmpty(lead) OrElse String.IsNullOrEmpty([end]) Then
                Return [alias]
            End If
            Return String.Format("{0}{1}{2}", lead, EscapeAlias([alias], lead, [end]), [end])
        End Function

        Private Function EscapeAlias(ByVal [alias] As String, ByVal lead As String, ByVal [end] As String) As String
            If String.IsNullOrEmpty([alias]) OrElse String.IsNullOrEmpty(lead) OrElse String.IsNullOrEmpty([end]) Then
                Return [alias]
            End If
            Dim result As String = [alias].Replace(lead, Twice(lead))
            If lead <> [end] Then
                result = result.Replace([end], Twice([end]))
            End If
            Return result
        End Function

        Private Function Twice(ByVal value As String) As String
            Return String.Format(CultureInfo.InvariantCulture, "{0}{0}", value)
        End Function
    End Class

    Public Class CustomColumnDataLoader
        Inherits ColumnarDataLoader

        Public Sub New(ByVal provider As ISupportColumnarDataLoader)
            MyBase.New(provider)
        End Sub

        Public Overrides Sub ProcessQuery(ByVal cancellationToken As CancellationToken, ByVal query As Query, ByVal action As Action(Of IDataReader, CancellationToken), ByVal connectionOptions As IConnectionOptions)
            CreateAndExecuteQueryCommand(connectionOptions, query, Sub(command)
                Using reader As IDataReader = command.ExecuteReader(CommandBehavior.Default)
                    action(reader, cancellationToken)
                End Using
            End Sub)
        End Sub
    End Class

    Public Class CustomMsSqlProviderFactory
        Inherits MSSqlProviderFactory

        Public Overrides Function CreateProviderFromString(ByVal connectionString As String, ByVal autoCreateOption As AutoCreateOption, <System.Runtime.InteropServices.Out()> ByRef objectsToDisposeOnDisconnect() As IDisposable) As IDataStore
            Return CustomMsSqlConnectionProvider.CustomCreateProviderFromString(connectionString, autoCreateOption, objectsToDisposeOnDisconnect)
        End Function

        Public Overrides Function CreateProviderFromConnection(ByVal connection As IDbConnection, ByVal autoCreateOption As AutoCreateOption) As IDataStore
            Return CustomMsSqlConnectionProvider.CreateCustomProviderFromConnection(connection, autoCreateOption)
        End Function
    End Class
End Namespace
