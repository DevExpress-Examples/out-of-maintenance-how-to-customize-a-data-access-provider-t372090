using System;
using System.Windows.Forms;
using ConsoleProviderCustomization;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.DataAccess.Native.Sql.ConnectionProviders;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.UI.Sql;

namespace QB
{
    public partial class QBForm : Form
    {
        public QBForm()
        {
            InitializeComponent();
        }

        void buttonsRunQb_Click(object sender, EventArgs e)
        {
            DataAccessMSSqlConnectionProvider.ProviderRegister();
            CustomMsSqlConnectionProvider.RegProvider();
            ColumnarDataLoaderFactory.RegisterCustomColumnarDataLoader<CustomMsSqlConnectionProvider>(provider => new CustomColumnDataLoader(provider));
            MsSqlConnectionParameters connectionParameters = new MsSqlConnectionParameters("server_name", "database_name", "username", "password", MsSqlAuthorizationType.SqlServer);
            SqlDataSource yourDataSource = new SqlDataSource(connectionParameters);
            yourDataSource.AddQuery();
        }
    }
}
