using EasyEfDb.Core;

namespace EasyEfDb.Tests.Test_Tools.ConfigDbs;

public class ConfigTestModel
{
    public string DbType { get; set; } = null!;
    public string DbName { get; set; }= null!;
    public string DbConnectionString { get; set; }= null!;

    public DatabaseType DbTypeFromConfig => GetDatabaseType();


    private DatabaseType GetDatabaseType()
    {
        // print current dbtype 
        Console.WriteLine($"Current DbType: {DbType}");
        
        var databaseType = DbType switch
        {
            "InMemory" => DatabaseType.InMemory,
            //"SqlServer" => DatabaseType.SqlServer,
            "PostgreSql" => DatabaseType.PostgreSql,
            "MySql" => DatabaseType.MySql,
            _ => throw new Exception("Database type not found." + DbType)
        };
        return databaseType;
    }
}