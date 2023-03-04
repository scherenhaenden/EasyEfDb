using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyEfDb.Core;

public class ConfigureContext: IConfigureContext
{
    public T GetContext<T>(DatabaseType databaseType, string connectionString, string? providerName = null) where T : DbContext
    {
        var optionsBuilder = BeginGeneration();
        optionsBuilder = GetOptions(databaseType, optionsBuilder, connectionString, providerName);
        return CreateContext<T>(optionsBuilder);

    }
    
    private DbContextOptionsBuilder BeginGeneration() 
    {
        var options = new DbContextOptionsBuilder();
        return options;
    }
    
    private T CreateContext<T>(DbContextOptionsBuilder dbContextOptionsBuilder)where T : DbContext
    {

        var context = (T)Activator.CreateInstance(typeof(T), dbContextOptionsBuilder.Options)!;
        return context;
    }
    
    private DbContextOptionsBuilder GetOptions (DatabaseType databaseType, DbContextOptionsBuilder optionsBuilder, string connectionString, string? providerName) 
    {
        switch (databaseType)
        {
            case DatabaseType.MsSql:
                return optionsBuilder.UseSqlServer(connectionString);
            case DatabaseType.Sqlite:
                return optionsBuilder.UseSqlite(connectionString);
            case DatabaseType.InMemory:
                return optionsBuilder.UseInMemoryDatabase(connectionString);
            case DatabaseType.Cosmos:
                return optionsBuilder.UseCosmos(connectionString, providerName!);
            case DatabaseType.PostgreSql:
                return optionsBuilder.UseNpgsql(connectionString);
            case DatabaseType.MySql:
                return UseMysql(optionsBuilder,connectionString);
            case DatabaseType.Maria:
                return UseMysql(optionsBuilder,connectionString);
            case DatabaseType.Db2:
                return UseDb2(optionsBuilder, connectionString);
            default:
                throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
        }
    }
    
    private DbContextOptionsBuilder UseMysql(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return optionsBuilder;
    }
    
    private DbContextOptionsBuilder UseDb2(DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder
            .UseDb2(connectionString, p =>
            {
                //p.SetServerInfo(IBMDBServerType.LUW, IBMDBServerVersion.None);
                p.SetServerInfo(IBMDBServerType.LUW);
                p.UseRowNumberForPaging();
                p.MaxBatchSize(1);
            });
        return optionsBuilder;
    }
    
    
}