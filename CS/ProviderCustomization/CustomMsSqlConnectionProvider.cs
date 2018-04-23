using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using DevExpress.DataAccess.Native.Sql.ConnectionProviders;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;

namespace ConsoleProviderCustomization {
    public class CustomMsSqlConnectionProvider : DataAccessMSSqlConnectionProvider {
        public CustomMsSqlConnectionProvider(IDbConnection connection, AutoCreateOption autoCreateOption) : base(connection, autoCreateOption) {}

        const string aliasLead = "[";
        const string aliasEnd = "]";

        static CustomMsSqlConnectionProvider() {
            Register();
            RegisterDataStoreProvider(XpoProviderTypeString, CustomCreateProviderFromString);
            RegisterDataStoreProvider("System.Data.SqlClient.SqlConnection", CreateCustomProviderFromConnection);
            RegisterFactory(new CustomMsSqlProviderFactory());
        }

        public static void RegProvider() {}

        public static IDataStore CustomCreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect) {
            IDbConnection connection = new SqlConnection(connectionString);
            objectsToDisposeOnDisconnect = new IDisposable[] {connection};
            return CreateCustomProviderFromConnection(connection, autoCreateOption);
        }

        public static IDataStore CreateCustomProviderFromConnection(IDbConnection connection, AutoCreateOption autoCreateOption) {
            return new CustomMsSqlConnectionProvider(connection, autoCreateOption);
        }

        public override string FormatColumn(string columnName) {
            return EncloseAlias(columnName, aliasLead, aliasEnd);
        }

        public override string FormatColumn(string columnName, string tableAlias) {
            return string.Format("{0}.{1}", EncloseAlias(tableAlias, aliasLead, aliasEnd), FormatColumn(columnName));
        }

        public override string FormatTable(string schema, string tableName, string tableAlias) {
            string encloseAlias = EncloseAlias(tableAlias, aliasLead, aliasEnd);
            return string.IsNullOrEmpty(encloseAlias)
                ? FormatTable(schema, tableName)
                : FormatTable(schema, tableName) + " " + encloseAlias;
        }

        public override string FormatTable(string schema, string tableName) {
            if(schema != String.Empty)
                return string.Format("{0}.{1}", EncloseAlias(schema, aliasLead, aliasEnd), EncloseAlias(tableName, aliasLead, aliasEnd));
            if(string.IsNullOrEmpty(ObjectsOwner))
                return aliasLead + tableName + aliasEnd;
            return string.Format("{0}.{1}", EncloseAlias(ObjectsOwner, aliasLead, aliasEnd), EncloseAlias(tableName, aliasLead, aliasEnd));
        }

        string EncloseAlias(string alias, string lead, string end) {
            if(String.IsNullOrEmpty(alias) || String.IsNullOrEmpty(lead) || String.IsNullOrEmpty(end))
                return alias;
            return String.Format("{0}{1}{2}", lead, EscapeAlias(alias, lead, end), end);
        }

        string EscapeAlias(string alias, string lead, string end) {
            if(String.IsNullOrEmpty(alias) || String.IsNullOrEmpty(lead) || String.IsNullOrEmpty(end))
                return alias;
            string result = alias.Replace(lead, Twice(lead));
            if(lead != end)
                result = result.Replace(end, Twice(end));
            return result;
        }

        string Twice(string value) {
            return String.Format(CultureInfo.InvariantCulture, "{0}{0}", value);
        }
    }

    public class CustomColumnDataLoader : ColumnarDataLoader {
        public CustomColumnDataLoader(ISupportColumnarDataLoader provider) : base(provider) {}

        public override void ProcessQuery(CancellationToken cancellationToken, Query query, Action<IDataReader, CancellationToken> action, IConnectionOptions connectionOptions) {
            CreateAndExecuteQueryCommand(connectionOptions, query, command => {
                using(IDataReader reader = command.ExecuteReader(CommandBehavior.Default)) {
                    action(reader, cancellationToken);
                }
            });
        }
    }

    public class CustomMsSqlProviderFactory : MSSqlProviderFactory {
        public override IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect) {
            return CustomMsSqlConnectionProvider.CustomCreateProviderFromString(connectionString, autoCreateOption, out objectsToDisposeOnDisconnect);
        }

        public override IDataStore CreateProviderFromConnection(IDbConnection connection, AutoCreateOption autoCreateOption) {
            return CustomMsSqlConnectionProvider.CreateCustomProviderFromConnection(connection, autoCreateOption);
        }
    }
}
