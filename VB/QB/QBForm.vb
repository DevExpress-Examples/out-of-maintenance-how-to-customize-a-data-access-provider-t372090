Imports System
Imports System.Windows.Forms
Imports ConsoleProviderCustomization
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Native.Sql
Imports DevExpress.DataAccess.Native.Sql.ConnectionProviders
Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.UI.Sql

Namespace QB
    Partial Public Class QBForm
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub buttonsRunQb_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonsRunQb.Click
            DataAccessMSSqlConnectionProvider.ProviderRegister()
            CustomMsSqlConnectionProvider.RegProvider()
            ColumnarDataLoaderFactory.RegisterCustomColumnarDataLoader(Of CustomMsSqlConnectionProvider)(Function(provider) New CustomColumnDataLoader(provider))
            Dim connectionParameters As New MsSqlConnectionParameters("server_name", "database_name", "username", "password", MsSqlAuthorizationType.SqlServer)
            Dim yourDataSource As New SqlDataSource(connectionParameters)
            yourDataSource.AddQuery()
        End Sub
    End Class
End Namespace
