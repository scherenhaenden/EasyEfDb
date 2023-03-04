using Microsoft.EntityFrameworkCore;

namespace EasyEfDb.Core;

public interface IConfigureContext
{   
    T? GetContext <T>( DatabaseType databaseType, string connectionString,  string? providerName = null) where T : DbContext;
    
}