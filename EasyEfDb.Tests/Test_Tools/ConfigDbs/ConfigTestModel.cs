using EasyEfDb.Core;

namespace EasyEfDb.Tests.Test_Tools.ConfigDbs;

public class ConfigTestModel
{
    public string DbType { get; set; }
    public string DbName { get; set; }
    public string DbConnectionString { get; set; }

    public DatabaseType DbTypeFromConfig => GetDatabaseType();


    private DatabaseType GetDatabaseType()
    {
        var databaseType = DbType switch
        {
            "InMemory" => DatabaseType.InMemory,
            //"SqlServer" => DatabaseType.SqlServer,
            "PostgreSql" => DatabaseType.PostgreSql,
            "MySql" => DatabaseType.MySql,
            _ => throw new Exception("Database type not found.")
        };
        return databaseType;
    }
}