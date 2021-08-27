<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128582857/16.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T372090)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomMsSqlConnectionProvider.cs](./CS/ProviderCustomization/CustomMsSqlConnectionProvider.cs) (VB: [CustomMsSqlConnectionProvider.vb](./VB/ProviderCustomization/CustomMsSqlConnectionProvider.vb))
* [Program.cs](./CS/QB/Program.cs) (VB: [Program.vb](./VB/QB/Program.vb))
* [QBForm.cs](./CS/QB/QBForm.cs) (VB: [QBForm.vb](./VB/QB/QBForm.vb))
<!-- default file list end -->
# How to customize a Data Access provider


<p>This example illustrates how to customize the <strong>DataAccessMsSqlConnectionProvider</strong> to use double quotation marks instead of square brackets for enclosing table names. This example also shows the use of <strong>CommandBehavior.Default</strong> by <strong>IDataReader</strong> during the query execution (by default, the Data Access library uses <strong>CommandBehavior.SequentialAccess</strong>). To run this example on your server, specify the required credentials in the invoked <strong>QBForm</strong>.</p>

<br/>


